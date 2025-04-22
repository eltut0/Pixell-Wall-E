using Godot;
using System;

public partial class ExitButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += Exit;
	}

	private static void Exit()
	{
		System.Environment.Exit(0);
	}
}
