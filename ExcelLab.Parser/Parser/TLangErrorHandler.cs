using System.Diagnostics;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace ExcelLab.Parser;

public class TLangErrorHandler : BaseErrorListener
{
    private static TLangErrorHandler _instance;
    public static TLangErrorHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TLangErrorHandler();
            }

            return _instance;
        }
    }
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine,
        string msg, RecognitionException e)
    {
        throw new ParseCanceledException($"Error at index {charPositionInLine}. Char '{offendingSymbol.Text}'");
    }   
}