using Godot;
using System;
using System.Linq;
using System.Threading;

public partial class RealTimeExceptionsControl : Control
{
	private static string[] Code { get; set; } = [];
	public override void _Process(double delta)
	{
		if (Compiler.CodeCompiler.DontCheck) { return; }
		CodeEditord editor = GetNode<CodeEditord>(GlobalParameters.ProjectGlobalParameters.CodeEditorNode);
		string[] code = editor.GetCode();
		GD.Print(code.Length);
		if (code.SequenceEqual(Code)) { return; }
		Code = code;

		Lexer.Lexer.InitializeLex(code);
		Parser.Parser.ParserInit(true);
	}
}
