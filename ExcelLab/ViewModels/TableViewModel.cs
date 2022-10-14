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
    
    public ICommand CellEditEndedCommand { get; set;  }
    public ICommand CellEditBeganCommand { get; set;  }
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
            // if (CurrentCell != null) CurrentCell.IsSelected = false;
            // Table.GetCell("AN1");
            var p = parameter as SelectedCellsChangedEventArgs;
            var row = p.AddedCells[0].Item as Row;
            CurrentCell = row?.Cells[p.AddedCells[0].Column.DisplayIndex] ?? new Cell(0, 0, "error");
        });
        DataGridLoadedCommand = new ParameterizedCommand((parameter) =>
        {
            TableCommands.DataGridLoaded(parameter, Table);
        });
        CellEditBeganCommand = new ParameterizedCommand(parameter =>
        {
            if (CurrentCell == null) return;
            CurrentCell.IsEdited = true;
        });
        CellEditEndedCommand = new ParameterizedCommand(parameter =>
        {
            if (CurrentCell == null) return;
            CellContentConverter.Instance.Convert(CurrentCell, Table);
            // foreach (var dep in CurrentCell.Dependents)
            // {
            //     dep.ParsedContent = CellContentConverter.Instance.Convert(dep, Table);
            // }
            Debug.Write(parameter);
            CurrentCell.IsEdited = false;
        });
    }
}