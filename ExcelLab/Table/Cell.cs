using System.Diagnostics;

namespace ExcelLab;

public class Cell
{
    public int Row { get; set; }
    public int Column { get; set; }
    public bool Error { get; set; }
    public double ParsedContent { get; set; }

    private string _content;

    public string Content
    {
        get => _content;
        set
        {
            _content = value;
            Debug.Write(_content);
        }
    }

    public Cell(int row, int col, string content)
    {
        Row = row;
        Column = col;
        Content = content;
        Error = false;
    }
}