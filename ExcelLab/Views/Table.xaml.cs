using System.Windows.Controls;

namespace ExcelLab.Views;

public partial class Table : UserControl
{
    public Table()
    {
        InitializeComponent();
    }

    private void TableDataGrid_OnLoadingRow(object? sender, DataGridRowEventArgs e)
    {
        e.Row.Header = (e.Row.GetIndex() + 1).ToString();
    }
}