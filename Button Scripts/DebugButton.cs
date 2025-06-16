using Godot;

public partial class DebugButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += StartDebugging;
	}

	private void StartDebugging()
	{
		Color[,] colors = new Color[GlobalParameters.ProjectGlobalParameters.CanvasSize, GlobalParameters.ProjectGlobalParameters.CanvasSize];
		for (int i = 0; i < colors.GetLength(0); i++)
		{
			for (int j = 0; j < colors.GetLength(0); j++)
			{
				colors[i, j] = Colors.Transparent;
			}
		}

		Canvas.Canvas cv = GetNode<Canvas.Canvas>(GlobalParameters.ProjectGlobalParameters.CanvasNode);
		cv.SetColors(colors);
	}
}
