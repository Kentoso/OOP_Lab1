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
            string header = "";
            if (i < 26) header = ((char) ('A' + i)).ToString();
            else
            {
                int t = i + 1;
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
}