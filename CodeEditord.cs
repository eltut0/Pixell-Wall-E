using Godot;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

public partial class CodeEditord : CodeEdit
{
	[Export]
	private CodeEdit code;

	public override void _Ready()
	{
		code ??= GetNode<CodeEdit>(GlobalParameters.ProjectGlobalParameters.CodeEditorNode);

		CodeHighlighter syntaxhighlight = new();
		SyntaxHighlighter = syntaxhighlight;

		syntaxhighlight.KeywordColors = (Godot.Collections.Dictionary)new Godot.Collections.Dictionary<string, Color>()
		{

			{"GoTo", new Color("#FF79C6")},
			{"_", new Color("#FF79C6")},

			//yellow for all related with walle
			{ "Spawn", new Color("#FFFF00")},
			{ "ReSpawn", new Color("#FFFF00")},
			{ "Color", new Color("#FFFF00")},
			{"Size", new Color("#FFFF00")},

			//green for the drawing functions
			{"DrawCircle", new Color("#1E8449")},
			{"DrawRectangle", new Color("#1E8449")},
			{"DrawLine", new Color("#1E8449")},
			{"Fill", new Color("#1E8449")},
			{"DrawPixel", new Color("#1E8449")},

			//blue for functions
			{ "GetActualX", new Color("#4A6FA5")},
			{"GetActualY", new Color("#4A6FA5")},
			{"GetCanvasSize", new Color("#4A6FA5")},
			{"GetColorCount", new Color("#4A6FA5")},

			//red for boolean operators
			{"IsBrushColor", new Color("#8B0000")},
			{"IsBrushSize", new Color("#8B0000")},
			{"IsCanvasColor", new Color("#8B0000")},

			//candela 
			{"candela", new Color("#FFA500")},
		};

		syntaxhighlight.SymbolColor = new Color("#FF79C6");
		syntaxhighlight.NumberColor = new Color("#BD93F9");
		syntaxhighlight.AddColorRegion("\"", "\"", new Color("#CE9178"));
		syntaxhighlight.AddColorRegion("#", "#", new Color("#666666"));
	}

	public string[] GetCode()
	{
		string[] returnable = code.Text.Split('\n');
		return [.. returnable.Where(line => !string.IsNullOrWhiteSpace(line))];
	}
}