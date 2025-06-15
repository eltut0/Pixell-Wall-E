using Godot;
using System;

namespace Canvas
{
	public partial class Canvas : Control
	{
		private Color[,] colors;

		public void SetColors(Color[,] colorArray)
		{
			colors = colorArray;
			QueueRedraw();
		}

		public override void _Draw()
		{
			base._Draw();

			if (colors == null)
			{
				return;
			}

			int size = colors.GetLength(0);
			float cellSize = Mathf.Min(Size.X, Size.Y) / size;

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					Rect2 rect = new(
						i * cellSize,
						j * cellSize,
						cellSize,
						cellSize
					);
					DrawRect(rect, colors[i, j]);
				}
			}
		}
	}
}