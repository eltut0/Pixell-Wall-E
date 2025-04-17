using Godot;

public partial class RunCodeButton : Button
{
	public override void _Ready()
	{
		Pressed += RunProgram;
	}

	private void RunProgram()
	{
		CodeEditord editor = GetNode<CodeEditord>("/root/Main/Code_editor_container2/Editor/CodeEdit");
		string[] code = editor.GetCode();

		GD.Print("candela");
		GD.Print(code.Length);

		foreach (string line in code)
		{
			GD.Print(line);
		}
	}
}
