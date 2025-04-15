using Godot;
using System;

public partial class CodeEditord : CodeEdit
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CodeHighlighter syntaxhighlight = new CodeHighlighter();
		SyntaxHighlighter = syntaxhighlight;

		syntaxhighlight.KeywordColors = (Godot.Collections.Dictionary)new Godot.Collections.Dictionary<string, Color>()
		{
			{"GoTo", new Color("#FF79C6")},
			//amarillo para los relacionados con la posicion de Wall-E y sus variables
			{"Spawn", new Color("#FFFF00")},
			{"Color", new Color("#FFFF00")},
			{"Size", new Color("#FFFF00")},

			//para los relacionados con dibujar figuras verde
			{"DrawCircle", new Color("#1E8449")},
			{"DrawRectangle", new Color("#1E8449")},
			{"DrawLine", new Color("#1E8449")},
			{"Fill", new Color("#1E8449")},
		};

		syntaxhighlight.SymbolColor = new Color("FFFFFF");
		syntaxhighlight.NumberColor = new Color("#BD93F9");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
