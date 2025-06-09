using System.Text;
using Godot;
using Parser;

public partial class ErrorsTextBox : RichTextLabel
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	public override void _Process(double delta)
	{
		if (!(Exception.exceptionList.Count > 0)) { return; }

		StringBuilder exceptionsText = new();
		exceptionsText.Append("Exceptions: \n");
		foreach (var exception in Exception.exceptionList)
		{
			exceptionsText.Append($"Exception at line: {exception.Line}, {exception.Lex}, Exception type: {exception.Type.ToString()} \n");
		}

		Text = exceptionsText.ToString();
	}
}
