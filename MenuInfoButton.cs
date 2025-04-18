using Godot;
using System;

public partial class MenuInfoButton : Button
{
	public override void _Ready()
	{
		Pressed += OpenGHRepo;
	}

	private void OpenGHRepo()
	{

	}
}
