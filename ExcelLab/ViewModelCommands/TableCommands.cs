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
        for (int i = 0; i < table.Size.columns; i++)
        {
            string header = "";
            if (i > 25)
            {
                int t = i + 1;
                List<int> iIn26System = new List<int>();
                while (t > 0)
                {
                    iIn26System.Add(t % 26);
                    t /= 26;
                }

                bool isShort = iIn26System.Count == 1;
                iIn26System = iIn26System.Select((x, k) => k == 0 && !isShort ? x - 1 : x - 1).ToList();
                iIn26System.Reverse();

                for (int j = 0; j < iIn26System.Count; j++)
                {
                    header += (char) ('A' + iIn26System[j]);
                }
            }
            else
            {
                header = ((char) ('A' + i)).ToString();
            }
            
            var column = new DataGridTextColumn
            {
                Header = header,
                Width = 50,
                Binding = new Binding($"Cells[{i}].Content")
                    {UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged}
            };

            dg.Columns.Add(column);
        }
    }
}