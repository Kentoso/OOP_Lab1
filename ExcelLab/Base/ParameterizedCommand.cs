using System;
using System.Windows.Input;

namespace ExcelLab.Table;

public class ParameterizedCommand : ICommand
{
    private Action<object> _action;

    public ParameterizedCommand(Action<object> action)
    {
        _action = action;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter == null) return;
        _action(parameter);
    }

    public event EventHandler? CanExecuteChanged;
}