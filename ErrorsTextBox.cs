using System.Text;
using Godot;
using Parser;

public partial class ErrorsTextBox : RichTextLabel
{
	public override void _Process(double delta)
	{
		if (!(Exception.exceptionList.Count > 0))
		{
			Text = "";
			return;
		}

		StringBuilder exceptionsText = new();
		exceptionsText.Append("Exceptions: \n");
		foreach (var exception in Exception.exceptionList)
		{
			exceptionsText.Append($"Exception at line: {exception.Line}, {exception.Type.ToString()}, {exception.Lex} \n");
		}

		Text = exceptionsText.ToString();
	}
}
