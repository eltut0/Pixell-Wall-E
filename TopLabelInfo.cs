using Godot;
using System;

public partial class TopLabelInfo : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateLabel();
	}

	public void UpdateLabel()
	{
		string text = Convert.ToString(GlobalParameters.ProjectGlobalParameters.CanvasSize);
		Text = $"The current canvas size is {text} * {text}" + " -- " + $"The current file is: {GlobalParameters.ProjectGlobalParameters.ActualFileLocation}" + "                                                   Developed by Jorge Julio de Leon Masson";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateLabel();
	}
}
