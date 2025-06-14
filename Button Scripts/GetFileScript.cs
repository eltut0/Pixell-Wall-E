using Godot;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public partial class GetFileScript : Button
{
	private static string TempFileName { get; set; }

	[Signal]
	public delegate void OnDecisionMadeEventHandler(string response);
	public override void _Ready()
	{
		Pressed += async () => await GetFileName();
	}

	private async Task GetFileName()
	{
		GetFileScript localnode = GetNode<GetFileScript>(GlobalParameters.ProjectGlobalParameters.LoadButtonNode);
		DisplayInputWindow(localnode);

		await ToSignal(localnode, "OnDecisionMade");

		if (TempFileName != null)
		{
			GetTheScript(localnode);
		}
	}

	private static void GetTheScript(Node localnode)
	{
		string route = Path.Combine(GlobalParameters.ProjectGlobalParameters.GenericLocalFolderRoute, $"{TempFileName}.pw");
		GlobalParameters.ProjectGlobalParameters.ActualFileLocation = TempFileName;
		TempFileName = null;
		try
		{
			using StreamReader sr = new(route);
			GlobalParameters.ProjectGlobalParameters.CanvasSize = int.Parse(sr.ReadLine());
			StringBuilder sb = new();
			do
			{
				string line = sr.ReadLine();

				if (line == null)
				{
					break;
				}

				sb.Append($"{line}\n");

			} while (true);

			sr.Close();
			sb.Remove(sb.Length - 1, 1);

			SetScript(sb.ToString(), localnode);
		}
		catch (FileNotFoundException)
		{
			InfoWindow.DisplayInfoWindow("Error", "File not found", 300, 90);
		}
	}

	private static void SetScript(string code, Node local)
	{
		CodeEdit codeEdit = local.GetNode<CodeEdit>(GlobalParameters.ProjectGlobalParameters.CodeEditorNode);

		codeEdit.Clear();
		codeEdit.Text = code;
	}

	private static void DisplayInputWindow(GetFileScript localnode)
	{
		var inputwindow = new Window
		{
			Title = "Save file",
			Size = new Vector2I(300, 150),
			Unresizable = true
		};

		var vbox = new VBoxContainer
		{
			Size = inputwindow.Size
		};
		var hbox = new HBoxContainer();

		var label = new Label
		{
			Text = "File name",
			HorizontalAlignment = HorizontalAlignment.Center
		};

		var input = new LineEdit
		{
			PlaceholderText = $"Press info for listing the local files",
			SizeFlagsHorizontal = Control.SizeFlags.ExpandFill
		};

		var button = new Button
		{
			Text = "Done",
			SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter
		};

		var button2 = new Button
		{
			Text = "Info",
			SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter
		};

		vbox.AddChild(label);
		vbox.AddChild(input);
		vbox.AddChild(hbox);
		hbox.AddChild(button);
		hbox.AddChild(button2);
		inputwindow.AddChild(vbox);

		localnode.GetTree().Root.AddChild(inputwindow);

		button.Pressed += () =>
		{
			input.Text = Utils.FileNameCorrection(input.Text);
			TempFileName = input.Text;
			localnode.EmitSignal(SignalName.OnDecisionMade, "confirmed");
			inputwindow.QueueFree();
		};

		button2.Pressed += () =>
		{
			FileListingWindow();
			inputwindow.QueueFree();
		};

		inputwindow.CloseRequested += inputwindow.QueueFree;

		inputwindow.PopupCentered();
	}

	private static void FileListingWindow()
	{
		StringBuilder sb = new();

		string[] files = Directory.GetFiles(GlobalParameters.ProjectGlobalParameters.GenericLocalFolderRoute);
		int count = 0;
		int maxlenght = 0;
		if (files.Length > 0)
		{
			foreach (string file in files)
			{
				string[] splittedfile = file.Split('/');
				sb.Append($"{splittedfile[^1]}\n");

				if (splittedfile[^1].Length > maxlenght)
				{
					maxlenght = splittedfile[^1].Length;
				}
				count++;
			}
			sb.Remove(sb.Length - 1, 1);

			InfoWindow.DisplayInfoWindow("Files", sb.ToString(), 160 + (maxlenght * 10), 40 + count * 25);
		}
		else
		{
			InfoWindow.DisplayInfoWindow("Empty directory", "There aren't avaiable files. \n Copy files to the Local Files folder for \n loading to the editor", 300, 120);
		}
	}
}
