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

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="TLangParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.10.1")]
[System.CLSCompliant(false)]
public interface ITLangVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="TLangParser.content"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitContent([NotNull] TLangParser.ContentContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>parentheses</c>
	/// labeled alternative in <see cref="TLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParentheses([NotNull] TLangParser.ParenthesesContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>number</c>
	/// labeled alternative in <see cref="TLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumber([NotNull] TLangParser.NumberContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>factorial</c>
	/// labeled alternative in <see cref="TLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFactorial([NotNull] TLangParser.FactorialContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>addsub</c>
	/// labeled alternative in <see cref="TLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAddsub([NotNull] TLangParser.AddsubContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>decinc</c>
	/// labeled alternative in <see cref="TLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecinc([NotNull] TLangParser.DecincContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>muldiv</c>
	/// labeled alternative in <see cref="TLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMuldiv([NotNull] TLangParser.MuldivContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exponent</c>
	/// labeled alternative in <see cref="TLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExponent([NotNull] TLangParser.ExponentContext context);
}