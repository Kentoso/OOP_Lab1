using System.Collections.Generic;

namespace ExcelLab.Table;

public class TableData
{
    public (int width, int height) Size { get; set; }
    
    public List<Row> Rows { get; set; }
}