using System.Collections.Generic;
using System.Collections.ObjectModel;
using PropertyChanged;

namespace ExcelLab.Table;

[AddINotifyPropertyChangedInterface]
public class Row
{
    public static int RowCount = 0;
    public ObservableCollection<Cell> Cells { get; set; }

    public Row(int cNumber)
    {
        Cells = new ObservableCollection<Cell>();
        for (int i = 0; i < cNumber; i++)
        {
            // Cells.Add(new Cell(_rowCount, i, $"row: {_rowCount}, col: {i}"));
            Cells.Add(new Cell(RowCount, i, ""));
        }
        RowCount++;
    }
    
    public Row() {}

    public void AddCell()
    {
        Cells.Add(new Cell(Cells[0].Coordinates.Row, Cells.Count, ""));
    }
}