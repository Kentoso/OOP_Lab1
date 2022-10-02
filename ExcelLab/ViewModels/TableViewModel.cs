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
    public ICommand SelectCellCommand { get; set; }
    public ICommand RowNumerationUpdateCommand { get; set; }
    public ICommand DataGridLoadedCommand { get; set; }
    // public ObservableCollection<Row> TableRows { get; set; }
    public Cell CurrentCell { get; set; }
    
    public TableData Table { get; set; }
    // public ObservableCollection<Cell> Cells { get; set; }

    public TableViewModel()
    {
        Table = new TableData((10, 40));
        // TableRows = new ObservableCollection<Row>();
        // TableRows.Add(new Row(ColumnNumber));
        // TableRows.Add(new Row(ColumnNumber));
        // TableRows.Add(new Row(ColumnNumber));
        // TableRows.Add(new Row(ColumnNumber));
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
            TableCommands.DataGridLoaded(parameter, Table);
        });
    }

    private string GetCellContent((int x, int y) coord)
    {
        // return TableRows[coord.x].Cells[coord.y].Content;
        return "";
    }
    
    


}