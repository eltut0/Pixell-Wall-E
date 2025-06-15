using Godot;
using System;
using System.Threading;

public partial class RealTimeExceptionsControl : Control
{
	public override void _Process(double delta)
	{
		if (Compiler.CodeCompiler.DontCheck) { return; }
		CodeEditord editor = GetNode<CodeEditord>(GlobalParameters.ProjectGlobalParameters.CodeEditorNode);
		string[] code = editor.GetCode();
		Lexer.Lexer.InitializeLex(code);
		Parser.Parser.ParserInit(true);
	}
}
