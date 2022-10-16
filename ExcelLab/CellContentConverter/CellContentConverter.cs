using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using Antlr4.Runtime.Misc;
using ExcelLab.Parser.Interpreter;
using ExcelLab.Table;

namespace ExcelLab;

public class CellContentConverter
{
    private static CellContentConverter? _instance;

    public static CellContentConverter Instance
    {
        get
        {
            return _instance ??= new CellContentConverter();
        }
    }

    public void Convert(Cell cell, TableData tableData)
    {
        if (cell.Content.Trim() == "")
        {
            cell.Content = "";
            cell.ParsedContent = "";
            cell.Error = ErrorStates.None;
            return;
        }
        cell.Error = ErrorStates.None;
        Regex addressRegex = new Regex(@"(?<=\|)[A-Z]+[1-9][0-9]*(?=\|)", RegexOptions.IgnoreCase);
        var contentCopy = PreprocessAddressRange(cell.Content);
        if (!UpdateDeps(cell, tableData, contentCopy, addressRegex)) return;
        if (cell.IsThereASelfDependencyRecursive())
        {
            FillDependencyCycleWithRecursionError(cell);
            return;
        }
        contentCopy = ReplaceAddresses(tableData, contentCopy, addressRegex);
        if (contentCopy == null) cell.Error = ErrorStates.Address;
        if (cell.Error == ErrorStates.None)
        {
            cell.ParsedContent = Interpreter.Instance.Interpret(contentCopy).ToString(CultureInfo.InvariantCulture);
            foreach (var dep in cell.Dependents)
            {
                Convert(dep, tableData);
            }
        }
    }

    private bool UpdateDeps(Cell cell, TableData tableData, string content, Regex addressRegex)
    {
        var prevDependencies = new List<Cell>(cell.Dependencies);
        cell.Dependencies = new List<Cell>();
        var matches = addressRegex.Matches(content);
        foreach (Match address in matches)
        {
            string a = address.Value;
            var addressedCell = tableData.GetCell(a);
            if (addressedCell == null)
            {
                cell.Error = ErrorStates.Address;
                return false;
            }
            cell.Dependencies.Add(addressedCell);
            if (!addressedCell.Dependents.Contains(cell)) addressedCell.Dependents.Add(cell);
        }
        foreach (var pDep in prevDependencies)
        {
            if (cell.Dependencies.Contains(pDep)) continue;
            pDep.Dependents.Remove(cell);
        }
        return true;
    }
    private string? ReplaceAddresses(TableData tableData, string content, Regex addressRegex)
    {
        var contentCopy = content;
        var matches = addressRegex.Matches(contentCopy);
        while (matches.Count > 0)
        {
            foreach (Match address in matches)
            {
                string a = address.Value;
                var addressedCell = tableData.GetCell(a);
                if (addressedCell == null) return null; 
                var addressedCellContent = addressedCell.Content.Trim() == "" ? "0" : addressedCell.Content;
                contentCopy = contentCopy.Replace($"|{a}|", $"({addressedCellContent})");
                contentCopy = PreprocessAddressRange(contentCopy);
            }
            matches = addressRegex.Matches(contentCopy);
        }
        return contentCopy;
    }
    private void FillDependencyCycleWithRecursionError(Cell cell)
    {
        cell.Error = ErrorStates.Recursion;
        Stack<Cell> s = new Stack<Cell>();
        List<Cell> visited = new List<Cell>();
        Cell current = cell;
        s.Push(current);
        while (s.Count > 0)
        {
            current = s.Pop();
            if (!visited.Contains(current))
            {
                foreach (var dep in current.Dependents)
                {
                    dep.Error = ErrorStates.Recursion;
                    if (!visited.Contains(dep)) s.Push(dep);
                }

                visited.Add(current);
            }
        }
    }

    private string PreprocessAddressRange(string content)
    {
        Regex addressRegex = new Regex(@"(?<=\|)[A-Z]+[1-9][0-9]*:[A-Z]+[1-9][0-9]*(?=\|)", RegexOptions.IgnoreCase);
        var matches = addressRegex.Matches(content);
        var contentCopy = content;
        foreach (Match match in matches)
        {
            var value = match.Value;
            var colonIndex = value.IndexOf(':');
            var startAddress = value.Substring(0, colonIndex);
            var endAddress = value.Substring(colonIndex + 1);
            
            (int startRow, int startCol) = AddressToNumbers(startAddress);
            (int endRow, int endCol) = AddressToNumbers(endAddress);
            
            string result = "";
            for (int i = startRow; i <= endRow; i++)
            {
                for (int j = startCol; j <= endCol; j++)
                {
                    if ((i != startRow || j != startCol)) result += ",";
                    result += $"|{TableData.ConvertColumnIndexToHeader(j)}{i}|";
                }
            }

            contentCopy = contentCopy.Replace($"|{match.Value}|", result);
        }
        return contentCopy;
    }

    private (int, int) AddressToNumbers(string address)
    {
        var firstNumIndex = address.IndexOfAny("0123456789".ToCharArray());
        var columnStr = address.Substring(0, firstNumIndex).ToUpper();
        int row = Int32.Parse(address.Substring(firstNumIndex));
        int col = TableData.DecodeColumnHeader(columnStr);
        return (row, col);
    }
}