using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ExcelLab.Table;

public static class TableCommands
{
    public static void DataGridLoaded(object? parameter, TableData table)
    {
        var p = parameter as RoutedEventArgs;
        var dg = p.Source as DataGrid;
        var headers = GenerateColumnHeaders(table);
        SetDataGridColumns(dg, headers);
    }

    private static List<string> GenerateColumnHeaders(TableData table)
    {
        List<string> result = new List<string>();
        for (int i = 0; i < table.Size.columns; i++)
        {
            string header = TableData.ConvertColumnIndexToHeader(i);
            result.Add(header);
        }
        return result;
    }

    private static void SetDataGridColumns(DataGrid dg, List<string> headers)
    {
        for (int i = 0; i < headers.Count; i++)
        {
            var column = new DataGridTextColumn
            {
                Header = headers[i],
                Width = 50,
                Binding = new Binding($"Cells[{i}].ViewContent")
                    {UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged}
            };
            dg.Columns.Add(column);
        }
    }

    public static void AddOrRemoveColumn(DataGrid dg, TableData table)
    {
        var tableColNum = table.Size.columns;
        var dgColNum = dg.Columns.Count;
        if (tableColNum > dgColNum)
        {
            var column = new DataGridTextColumn
            {
                Header = TableData.ConvertColumnIndexToHeader(tableColNum - 1),
                Width = 50,
                Binding = new Binding($"Cells[{tableColNum - 1}].ViewContent")
                    {UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged}
            };
            dg.Columns.Add(column);
        }
        else if (tableColNum < dgColNum)
        {
            dg.Columns.RemoveAt(dgColNum - 1);
        }
    }

    public static void RegenerateColumnHeaders(DataGrid dg, TableData table)
    {
        var headers = GenerateColumnHeaders(table);
        dg.Columns.Clear();
        SetDataGridColumns(dg, headers);
    }
    
}