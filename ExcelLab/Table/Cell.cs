using System.Collections.Generic;
using System.Diagnostics;
using ExcelLab.Table;
using PropertyChanged;

namespace ExcelLab;
[AddINotifyPropertyChangedInterface]
public class Cell
{
    // public int Row { get; set; }
    // public int Column { get; set; }
    public (int Row, int Column) Coordinates { get; }
    public ErrorStates Error { get; set; }
    public bool IsEdited { get; set; }

    public List<Cell> Dependencies;
    public List<Cell> Dependents;
    public string ParsedContent { get; set; }
    public string Content { get; set; }
    private string _previousContent { get; set; }

    public string ViewContent
    {
        get
        {
            if (IsEdited) return Content;
            switch (Error)
            {
                case ErrorStates.None:
                    return ParsedContent;
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
        Coordinates = (row, col);
        ViewContent = content;
        Error = ErrorStates.None;
        Dependencies = new List<Cell>();
        Dependents = new List<Cell>();
        Content = "";
        ParsedContent = "";
    }
    
    public Cell() {}

    public bool IsThereASelfDependencyRecursive(HashSet<Cell>? path = null)
    {
        if (path == null) path = new HashSet<Cell>();
        if (path.Contains(this)) return true;
        if (Dependencies.Count == 0) return false;
        path.Add(this);
        bool a = false;
        foreach (var dep in Dependencies)
        {
            a = a || dep.IsThereASelfDependencyRecursive(new HashSet<Cell>(path));
        }
        return a;
    }
}