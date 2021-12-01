using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class GrowingGrid<T> : IGrid<T>
{
	private T _defaultValue;

	public int UsedMinX => _grid.UsedMinX;
	public int UsedMinY => _grid.UsedMinY;
	public int UsedMaxX => _grid.UsedMaxX;
	public int UsedMaxY => _grid.UsedMaxY;
	public int MinX => _grid.MinX;
	public int MinY => _grid.MinY;
	public int MaxX => _grid.MaxX;
	public int MaxY => _grid.MaxY;
	public int FullWidth => _grid.FullWidth;
	public int FullHeight => _grid.FullHeight;
	public int UsedWidth => _grid.UsedWidth;
	public int UsedHeight => _grid.UsedHeight;

	private Grid<T> _grid;
	private int _growthIncrement;
	private bool _growsOnRead;
	private bool _growsOnWrite;

	public int GrowthTimesRight = 0;
	public int GrowthTimesLeft = 0;
	public int GrowthTimesUp = 0;
	public int GrowthTimesDown = 0;
	public int GrowthTimes => GrowthTimesRight + GrowthTimesLeft + GrowthTimesUp + GrowthTimesDown;

	int OffsetX => _grid.OffsetX;
	int OffsetY => _grid.OffsetY;

	public Point TopLeft => _grid.TopLeft;
	public Point TopRight => _grid.TopRight;
	public Point BottomLeft => _grid.BottomLeft;
	public Point BottomRight => _grid.BottomRight;
	public Point Center => _grid.Center;

	public bool XInBound(int x) => _grid.XInBound(x);
	public bool YInBound(int y) => _grid.YInBound(y);
	public bool PointInBound(Point pt) => _grid.PointInBound(pt);

	public Action<GrowingGridEvent>? OnGridGrown;

	public GrowingGrid(T defaultValue, Point xRange, Point yRange, int growthIncrement, bool growsOnRead = true, bool growsOnWrite = true)
	{
		_grid = new Grid<T>(defaultValue, xRange, yRange);
		_defaultValue = defaultValue;
		_growthIncrement = growthIncrement;
		_growsOnRead = growsOnRead;
		_growsOnWrite = growsOnWrite;
	}

	public GrowingGrid(T defaultValue, T[,] startingGrid, GridAxes axes, int growthIncrement, bool growsOnRead = true, bool growsOnWrite = true)
	{
		_defaultValue = defaultValue;
		_growthIncrement = growthIncrement;
		_growsOnRead = growsOnRead;
		_growsOnWrite = growsOnWrite;

		int xIndex = axes == GridAxes.YX ? 1 : 0;
		int yIndex = axes == GridAxes.YX ? 0 : 1;
		var xRange = new Point(0, startingGrid.GetLength(xIndex) - 1);
		var yRange = new Point(0, startingGrid.GetLength(yIndex) - 1);

		_grid = new Grid<T>(defaultValue, xRange, yRange);

		AddGrid(0, 0, startingGrid, axes);
	}

	public T this[Point key]
	{
		get { return this[key.X, key.Y]; }
		set { this[key.X, key.Y] = value; }
	}

	public T this[int x, int y]
	{
		get
		{
			var targetX = x;
			var targetY = y;
			if (_growsOnWrite)
				GrowIfNeeded(targetX, targetY);
			return _grid[targetX, targetY];
		}
		set
		{
			var targetX = x;
			var targetY = y;
			if (_growsOnWrite)
				GrowIfNeeded(targetX, targetY);
			_grid[targetX, targetY] = value;
		}
	}

	private void GrowIfNeeded(int targetX, int targetY)
	{
		var growthNeeded = false;
		int left = 0, right = 0, top = 0, bottom = 0;
		if (targetX < _grid.MinX)
		{
			growthNeeded = true;
			GrowthTimesLeft++;
			left = _growthIncrement * (int)Math.Ceiling(MathF.Abs(targetX - _grid.MinX) / _growthIncrement);
		}
		if (targetX > _grid.MaxX)
		{
			growthNeeded = true;
			GrowthTimesRight++;
			right = _growthIncrement * (int)Math.Ceiling((targetX - _grid.MaxX) * 1f / _growthIncrement);
		}
		if (targetY < _grid.MinY)
		{
			growthNeeded = true;
			GrowthTimesDown++;
			bottom = _growthIncrement * (int)Math.Ceiling(MathF.Abs(targetY - _grid.MinY) / _growthIncrement);
		}
		if (targetY > _grid.MaxY)
		{
			growthNeeded = true;
			GrowthTimesUp++;
			top = _growthIncrement * (int)Math.Ceiling((targetY - _grid.MaxY) * 1f / _growthIncrement);
		}
		if (growthNeeded)
			GrowGrid(top, right, bottom, left);
	}

	public void AddGrid(int leftX, int bottomY, T[,] grid, GridAxes axes) => _grid.AddGrid(leftX, bottomY, grid, axes);

	private void GrowGrid(int up, int right, int down, int left)
	{
		var newGrid = new Grid<T>(_defaultValue, new Point(_grid.MinX - left, _grid.MaxX + right), new Point(_grid.MinY - down, _grid.MaxY + up));

		for (int x = _grid.MinX; x <= _grid.MaxX; x++)
			for (int y = _grid.MinY; y <= _grid.MaxY; y++)
				newGrid[x, y] = _grid[x, y];

		_grid = newGrid;
		OnGridGrown?.Invoke(new GrowingGridEvent(this, up, right, down, left));
	}

	public IEnumerable<Point> Points() => _grid.Points();
	public IEnumerable<Point> AreaSquareAround(Point pt, int radiusDistance) => _grid.AreaSquareAround(pt, radiusDistance);
	public IEnumerable<Point> AreaAround(Point pt, int manhattanDistance) => _grid.AreaAround(pt, manhattanDistance);
	public IEnumerable<int> ColumnIndexs() => _grid.ColumnIndexs();
	public IEnumerable<int> RowIndexs() => _grid.RowIndexs();
	public T[,] ToArray() => _grid.ToArray();

	public struct GrowingGridEvent
	{
		public GrowingGrid<T> Grid;
		public int Up;
		public int Right;
		public int Down;
		public int Left;

		public GrowingGridEvent(GrowingGrid<T> growingGrid, int up, int right, int down, int left)
		{
			Grid = growingGrid;
			Up = up;
			Right = right;
			Down = down;
			Left = left;
		}
	}
}

