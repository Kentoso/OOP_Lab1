using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExcelLab.Annotations;
using PropertyChanged;

namespace ExcelLab.Table;

[AddINotifyPropertyChangedInterface]
public class TableData
{
    public (int rows, int columns) Size { get; set; }
    
    public List<Row> Rows { get; set; }

    public TableData((int rows, int columns) size)
    {
        Size = size;
        Rows = new List<Row>();
        for (int i = 0; i < Size.rows; i++)
        {
            var row = new Row(Size.columns);
            Rows.Add(row);
        }
    }
}