using System;
using Xunit;
using ExcelLab.Parser.Interpreter;
namespace ParserTests;

public class ParserTests
{
    [Theory]
    [InlineData("3 + 4", 7)]
    [InlineData("(3 + 4) * 3", 21)]
    [InlineData("3 + 4 * 4", 19)]
    [InlineData("3 * 4 * 3", 36)]
    [InlineData("3 * 5 + 6", 21)]
    private void BasicExpressions(string content, double expected)
    {
        var result = Interpreter.Instance.Interpret(content);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("--3", 2)]
    [InlineData("--10 + 10", 19)]
    [InlineData("--(12 + 3)", 14)]
    [InlineData("---7", -8)]
    [InlineData("+++7", 8)]
    private void UnaryOperations(string content, double expected)
    {
        var result = Interpreter.Instance.Interpret(content);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("24!", 620448401733239439360000.0)]
    [InlineData("4!", 24.0)]
    private void BigNumberOperations(string content, double expected)
    {
        var result = Interpreter.Instance.Interpret(content);
        bool isEqual = Math.Abs(result - expected) < expected * 0.5;
        Assert.True(isEqual);
    }
    
    [Theory]
    [InlineData("2^5", 32)]
    [InlineData("1/3", 1/3)]
    private void PrecisionOperations(string content, double expected)
    {
        var result = Interpreter.Instance.Interpret(content);
        bool isEqual = Math.Abs(result - expected) < expected * 1e-10;
        Assert.True(isEqual);
    }
}