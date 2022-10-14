using System;
using Antlr4.Runtime.Misc;
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
    [InlineData("1/3", 1.0/3.0)]
    private void PrecisionOperations(string content, double expected)
    {
        var result = Interpreter.Instance.Interpret(content);
        bool isEqual = Math.Abs(result - expected) < expected * 1e-10;
        Assert.True(isEqual);
    }

    [Theory]
    [InlineData("2+a")]
    [InlineData("abc")]
    private void ErrorOperations(string content)
    {
        Assert.Throws<ParseCanceledException>(() => Interpreter.Instance.Interpret(content));
    }

    [Theory]
    [InlineData("MAX(1, 3^2)", 9)]
    [InlineData("MIN(1/12, -12, 332, 11)", -12)]
    [InlineData("AVG(1, 2, 3, 4, 5, 6)", 3.5)]
    [InlineData("IF(0, 1, 2)", 2)]
    [InlineData("IF(1, 1, 2)", 1)]
    [InlineData("CMP(1, 1)", 0)]
    [InlineData("CMP(0.333333333, 1/3)", 0)]
    [InlineData("CMP(100, 99.9999)", 1)]
    [InlineData("CMP(100, 100.0000001)", -1)]
    private void Functions(string content, double expected)
    {
        var result = Interpreter.Instance.Interpret(content);
        Assert.Equal(expected, result);
    }
}