//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.10.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/Demian/RiderProjects/ExcelLab/ExcelLab.Parser\TLang.g4 by ANTLR 4.10.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.10.1")]
[System.CLSCompliant(false)]
public partial class TLangLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, SUM=6, AVG=7, MAX=8, MIN=9, IF=10, 
		CMP=11, FLOAT=12, PLUSPLUS=13, MINUSMINUS=14, PLUS=15, MINUS=16, ASTERISK=17, 
		SLASH=18, WS=19, UKNOWNCHAR=20;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "SUM", "AVG", "MAX", "MIN", "IF", 
		"CMP", "FLOAT", "PLUSPLUS", "MINUSMINUS", "PLUS", "MINUS", "ASTERISK", 
		"SLASH", "DIGITS", "WS", "UKNOWNCHAR"
	};


	public TLangLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public TLangLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'('", "','", "')'", "'!'", "'^'", "'SUM'", "'AVG'", "'MAX'", "'MIN'", 
		"'IF'", "'CMP'", null, null, null, "'+'", "'-'", "'*'", "'/'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, "SUM", "AVG", "MAX", "MIN", "IF", 
		"CMP", "FLOAT", "PLUSPLUS", "MINUSMINUS", "PLUS", "MINUS", "ASTERISK", 
		"SLASH", "WS", "UKNOWNCHAR"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "TLang.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static TLangLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,20,114,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,1,0,1,
		0,1,1,1,1,1,2,1,2,1,3,1,3,1,4,1,4,1,5,1,5,1,5,1,5,1,6,1,6,1,6,1,6,1,7,
		1,7,1,7,1,7,1,8,1,8,1,8,1,8,1,9,1,9,1,9,1,10,1,10,1,10,1,10,1,11,4,11,
		78,8,11,11,11,12,11,79,1,11,1,11,4,11,84,8,11,11,11,12,11,85,3,11,88,8,
		11,1,12,1,12,1,12,1,13,1,13,1,13,1,14,1,14,1,15,1,15,1,16,1,16,1,17,1,
		17,1,18,1,18,1,19,4,19,107,8,19,11,19,12,19,108,1,19,1,19,1,20,1,20,0,
		0,21,1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,10,21,11,23,12,25,13,27,
		14,29,15,31,16,33,17,35,18,37,0,39,19,41,20,1,0,2,1,0,48,57,3,0,9,10,13,
		13,32,32,116,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,
		0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,
		1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,
		0,0,33,1,0,0,0,0,35,1,0,0,0,0,39,1,0,0,0,0,41,1,0,0,0,1,43,1,0,0,0,3,45,
		1,0,0,0,5,47,1,0,0,0,7,49,1,0,0,0,9,51,1,0,0,0,11,53,1,0,0,0,13,57,1,0,
		0,0,15,61,1,0,0,0,17,65,1,0,0,0,19,69,1,0,0,0,21,72,1,0,0,0,23,77,1,0,
		0,0,25,89,1,0,0,0,27,92,1,0,0,0,29,95,1,0,0,0,31,97,1,0,0,0,33,99,1,0,
		0,0,35,101,1,0,0,0,37,103,1,0,0,0,39,106,1,0,0,0,41,112,1,0,0,0,43,44,
		5,40,0,0,44,2,1,0,0,0,45,46,5,44,0,0,46,4,1,0,0,0,47,48,5,41,0,0,48,6,
		1,0,0,0,49,50,5,33,0,0,50,8,1,0,0,0,51,52,5,94,0,0,52,10,1,0,0,0,53,54,
		5,83,0,0,54,55,5,85,0,0,55,56,5,77,0,0,56,12,1,0,0,0,57,58,5,65,0,0,58,
		59,5,86,0,0,59,60,5,71,0,0,60,14,1,0,0,0,61,62,5,77,0,0,62,63,5,65,0,0,
		63,64,5,88,0,0,64,16,1,0,0,0,65,66,5,77,0,0,66,67,5,73,0,0,67,68,5,78,
		0,0,68,18,1,0,0,0,69,70,5,73,0,0,70,71,5,70,0,0,71,20,1,0,0,0,72,73,5,
		67,0,0,73,74,5,77,0,0,74,75,5,80,0,0,75,22,1,0,0,0,76,78,3,37,18,0,77,
		76,1,0,0,0,78,79,1,0,0,0,79,77,1,0,0,0,79,80,1,0,0,0,80,87,1,0,0,0,81,
		83,5,46,0,0,82,84,3,37,18,0,83,82,1,0,0,0,84,85,1,0,0,0,85,83,1,0,0,0,
		85,86,1,0,0,0,86,88,1,0,0,0,87,81,1,0,0,0,87,88,1,0,0,0,88,24,1,0,0,0,
		89,90,3,29,14,0,90,91,3,29,14,0,91,26,1,0,0,0,92,93,3,31,15,0,93,94,3,
		31,15,0,94,28,1,0,0,0,95,96,5,43,0,0,96,30,1,0,0,0,97,98,5,45,0,0,98,32,
		1,0,0,0,99,100,5,42,0,0,100,34,1,0,0,0,101,102,5,47,0,0,102,36,1,0,0,0,
		103,104,7,0,0,0,104,38,1,0,0,0,105,107,7,1,0,0,106,105,1,0,0,0,107,108,
		1,0,0,0,108,106,1,0,0,0,108,109,1,0,0,0,109,110,1,0,0,0,110,111,6,19,0,
		0,111,40,1,0,0,0,112,113,9,0,0,0,113,42,1,0,0,0,5,0,79,85,87,108,1,6,0,
		0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
