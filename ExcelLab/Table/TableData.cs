using System;
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

    public Cell GetCell(string address)
    {   
        var firstNumberIndex = address.IndexOfAny("0123456789".ToCharArray());
        if (firstNumberIndex == -1) return null;
        var column = address.Substring(0, firstNumberIndex).ToUpper();
        int row = Convert.ToInt32(address.Substring(firstNumberIndex)) - 1;
        if (row >= Size.rows) return null;
        int columnNumber = -1;
        for (int i = 0; i < column.Length; i++)
        {
            columnNumber += (int)Math.Pow(26, i) * (column[column.Length - 1 - i] - 'A' + 1);
        }
        if (columnNumber >= Size.columns) return null;
        return Rows[row].Cells[columnNumber];
    }
}