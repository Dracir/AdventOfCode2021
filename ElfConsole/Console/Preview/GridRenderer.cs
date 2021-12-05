using System;
using System.Collections.Generic;
using Console = ElfConsole;

public class GridRenderer<T>
{
	public RectInt DrawZone;
	public IGrid<T>? Grid;
	private Func<T, char> _getTilePreview;
	public Func<T, ConsoleColor>? _GetTileColor;
	public char EmptyChar = ' ';
	public Point Offset;

	private int GridUsedWidth => Grid == null ? 0 : Grid.UsedWidth;
	private int GridUsedHeight => Grid == null ? 0 : Grid.UsedHeight;

	private int GridPreviewWidth => Math.Min(GridUsedWidth, DrawZone.Width);
	private int GridPreviewHeight => Math.Min(GridUsedHeight, DrawZone.Height);

	private Grid<char> _renderingChar;
	private Grid<ConsoleColor> _renderingColor;
	private List<Point> _changedPosition = new List<Point>();

	public GridRenderer(Func<T, char> getTilePreview, RectInt drawZone)
	{
		_getTilePreview = getTilePreview;
		DrawZone = drawZone;
		Offset = Point.ZERO;
		_renderingChar = new Grid<char>(' ', drawZone.WidthRange, drawZone.HeightRange);
		_renderingColor = new Grid<ConsoleColor>(ConsoleColor.White, drawZone.WidthRange, drawZone.HeightRange);
	}

	public void Update()
	{
		ElfConsole.StackCurrentColor();
		UpdateRenderingCache();
		DrawCache();
		ElfConsole.UnStackCurrentColor();
	}

	private void UpdateRenderingCache()
	{
		if (Grid == null)
			return;

		_changedPosition.Clear();
		for (int y = 0; y < GridPreviewHeight; y++)
		{
			if (!Grid.YInBound(y + Offset.Y))
				continue;
			for (int x = 0; x < GridPreviewWidth; x++)
			{
				if (!Grid.XInBound(x + Offset.X))
					continue;
				UpdateCacheForTile(y, x, Grid[x + Offset.X, y + Offset.Y]);
			}
		}
	}

	private void UpdateCacheForTile(int y, int x, T tileValue)
	{
		var tileChar = _getTilePreview(tileValue);
		if (_renderingChar[x, y] != tileChar)
		{
			_changedPosition.Add(new Point(x, y));
			_renderingChar[x, y] = tileChar;
		}
		if (_GetTileColor != null)
		{
			var tileColor = _GetTileColor(tileValue);
			if (_renderingColor[x, y] != tileColor)
			{
				if (!_changedPosition.Contains(new Point(x, y)))
					_changedPosition.Add(new Point(x, y));
				_renderingColor[x, y] = tileColor;
			}
		}
	}

	private void DrawCache()
	{
		var changes = _changedPosition.GroupBy(p => p.Y).OrderBy(x => x.Key).ToList();
		for (int y = 0; y < GridPreviewHeight; y++)
		{
			if (!changes.Any(row => row.Key == y))
				continue;

			var changedX = changes.First(groupe => groupe.Key == y).Select(pt => pt.X).OrderBy(x => x);
			var xGroups = changedX.GroupWhile((preceding, next) => preceding + 1 == next);
			foreach (var xGroup in xGroups)
				DrawXGroup(xGroup.ToList(), y);
		}
	}

	private void DrawXGroup(List<int> xGroup, int y)
	{
		int x = xGroup.First();
		int startX = x;
		var color = _renderingColor[x, y];
		ElfConsole.ForegroundColor = color;
		var lineBuffer = "" + _renderingChar[x, y];
		while (++x <= xGroup.Last())
		{
			//TODO Skips x that are not in changedX
			if (_renderingColor[x, y] != color)
			{
				ElfConsole.WriteLineAt(lineBuffer, DrawZone.X + startX, DrawZone.Y + y);
				lineBuffer = "";
				color = _renderingColor[x, y];
				ElfConsole.ForegroundColor = color;
				startX = x;
			}
			lineBuffer += _renderingChar[x, y];
		}

		ElfConsole.WriteLineAt(lineBuffer, DrawZone.X + startX, DrawZone.Y + y);
	}
}