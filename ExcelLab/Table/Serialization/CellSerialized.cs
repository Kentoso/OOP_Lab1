namespace ExcelLab.Table.Serialization;

public class CellSerialized
{
    public (int Row, int Column) Coordinates { get; set;  }
    public string Content { get; set; }
}