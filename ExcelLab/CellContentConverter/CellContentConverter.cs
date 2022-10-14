using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
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
        cell.Error = ErrorStates.None;
        if (cell.Content == "")
        {
            cell.ParsedContent = "";
            return;
        }
        Regex addressRegex = new Regex(@"(?<=\|)[A-Z]+[1-9][0-9]*(?=\|)", RegexOptions.IgnoreCase);
        if (!UpdateDeps(cell, tableData, addressRegex)) return;
        if (cell.IsThereADependencyCycle())
        {
            FillDependencyCycleWithRecursionError(cell);
            return;
        }
        var contentCopy = cell.Content;
        contentCopy = ReplaceAddresses(tableData, contentCopy, addressRegex);
        cell.ParsedContent = Interpreter.Instance.Interpret(contentCopy).ToString(CultureInfo.InvariantCulture);

        foreach (var dep in cell.Dependents)
        {
            Convert(dep, tableData);
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
                cell.ParsedContent = "#ADDRESS_ERROR#";
                return false;
            }

            cell.Dependencies.Add(addressedCell);
            if (!addressedCell.Dependents.Contains(cell)) addressedCell.Dependents.Add(cell);
            // var addressedCellContent = addressedCell.Content.Trim() == "" ? "0" : addressedCell.Content;
            // contentCopy = contentCopy.Replace($"|{a}|", $"({addressedCellContent})");
        }
        foreach (var pDep in prevDependencies)
        {
            if (cell.Dependencies.Contains(pDep)) continue;
            pDep.Dependents.Remove(cell);
        }
        return true;
    }
    private string ReplaceAddresses(TableData tableData, string content, Regex addressRegex)
    {
        //TODO: iterative address replacement with content
        var contentCopy = content;
        var matches = addressRegex.Matches(contentCopy);
        while (matches.Count > 0)
        {
            foreach (Match address in matches)
            {
                string a = address.Value;
                var addressedCell = tableData.GetCell(a);
                if (addressedCell == null)
                {
                    return "#ADDRESS_ERROR#";
                }
                var addressedCellContent = addressedCell.Content.Trim() == "" ? "0" : addressedCell.Content;
                contentCopy = contentCopy.Replace($"|{a}|", $"({addressedCell.Content})");
            }
            matches = addressRegex.Matches(contentCopy);
        }
        return contentCopy;
    }
    private void FillDependencyCycleWithRecursionError(Cell cell)
    {
        cell.ParsedContent = "#RECURSION_ERROR$";
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
                    dep.ParsedContent = "#RECURSION_ERROR$";
                    if (!visited.Contains(dep)) s.Push(dep);
                }

                visited.Add(current);
            }
        }
    }
    // public string Convert(Cell cell, TableData tableData)
    // {
    //     if (cell.ViewContent == "") return "";
    //     var prevDependencies = new List<Cell>(cell.Dependencies);
    //     cell.Dependencies = new List<Cell>();
    //     var contentCopy = cell.Content;
    //     Regex addressRegex = new Regex(@"(?<=\|)[A-Z]+[1-9]+(?=\|)");
    //     var matches = addressRegex.Matches(contentCopy);
    //     // while (matches.Count > 0)
    //     // {
    //     //     foreach (Match address in matches)
    //     //     {
    //     //         string a = address.Value;
    //     //         var addressedCell = tableData.GetCell(a);
    //     //         if (addressedCell == null) return "#ADDRESS_ERROR#";
    //     //         if (addressedCell == cell) return "#RECURSION_ERROR#";
    //     //         contentCopy = contentCopy.Replace($"|{a}|", addressedCell.Content);
    //     //     }
    //     //     matches = addressRegex.Matches(contentCopy);
    //     // }
    //     foreach (Match address in matches)
    //     {
    //         string a = address.Value;
    //         var addressedCell = tableData.GetCell(a);
    //         if (addressedCell == null) return "#ADDRESS_ERROR#";
    //         cell.Dependencies.Add(addressedCell);
    //         if (!addressedCell.Dependents.Contains(cell)) addressedCell.Dependents.Add(cell);
    //         contentCopy = contentCopy.Replace($"|{a}|", $"({addressedCell.ViewContent})");
    //     }
    //
    //     foreach (var pDep in prevDependencies)
    //     {
    //         if (cell.Dependencies.Contains(pDep)) continue;
    //         pDep.Dependents.Remove(cell);
    //     }
    //     if (cell.IsThereADependencyCycle())
    //     {
    //         // foreach (var dep in cell.Dependencies)
    //         // {
    //         //     dep.ParsedContent = Convert(dep, tableData);
    //         // }
    //         return "#RECURSION_ERROR$";
    //     }
    //
    //     cell.ParsedContent = Interpreter.Instance.Interpret(contentCopy).ToString(CultureInfo.InvariantCulture);
    //
    //     foreach (var dep in cell.Dependents)
    //     {
    //         dep.ParsedContent = Convert(dep, tableData);
    //     }
    //     
    //     return Interpreter.Instance.Interpret(contentCopy).ToString();
    // }

   
    // public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    // {
    //     string content = value as string;
    //     if (content == "") return "";
    //     return Interpreter.Instance.Interpret(content).ToString();
    // }
    //
    // public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    // {
    //     return value.ToString();
    // }
}