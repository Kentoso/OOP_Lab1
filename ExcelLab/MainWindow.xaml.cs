using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExcelLab.Table;

namespace ExcelLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // for (int i = 0; i < TableViewModel.ColumnNumber; i++)
            // {
            //     var column = new DataGridTextColumn
            //     {
            //         Header = ((char)('A' + i)).ToString(),
            //         Width = 50,
            //         Binding = new Binding($"Cells[{i}].Content") {UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged}
            //     };
            //     
            //     TableDataGrid.Columns.Add(column);
            // }
        }

        private void TableDataGrid_OnLoadingRow(object? sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()).ToString();
        }
    }
}