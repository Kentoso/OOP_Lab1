using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using ExcelLab.Annotations;
using PropertyChanged;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ExcelLab.Table;

[AddINotifyPropertyChangedInterface]
public class TableData
{
    public (int rows, int columns) Size { get; set; }
    
    public ObservableCollection<Row> Rows { get; set; }

    public TableData((int rows, int columns) size)
    {
        Size = size;
        Rows = new ObservableCollection<Row>();
        for (int i = 0; i < Size.rows; i++)
        {
            var row = new Row(Size.columns);
            Rows.Add(row);
        }
    }
    public TableData() {}

    public Cell? GetCell(string address)
    {
        var firstNumberIndex = address.IndexOfAny("0123456789".ToCharArray());
        if (firstNumberIndex == -1) return null;
        var column = address.Substring(0, firstNumberIndex).ToUpper();
        int row = Convert.ToInt32(address.Substring(firstNumberIndex)) - 1;
        if (row >= Size.rows) return null;
        int columnNumber = DecodeColumnHeader(column);
        if (columnNumber >= Size.columns) return null;
        return Rows[row].Cells[columnNumber];
    }

    public static int DecodeColumnHeader(string columnHeader)
    {
        int columnNumber = -1;
        for (int i = 0; i < columnHeader.Length; i++)
        {
            columnNumber += (int) Math.Pow(26, i) * (columnHeader[columnHeader.Length - 1 - i] - 'A' + 1);
        }
        return columnNumber;
    }
    public static string ConvertColumnIndexToHeader(int number)
    {
        string header = "";
        if (number < 26) header = ((char) ('A' + number)).ToString();
        else
        {
            int t = number + 1;
            List<int> iIn26System = new List<int>();
            while (t > 0)
            {
                iIn26System.Add(t % 26);
                t /= 26;
            }

            for (int j = 0; j < iIn26System.Count; j++) iIn26System[j]--;
            iIn26System.Reverse();

            for (int j = 0; j < iIn26System.Count; j++)
            {
                header += (char) ('A' + iIn26System[j]);
            }
        }
        return header;
    }
    public void AddRow()
    {
        Size = (Size.rows + 1, Size.columns);
        var row = new Row(Size.columns);
        Rows.Add(row);
    }

    public void AddColumn()
    {
        Size = (Size.rows, Size.columns + 1);
        foreach (var row in Rows)
        {
            row.AddCell();
        }
    }

    public bool RemoveRow()
    {
        if (Size.rows == 1) return false;
        var lastRow = Rows[^1];
        foreach (var cell in lastRow.Cells)
        {
            if (cell.Content != "") return false;
        }
        Rows.RemoveAt(Rows.Count - 1);
        Size = (Size.rows - 1, Size.columns);
        return true;
    }

    public bool RemoveColumn()
    {
        if (Size.columns == 1) return false;
        var lastColumn = new List<Cell>();
        foreach (var row in Rows)
        {
            lastColumn.Add(row.Cells[^1]);
        }
        foreach (var cell in lastColumn)
        {
            if (cell.Content != "") return false;
        }
        foreach (var row in Rows)
        {
            row.Cells.RemoveAt(row.Cells.Count - 1);
        }
        Size = (Size.rows, Size.columns - 1);
        return true;
    }

    public async void SerializeToYaml(string path)
    {
        var serializer = new SerializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance).Build();
        var yaml = serializer.Serialize(this);
        await File.WriteAllTextAsync(path, yaml);
    }

    public static async Task<TableData> DeserializeFromYaml(string path)
    {
        var deserializer = new DeserializerBuilder().WithNamingConvention(PascalCaseNamingConvention.Instance).Build();
        var yaml = await File.ReadAllTextAsync(path);
        var tableData = deserializer.Deserialize<TableData>(yaml);
        return tableData;
    }

    public async void SerializeToJson(string path)
    {
        var options = new JsonSerializerOptions() {IncludeFields = true};
        var json = JsonSerializer.Serialize(this, options);
        await File.WriteAllTextAsync(path, json);
    }

    public static async Task<TableData> DeserializeFromJson(string path)
    {
        var json = await File.ReadAllTextAsync(path);
        var options = new JsonSerializerOptions() {IncludeFields = true};
        var tableData = JsonSerializer.Deserialize<TableData>(json, options);
        return tableData;
    }
}