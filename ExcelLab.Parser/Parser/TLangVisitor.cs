using System.Globalization;
using Antlr4.Runtime;

namespace ExcelLab.Parser;

public class TLangVisitor : TLangBaseVisitor<double>
{
    public override double VisitDecinc(TLangParser.DecincContext context)
    {
        return Visit(context.expression()) + (context.op.Type == TLangParser.PLUSPLUS ? 1 : -1);
    }

    public override double VisitExponent(TLangParser.ExponentContext context)
    {
        return Math.Pow(Visit(context.expression()[0]), Visit(context.expression()[1]));
    }

    public override double VisitFactorial(TLangParser.FactorialContext context)
    {
        double num = Visit(context.expression());
        double t = Math.Sqrt(2 * num * Math.PI) * Math.Pow(num / Math.E, num);
        double result = t + t / (12 * num) + 1 / (288 * num * num);
        return Math.Round(result);
    }

    public override double VisitNumber(TLangParser.NumberContext context)
    {
        var text = context.GetText();
        return Convert.ToDouble(text, CultureInfo.InvariantCulture);
    }

    public override double VisitParentheses(TLangParser.ParenthesesContext context)
    {
        return Visit(context.expression());
    }

    public override double VisitContent(TLangParser.ContentContext context)
    {
        return Visit(context.expression());
    }

    public override double VisitAddsub(TLangParser.AddsubContext context)
    {
        var leftSubtreeResult = Visit(context.expression()[0]);
        var rightSubtreeResult = Visit(context.expression()[1]);
        var result = context.op.Type == TLangParser.PLUS
            ? leftSubtreeResult + rightSubtreeResult
            : leftSubtreeResult - rightSubtreeResult;
        return result;
    }

    public override double VisitMuldiv(TLangParser.MuldivContext context)
    {
        var leftSubtreeResult = Visit(context.expression()[0]);
        var rightSubtreeResult = Visit(context.expression()[1]);
        var result = context.op.Type == TLangParser.ASTERISK
            ? leftSubtreeResult * rightSubtreeResult
            : leftSubtreeResult / rightSubtreeResult;
        return result;
    }
}