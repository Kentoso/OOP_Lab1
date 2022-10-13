// See https://aka.ms/new-console-template for more information

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ExcelLab.Parser
{
    public static class Program
    {
        public static void Main()
        {
            // var text = File.ReadAllText(@"C:\Users\Demian\RiderProjects\ExcelLab\ExcelLab.Parser\test.txt");
            var stream = CharStreams.fromString("1+5+2");
            var lexer = new TLangLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new TLangParser(tokens) {BuildParseTree = true};
            
            var parseTree = parser.content();
            // Console.WriteLine(parseTree.ToStringTree());
            // TLangListener listener = new TLangListener();
            // ParseTreeWalker.Default.Walk(listener, parseTree);
        }
    }
}