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
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.10.1")]
[System.CLSCompliant(false)]
public partial class TLangParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		Number=1, Identifier=2, Equals=3, Whitespace=4, Newline=5;
	public const int
		RULE_file = 0, RULE_assignment = 1;
	public static readonly string[] ruleNames = {
		"file", "assignment"
	};

	private static readonly string[] _LiteralNames = {
		null, null, null, "'='"
	};
	private static readonly string[] _SymbolicNames = {
		null, "Number", "Identifier", "Equals", "Whitespace", "Newline"
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

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static TLangParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public TLangParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public TLangParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class FileContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public AssignmentContext assignment() {
			return GetRuleContext<AssignmentContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Eof() { return GetToken(TLangParser.Eof, 0); }
		public FileContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_file; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			ITLangListener typedListener = listener as ITLangListener;
			if (typedListener != null) typedListener.EnterFile(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			ITLangListener typedListener = listener as ITLangListener;
			if (typedListener != null) typedListener.ExitFile(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITLangVisitor<TResult> typedVisitor = visitor as ITLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFile(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public FileContext file() {
		FileContext _localctx = new FileContext(Context, State);
		EnterRule(_localctx, 0, RULE_file);
		try {
			State = 6;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case Identifier:
				EnterOuterAlt(_localctx, 1);
				{
				State = 4;
				assignment();
				}
				break;
			case Eof:
				EnterOuterAlt(_localctx, 2);
				{
				State = 5;
				Match(Eof);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AssignmentContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Identifier() { return GetToken(TLangParser.Identifier, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Equals() { return GetToken(TLangParser.Equals, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Number() { return GetToken(TLangParser.Number, 0); }
		public AssignmentContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_assignment; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			ITLangListener typedListener = listener as ITLangListener;
			if (typedListener != null) typedListener.EnterAssignment(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			ITLangListener typedListener = listener as ITLangListener;
			if (typedListener != null) typedListener.ExitAssignment(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			ITLangVisitor<TResult> typedVisitor = visitor as ITLangVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAssignment(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public AssignmentContext assignment() {
		AssignmentContext _localctx = new AssignmentContext(Context, State);
		EnterRule(_localctx, 2, RULE_assignment);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 8;
			Match(Identifier);
			State = 9;
			Match(Equals);
			State = 10;
			Match(Number);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static int[] _serializedATN = {
		4,1,5,13,2,0,7,0,2,1,7,1,1,0,1,0,3,0,7,8,0,1,1,1,1,1,1,1,1,1,1,0,0,2,0,
		2,0,0,11,0,6,1,0,0,0,2,8,1,0,0,0,4,7,3,2,1,0,5,7,5,0,0,1,6,4,1,0,0,0,6,
		5,1,0,0,0,7,1,1,0,0,0,8,9,5,2,0,0,9,10,5,3,0,0,10,11,5,1,0,0,11,3,1,0,
		0,0,1,6
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
