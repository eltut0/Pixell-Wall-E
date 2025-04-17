using Godot;
using System;

public partial class ResizeCanvasButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ResizeCanvas;
	}

	//for setting new canvas dimentions
	private void ResizeCanvas()
	{
		//window for canvas size input
		var inputwindow = new Window();
		inputwindow.Title = "Canvas Resize";
		inputwindow.Size = new Vector2I(300, 150);
		inputwindow.Unresizable = true;

		var vbox = new VBoxContainer();
		vbox.Size = inputwindow.Size;

		// label for the window's ui
		var label = new Label
		{
			Text = "Set the new canvas' dimentions:",
			HorizontalAlignment = HorizontalAlignment.Center
		};

		var input = new LineEdit
		{
			PlaceholderText = "Here...",
			SizeFlagsHorizontal = Control.SizeFlags.ExpandFill
		};

		var button = new Button
		{
			Text = "Set",
			SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter
		};

		vbox.AddChild(label);
		vbox.AddChild(input);
		vbox.AddChild(button);
		inputwindow.AddChild(vbox);

		GetTree().Root.AddChild(inputwindow);

		button.Pressed += () =>
		{
			int result;
			if (int.TryParse(input.Text, out result))
			{
				if (result > 0)
				{
					GlobalParameters.ProjectGlobalParameters.CanvasSize = result;
				}
			}

			inputwindow.QueueFree();
		};

		inputwindow.PopupCentered();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
