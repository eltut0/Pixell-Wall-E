using Godot;
using System;

public partial class DebugButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += StartDebugging;
	}

	private void StartDebugging()
	{
		//delete
		InfoWindow.DisplayInfoWindow("Error", "Not implemented function", 300, 75);
		//delete
	}
}
