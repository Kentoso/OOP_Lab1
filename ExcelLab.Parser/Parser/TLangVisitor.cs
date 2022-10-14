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

    public override double VisitMinmax(TLangParser.MinmaxContext context)
    {
        var expressions = context._exps;
        if (expressions.Count == 0) return double.NaN;
        List<double> results = expressions.Select(e => Visit(e)).ToList();
        if (context.op.Type == TLangParser.MAX)
        {
            double max = double.MinValue;
            foreach (var r in results)
            {
                if (max < r) max = r;
            }
            return max;
        }
        else
        {
            double min = double.MaxValue;
            foreach (var r in results)
            {
                if (min > r) min = r;
            }
            return min;
        }
    }

    public override double VisitAvg(TLangParser.AvgContext context)
    {
        var expressions = context._exps;
        if (expressions.Count == 0) return 0;
        List<double> results = expressions.Select(e => Visit(e)).ToList();
        double result = 0;
        foreach (var r in results) result += r;
        result /= results.Count;
        return result;
    }

    public override double VisitIf(TLangParser.IfContext context)
    {
        var condition = context.condition;
        var result = Visit(condition) != 0 ? Visit(context.ifbranch) : Visit(context.elsebranch);
        return result;
    }

    public override double VisitCmp(TLangParser.CmpContext context)
    {
        var first = Visit(context.expression()[0]);
        var second = Visit(context.expression()[1]);
        if (first > second) return 1;
        else if (Math.Abs(first - second) < first * 1e-10) return 0;
        else return -1;
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