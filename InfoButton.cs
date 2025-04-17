using Godot;
using System;

public partial class InfoButton : Button
{
	public override void _Ready()
	{
		Pressed += InfoWindow;
	}

	private void InfoWindow()
	{
		//window for canvas size input
		var inputwindow = new Window();
		inputwindow.Title = "Visual Wall-E Code";
		inputwindow.Size = new Vector2I(300, 150);
		inputwindow.Unresizable = true;

		var vbox = new VBoxContainer();
		vbox.Size = inputwindow.Size;

		//label 
		var editorv = new Label
		{
			Text = $"Editor version: {GlobalParameters.ProjectGlobalParameters.EditorVersion}",
			HorizontalAlignment = HorizontalAlignment.Center
		};

		var compilerv = new Label
		{
			Text = $"Compiler version: {GlobalParameters.ProjectGlobalParameters.CompilerVersion}",
			HorizontalAlignment = HorizontalAlignment.Center
		};

		var button = new Button
		{
			Text = "OK",
			SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter
		};

		vbox.AddChild(editorv);
		vbox.AddChild(compilerv);
		vbox.AddChild(button);
		inputwindow.AddChild(vbox);

		GetTree().Root.AddChild(inputwindow);

		button.Pressed += () =>
		{
			inputwindow.QueueFree();
		};

		inputwindow.PopupCentered();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
