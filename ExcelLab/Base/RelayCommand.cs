using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ExcelLab.Table;

public class RelayCommand : ICommand
{
    private Action _action;
    
    public RelayCommand(Action action)
    {
        _action = action;
    }
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _action();
        Debug.WriteLine(parameter);
    }

    public event EventHandler? CanExecuteChanged;
}