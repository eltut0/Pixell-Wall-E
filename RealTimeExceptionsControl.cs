using Godot;
using System;
using System.Threading;

public partial class RealTimeExceptionsControl : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CodeEditord editor = GetNode<CodeEditord>(GlobalParameters.ProjectGlobalParameters.CodeEditorNode);
		string[] code = editor.GetCode();
		Lexer.Lexer.InitializeLex(code);
		Parser.Parser.ParserInit();
	}
}
