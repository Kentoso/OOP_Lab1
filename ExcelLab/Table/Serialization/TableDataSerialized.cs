using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExcelLab.Table.Serialization;

public class TableDataSerialized
{
    public (int rows, int columns) Size { get; set; }

    public List<RowSerialized> Rows { get; set; }

    public static TableDataSerialized Serialize(TableData table)
    {
        var result = new TableDataSerialized();
        result.Rows = new List<RowSerialized>();
        result.Size = table.Size;
        foreach (var row in table.Rows)
        {
            var newRow = new RowSerialized();
            newRow.Cells = new List<CellSerialized>();
            RowSerialized.RowCount = Row.RowCount;
            foreach (var cell in row.Cells)
            {
                var newCell = new CellSerialized();
                newCell.Coordinates = cell.Coordinates;
                newCell.Content = cell.Content;
                newRow.Cells.Add(newCell);
            }
            result.Rows.Add(newRow);
        }
        return result;
    }

    public TableData Deserialize()
    {
        TableData result = new TableData();
        result.Size = Size;
        result.Rows = new ObservableCollection<Row>();
        foreach (var row in Rows)
        {
            var newRow = new Row();
            newRow.Cells = new ObservableCollection<Cell>();
            foreach (var cell in row.Cells)
            {
                var newCell = new Cell(cell.Coordinates.Row, cell.Coordinates.Column, cell.Content);
                newRow.Cells.Add(newCell);
            }

            result.Rows.Add(newRow);
        }

        return result;
    }
}