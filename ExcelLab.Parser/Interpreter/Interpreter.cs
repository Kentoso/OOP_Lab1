using Antlr4.Runtime;
using ExcelLab.Parser;

namespace ExcelLab.Parser.Interpreter;

public class Interpreter
{
    private TLangLexer Lexer;
    private CommonTokenStream TokenStream;
    private TLangParser Parser;
    private static Interpreter _instance = null;
    
    public static Interpreter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Interpreter();
            }
            return _instance;
        }
    }
    
    public Interpreter()
    {
        Lexer = new TLangLexer(CharStreams.fromString(""));
        TokenStream = new CommonTokenStream(Lexer);
        Parser = new TLangParser(TokenStream) {BuildParseTree = true};
    }

    public double Interpret(string content)
    {
        var stream = CharStreams.fromString(content);
        Lexer.SetInputStream(stream);
        TokenStream.SetTokenSource(Lexer);
        Parser = new TLangParser(TokenStream) {BuildParseTree = true};
        Parser.RemoveErrorListeners();
        Parser.AddErrorListener(TLangErrorHandler.Instance);
        TLangVisitor visitor = new TLangVisitor();
        var b = Parser.content();
        var result = visitor.Visit(b);
        // Parser.NumberOfSyntaxErrors
        return result;
    }
}