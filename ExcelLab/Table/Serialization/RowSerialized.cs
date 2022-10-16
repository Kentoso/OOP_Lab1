using System.Collections.Generic;

namespace ExcelLab.Table.Serialization;

public class RowSerialized
{
    public static int RowCount = 0;
    
    public List<CellSerialized> Cells { get; set; }
}