using Antlr4.Runtime;
using ExcelLab.Parser;

namespace ExcelLab.Parser.Interpreter;

public class Interpreter
{
    private TLangLexer Lexer;
    private CommonTokenStream TokenStream;
    private TLangParser _parser;
    private static Interpreter? _instance;
    
    public static Interpreter Instance
    {
        get { return _instance ??= new Interpreter(); }
    }
    
    public Interpreter()
    {
        Lexer = new TLangLexer(CharStreams.fromString(""));
        TokenStream = new CommonTokenStream(Lexer);
        _parser = new TLangParser(TokenStream) {BuildParseTree = true};
    }

    public double Interpret(string content)
    {
        var stream = CharStreams.fromString(content);
        Lexer.SetInputStream(stream);
        TokenStream.SetTokenSource(Lexer);
        _parser = new TLangParser(TokenStream) {BuildParseTree = true};
        _parser.RemoveErrorListeners();
        _parser.AddErrorListener(TLangErrorHandler.Instance);
        TLangVisitor visitor = new TLangVisitor();
        var b = _parser.content();
        var result = visitor.Visit(b);
        // Parser.NumberOfSyntaxErrors
        return result;
    }
}