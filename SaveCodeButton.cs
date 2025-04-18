using Godot;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public partial class SaveCodeButton : Button
{
	[Signal]
	public delegate void OnDecisionMadeEventHandler(string response);

	public override void _Ready()
	{
		Pressed += async () => await SaveCode();
	}

	//creation or change of a file
	private async Task SaveCode()
	{
		SaveCodeButton localnode = GetNode<SaveCodeButton>(GlobalParameters.ProjectGlobalParameters.SaveButtonNode);
		InteractiveWindow(localnode);

		await ToSignal(localnode, "OnDecisionMade");

		OverWriteFile(localnode);
	}

	public static void InteractiveWindow(SaveCodeButton localnode)
	{
		var inputwindow = new Window();
		inputwindow.Title = "Save file";
		inputwindow.Size = new Vector2I(300, 150);
		inputwindow.Unresizable = true;

		var vbox = new VBoxContainer();
		vbox.Size = inputwindow.Size;

		var label = new Label
		{
			Text = "File name",
			HorizontalAlignment = HorizontalAlignment.Center
		};

		var input = new LineEdit
		{
			PlaceholderText = $"Actually: {GlobalParameters.ProjectGlobalParameters.ActualFileLocation}",
			SizeFlagsHorizontal = Control.SizeFlags.ExpandFill
		};

		var button = new Button
		{
			Text = "Done",
			SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter
		};

		vbox.AddChild(label);
		vbox.AddChild(input);
		vbox.AddChild(button);
		inputwindow.AddChild(vbox);

		localnode.GetTree().Root.AddChild(inputwindow);

		button.Pressed += () =>
		{
			//if enters the file with the .pw
			if (input.Text[input.Text.Length - 1] == 'w' && input.Text[input.Text.Length - 2] == 'p' && input.Text[input.Text.Length - 3] == '.')
			{
				input.Text = input.Text.Substring(0, input.Text.Length - 3);
			}
			GlobalParameters.ProjectGlobalParameters.ActualFileLocation = $"{input.Text}.pw";
			localnode.EmitSignal(SignalName.OnDecisionMade, "confirmed");
			inputwindow.QueueFree();
		};

		inputwindow.CloseRequested += () => inputwindow.QueueFree();

		inputwindow.PopupCentered();
	}

	public static void OverWriteFile(SaveCodeButton localnode)
	{
		try
		{
			using (StreamWriter sw = new StreamWriter(Path.Combine(GlobalParameters.ProjectGlobalParameters.GenericLocalFolderRoute, GlobalParameters.ProjectGlobalParameters.ActualFileLocation)))
			{
				CodeEditord editor = localnode.GetNode<CodeEditord>(GlobalParameters.ProjectGlobalParameters.CodeEditorNode);
				string[] code = editor.GetCode();

				sw.WriteLine(GlobalParameters.ProjectGlobalParameters.CanvasSize);

				foreach (string line in code)
				{
					sw.WriteLine(line);
				}
			}
		}
		catch (DirectoryNotFoundException)
		{
			GlobalParameters.ProjectGlobalParameters.ActualFileLocation = "Non opened project";
			InfoWindow.DisplayInfoWindow("Warning", "The current name is not valid!", 300, 90);
		}
	}
}
