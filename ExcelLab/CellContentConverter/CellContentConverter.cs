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
    private static CellContentConverter _instance;

    public static CellContentConverter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CellContentConverter();
            }
            return _instance;
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

    public string PreprocessAddressRange(string content)
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
            
            var startFirstNumIndex = startAddress.IndexOfAny("0123456789".ToCharArray());
            var startColumnStr = startAddress.Substring(0, startFirstNumIndex).ToUpper();
            int startRow = Int32.Parse(startAddress.Substring(startFirstNumIndex));
            int startCol = TableData.DecodeColumnHeader(startColumnStr);

            var endFirstNumIndex = endAddress.IndexOfAny("0123456789".ToCharArray());
            var endColumnStr = endAddress.Substring(0, endFirstNumIndex).ToUpper();
            int endRow = Int32.Parse(endAddress.Substring(endFirstNumIndex));
            int endCol = TableData.DecodeColumnHeader(endColumnStr);
            
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
}