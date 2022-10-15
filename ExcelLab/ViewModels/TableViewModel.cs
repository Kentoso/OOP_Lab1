using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Antlr4.Runtime.Misc;
using ExcelLab.Table;

namespace ExcelLab;

public class TableViewModel : BaseViewModel
{
    public ICommand SelectCellCommand { get; set; }
    public ICommand AddRowCommand { get; set; }
    public ICommand AddColumnCommand { get; set; }
    public ICommand ScrollCommand { get; set; }
    public ICommand RemoveRowCommand { get; set; }
    public ICommand RemoveColumnCommand { get; set; }
    public ICommand DataGridLoadedCommand { get; set; }
    public ICommand CellEditEndedCommand { get; set;  }
    public ICommand CellEditBeganCommand { get; set;  }
    public ICommand SaveTableCommand { get; set; }
    public ICommand OpenTableCommand { get; set; }
    public ICommand NewTableCommand { get; set; }
    public Cell CurrentCell { get; set; }
    public TableData Table { get; set; }
    public string LatestSyntaxError { get; set; }
    
    private DataGrid _dg;

    public TableViewModel()
    {
        Table = new TableData((10, 10));
        LatestSyntaxError = "";
        _dg = new DataGrid();
        CurrentCell = new Cell(-1, -1, "");
        SetRelayCommands();
        SetParametrizedCommands();
    }

    private void SetRelayCommands()
    {
        AddRowCommand = new RelayCommand(() => { Table.AddRow(); });
        AddColumnCommand = new RelayCommand(() =>
        {
            Table.AddColumn();
            AddRemoveColumnHeaders();
        });
        RemoveRowCommand = new RelayCommand(() => { Table.RemoveRow(); });
        RemoveColumnCommand = new RelayCommand(() =>
        {
            if (Table.RemoveColumn()) AddRemoveColumnHeaders();
        });
        SaveTableCommand = new RelayCommand(() =>
        {
            using var dialog = new SaveFileDialog();
            dialog.ShowHelp = false;
            dialog.Filter = "All Files | *.*";
            if (dialog.ShowDialog() == DialogResult.OK && dialog.CheckPathExists)
            {
                Table.SerializeToJson(dialog.FileName);
            }
        });
        OpenTableCommand = new RelayCommand(async () =>
        {
            using var dialog = new OpenFileDialog();
            dialog.ShowHelp = false;
            dialog.Filter = "All Files | *.*";
            if (dialog.ShowDialog() == DialogResult.OK && dialog.CheckPathExists)
            {
                CurrentCell = null;
                Table = null;
                LatestSyntaxError = "";
                Table = await TableData.DeserializeFromJson(dialog.FileName);
                TableCommands.RegenerateColumnHeaders(_dg, Table);
            }
        });
        NewTableCommand = new RelayCommand(() =>
        {
            var messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to create new table? (Unsaved changes will be deleted)", "New table confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                CurrentCell = null;
                Table = null;
                LatestSyntaxError = "";
                Table = new TableData((10, 10));
                TableCommands.RegenerateColumnHeaders(_dg, Table);
            }
        });
    }

    private void SetParametrizedCommands()
    {
        SelectCellCommand = new ParameterizedCommand((parameter) =>
        {
            var p = parameter as SelectedCellsChangedEventArgs;
            if (p == null || p.AddedCells.Count == 0) return;
            var row = p.AddedCells[0].Item as Row;
            CurrentCell = row?.Cells[p.AddedCells[0].Column.DisplayIndex] ?? new Cell(0, 0, "error");
        });
        DataGridLoadedCommand = new ParameterizedCommand((parameter) =>
        {
            TableCommands.DataGridLoaded(parameter, Table);
            var p = parameter as RoutedEventArgs;
            _dg = p.Source as DataGrid;
        });
        CellEditBeganCommand = new ParameterizedCommand(parameter =>
        {
            if (CurrentCell == null) return;
            CurrentCell.IsEdited = true;
        });
        ScrollCommand = new ParameterizedCommand(parameter =>
        {
            var p = parameter as MouseWheelEventArgs;
            var dg = p?.Source as DataGrid;
            var scrollViewer = dg?.Parent as ScrollViewer;
            scrollViewer?.ScrollToVerticalOffset(scrollViewer.VerticalOffset - p.Delta);
            p.Handled = true;
        });
        CellEditEndedCommand = new ParameterizedCommand(parameter =>
        {
            if (CurrentCell == null) return;
            LatestSyntaxError = "";
            try
            {
                CellContentConverter.Instance.Convert(CurrentCell, Table);
            }
            catch (ParseCanceledException e)
            {
                CurrentCell.Error = ErrorStates.Syntax;
                LatestSyntaxError = $"{e.Message}";
            }

            CurrentCell.IsEdited = false;
        });
    }
    private void AddRemoveColumnHeaders()
    {
        TableCommands.AddOrRemoveColumn(_dg, Table);
    }
}