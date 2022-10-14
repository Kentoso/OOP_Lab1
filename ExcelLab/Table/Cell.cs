using System.Collections.Generic;
using System.Diagnostics;
using ExcelLab.Table;
using PropertyChanged;

namespace ExcelLab;
[AddINotifyPropertyChangedInterface]
public class Cell
{
    public int Row { get; set; }
    public int Column { get; set; }
    public ErrorStates Error { get; set; }
    public bool IsEdited { get; set; }

    public List<Cell> Dependencies;
    public List<Cell> Dependents;
    public string ParsedContent { get; set; }
    
    public string Content { get; set; }

    public string ViewContent
    {
        get {
            switch (Error)
            {
                case ErrorStates.None:
                    return IsEdited ? Content : ParsedContent;
                case ErrorStates.Recursion:
                    return "#RECURSION_ERROR#";
                case ErrorStates.Address:
                    return "#ADDRESS_ERRROR#";
                case ErrorStates.Syntax:
                    return "#SYNTAX_ERROR#";
                default:
                    return "#UNKNOWN_ERROR#";
            }
        } 
        set => Content = value;
    }

    public Cell(int row, int col, string content)
    {
        Row = row;
        Column = col;
        ViewContent = content;
        Error = ErrorStates.None;
        Dependencies = new List<Cell>();
        Dependents = new List<Cell>();
    }

    public bool IsThereADependencyCycle()
    {
        Cell current = this;
        List<Cell> visited = new List<Cell>();
        Stack<Cell> s = new Stack<Cell>();
        s.Push(this);
        while (s.Count > 0)
        {
            current = s.Pop();
            if (visited.Contains(current)) return true;
            visited.Add(current);
            foreach (var dep in current.Dependencies)
            {
                s.Push(dep);
            }
        }
        return false;
    }
}