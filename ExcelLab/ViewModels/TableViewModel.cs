using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ExcelLab.Table;

namespace ExcelLab;

public class TableViewModel : BaseViewModel
{

    public static int ColumnNumber = 80;
    public ICommand SelectCellCommand { get; set; }
    public ICommand RowNumerationUpdateCommand { get; set; }
    public ICommand DataGridLoadedCommand { get; set; }
    public ObservableCollection<Row> TableRows { get; set; }
    public Cell CurrentCell { get; set; }
    // public ObservableCollection<Cell> Cells { get; set; }

    public TableViewModel()
    {
        TableRows = new ObservableCollection<Row>();
        TableRows.Add(new Row(ColumnNumber));
        TableRows.Add(new Row(ColumnNumber));
        TableRows.Add(new Row(ColumnNumber));
        TableRows.Add(new Row(ColumnNumber));
        SelectCellCommand = new ParameterizedCommand((parameter) =>
        {
            var p = parameter as SelectedCellsChangedEventArgs;
            var row = p.AddedCells[0].Item as Row;
            CurrentCell = row?.Cells[p.AddedCells[0].Column.DisplayIndex] ?? new Cell(0, 0, "error");
        });
        RowNumerationUpdateCommand = new ParameterizedCommand((parameter) =>
        {
            var p = parameter as DataGridRowEventArgs;
            p.Row.Header = p.Row.GetIndex();
        });
        DataGridLoadedCommand = new ParameterizedCommand((parameter) =>
        {
            var p = parameter as RoutedEventArgs;
            var dg = p.Source as DataGrid;
            for (int i = 0; i < ColumnNumber; i++)
            {
                int t = i + 1;
                List<int> iIn26System = new List<int>();
                while (t > 0)
                { 
                    iIn26System.Add(t % 26);
                    t /= 26;
                }

                bool isShort = iIn26System.Count == 1;
                iIn26System = iIn26System.Select((x,k) => k == 0 && !isShort ? x : x - 1).ToList();
                iIn26System.Reverse();
                
                string header = "";

                for (int j = 0; j < iIn26System.Count; j++)
                {
                    header += (char) ('A' + iIn26System[j]);
                }
                Debug.WriteLine(header);
                var column = new DataGridTextColumn
                {
                    Header = header,
                    Width = 50,
                    Binding = new Binding($"Cells[{i}].Content")
                        {UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged}
                };

                dg.Columns.Add(column);
            }
            Debug.Write(p);
        });
    }

    private string GetCellContent((int x, int y) coord)
    {
        return TableRows[coord.x].Cells[coord.y].Content;
    }
    
    


}