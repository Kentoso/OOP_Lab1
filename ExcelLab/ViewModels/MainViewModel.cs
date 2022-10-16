using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using ExcelLab.Table;

namespace ExcelLab;

public class MainViewModel : BaseViewModel
{
    public BaseViewModel CurrentViewModel { get; set; }
    public ICommand DisplayNewTableCommand { get; set; } 
    public ICommand DisplayExistingTableCommand { get; set; } 
    public ICommand DisplayMenuCommand { get; set; }
    public MainViewModel()
    {
        DisplayMenuCommand = new RelayCommand(() =>
        {
            var messageBoxResult = System.Windows.MessageBox.Show(
                "Are you sure you want to return to menu? (Unsaved changes will be deleted)",
                "Return to menu?", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                CurrentViewModel = new MenuViewModel();
            }
        });
        CurrentViewModel = new MenuViewModel();
        DisplayNewTableCommand = new RelayCommand(() => { CurrentViewModel = new TableViewModel();});
        DisplayExistingTableCommand = new RelayCommand(async () =>
        {
            using var dialog = new OpenFileDialog();
            dialog.ShowHelp = false;
            dialog.Filter = "All Files | *.*";
            if (dialog.ShowDialog() == DialogResult.OK && dialog.CheckPathExists)
            {
                var tableData = await TableData.DeserializeFromJson(dialog.FileName);
                CurrentViewModel = new TableViewModel(tableData);
            }
        });
    }
    
}