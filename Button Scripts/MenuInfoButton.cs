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
		Error attempt = OS.ShellOpen(GlobalParameters.ProjectGlobalParameters.GHLink);

		if (attempt == Error.Ok)
		{
			InfoWindow.DisplayInfoWindow("Info", "You have an opened tab on your browser \n with the github repo. Check the README.md file.", 400, 100);
		}
		else
		{
			InfoWindow.DisplayInfoWindow("Error", "Something failed trying to open the browser", 400, 90);
		}
	}
}
