using System.Collections.Generic;
using PropertyChanged;

namespace ExcelLab.Table;

[AddINotifyPropertyChangedInterface]
public class Row
{
    private static int _rowCount = 0;
    public List<Cell> Cells { get; set; }

    public Row(int cNumber)
    {
        _rowCount++;
        Cells = new List<Cell>();
        for (int i = 0; i < cNumber; i++)
        {
            Cells.Add(new Cell(_rowCount, i, $"row: {_rowCount}, col: {i}"));
        }
    }
}