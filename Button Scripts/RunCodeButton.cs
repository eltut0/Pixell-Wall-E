using Godot;
using Parser;

public partial class RunCodeButton : Button
{
	public override void _Ready()
	{
		Pressed += RunProgram;
	}

	private void RunProgram()
	{
		CodeEditord editor = GetNode<CodeEditord>(GlobalParameters.ProjectGlobalParameters.CodeEditorNode);
		string[] code = editor.GetCode();
		Compiler.CodeCompiler.RegularCompilationStart(code);
	}
}