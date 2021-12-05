using System;
using System.Collections.Generic;


public class HexPreview<T>
{
	/*public RectInt Viewport;
	public HexMap<T> HexMap;
	private Func<T, char> _getTilePreview;
	public Func<T, ConsoleColor> GetTileColor;
	public char EmptyChar = ' ';
	public Point Offset;
	public bool ReverseY;

	private int GridUsedWidth => HexMap == null ? 0 : HexMap.Width;
	private int GridUsedHeight => HexMap == null ? 0 : HexMap.Height;

	private int GridPreviewWidth => Math.Min(GridUsedWidth, Viewport.Width);
	private int GridPreviewHeight => Math.Min(GridUsedHeight, Viewport.Height);

	public HexPreview(Func<T, char> getTilePreview, RectInt viewport)
	{
		_getTilePreview = getTilePreview;
		Viewport = viewport;
	}

	public void Update()
	{
		var p = BetterConsole.Position;
		Console.ForegroundColor = ConsoleManager.Skin.HeaderValueColor;

		if (HexMap != null)
			for (int y = 0; y < Viewport.Height; y++)
			{
				var drawRow = ReverseY ? Viewport.Height - y - 1 : y;
				//BetterConsole.WriteAt(new string(' ', Viewport.Width), Viewport.X, drawRow + Viewport.Y);
				if (HexMap.YInBound(y + Offset.Y))
				{
					var oddLine = y % 2 == 0;
					if (GetTileColor == null)
					{
						var line = "";
						for (int x = 0; x < Viewport.Width / 2; x++)
						{
							if (HexMap.XInBound(x + Offset.X))
								line += _getTilePreview(HexMap[x + Offset.X, y + Offset.Y]);
							else
								line += EmptyChar;
						}
						BetterConsole.WriteAt(line, Viewport.X, drawRow + Viewport.Y);
					}
					else
					{
						var oddOffset = -(y % 2);
						for (int col = 0; col < Viewport.Width / 2; col++)
						{
							if (HexMap.XInBound(col + Offset.X))
							{
								var tile = HexMap[col + Offset.X, y + Offset.Y];
								Console.ForegroundColor = GetTileColor(tile);
								BetterConsole.WriteAt(_getTilePreview(tile), 1 + oddOffset + col * 2 + Viewport.X, drawRow + Viewport.Y);
							}
							else
								BetterConsole.WriteAt(EmptyChar, 1 + oddOffset + col * 2 + Viewport.X, drawRow + Viewport.Y);
						}
					}
				}
				else
				{
					BetterConsole.WriteAt("", Viewport.X, drawRow + Viewport.Y);
				}

			}

		Console.ResetColor();
		BetterConsole.Position = p;
	}*/
}