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
        if (!UpdateDeps(cell, tableData, addressRegex)) return;
        if (cell.IsThereADependencyCycle())
        {
            FillDependencyCycleWithRecursionError(cell);
            return;
        }
        var contentCopy = ReplaceAddresses(tableData, cell.Content, addressRegex);
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

    private bool UpdateDeps(Cell cell, TableData tableData, Regex addressRegex)
    {
        var prevDependencies = new List<Cell>(cell.Dependencies);
        cell.Dependencies = new List<Cell>();
        var matches = addressRegex.Matches(cell.Content);
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
}