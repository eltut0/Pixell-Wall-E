using System.Threading;
using Godot;

public partial class RunCodeButton : Button
{
	public override void _Ready()
	{
		Pressed += RunProgram;
	}

	private void RunProgram()
	{
		//delete
		InfoWindow.DisplayInfoWindow("Error", "Not implemented function", 300, 75);
		//delete
		CodeEditord editor = GetNode<CodeEditord>(GlobalParameters.ProjectGlobalParameters.CodeEditorNode);
		string[] code = editor.GetCode();

		Compiler.CodeCompiler.RegularCompilationStart(code);
	}
}