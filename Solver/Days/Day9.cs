using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;

public class Day9 : DayBase
{
	public override long Part1(string input)
	{
		var inputGrid = InputParser.ParseIntGrid(input, '\n', null);
		var grid = new Grid<int>(0, new RangeInt(0, inputGrid.GetLength(0) - 1), new RangeInt(0, inputGrid.GetLength(1) - 1));
		grid.AddGrid(0, 0, inputGrid, GridPlane.XY);


		Dictionary<Point, Point> lowestDirection = GetLowestDirections(grid);
		List<Point> lowestPoints = GetLowestPoints(grid, lowestDirection);

		var renderer = new GridRenderer<int>(x => x.ToString()[0], new RectInt(0, 0, 100, 50));
		renderer.Grid = grid;
		renderer._GetTileColorWithPosition = (value, pt) => GetTileColorPart1(lowestPoints, value, pt);
		//renderer.Update();

		return lowestPoints.Select(x => grid[x]).Sum(x => x + 1);
	}

	public override long Part2(string input)
	{
		var inputGrid = InputParser.ParseIntGrid(input, '\n', null);
		var grid = new Grid<int>(0, new RangeInt(0, inputGrid.GetLength(0) - 1), new RangeInt(0, inputGrid.GetLength(1) - 1));
		grid.AddGrid(0, 0, inputGrid, GridPlane.XY);

		Dictionary<Point, Point> lowestDirection = GetLowestDirections(grid);

		List<Point> lowestPoints = GetLowestPoints(grid, lowestDirection);
		var basins = new Dictionary<Point, List<Point>>();
		var basinColors = new Dictionary<Point, ConsoleColor>();
		foreach (var pt in lowestPoints)
		{
			basins.Add(pt, new List<Point>());
			basinColors.Add(pt, GetRandomColor());
		}

		foreach (var pt in grid.PointsAndValues())
		{
			if (pt.Value == 9)
				continue;
			var lowestPoint = FellowPointToEnd(lowestDirection, pt.Point);
			if (!lowestPoint.HasValue)
				continue;
			basins[lowestPoint.Value].Add(pt.Point);
		}

		var renderer = new GridRenderer<int>(x => x.ToString()[0], new RectInt(0, 0, 100, 50));
		renderer.Grid = grid;
		renderer._GetTileColorWithPosition = (value, pt) => GetTileColorPart2(basins, basinColors, value, pt);
		renderer.Update();

		var sizes = basins.Select(x => x.Value.Count)
			.OrderByDescending(x => x).ToArray();
		var total = 1;
		for (int i = 0; i < 3; i++)
			total *= sizes[i];
		return total;
	}

	private ConsoleColor GetRandomColor() => new Random().Next(6) switch
	{
		0 => ConsoleColor.Red,
		1 => ConsoleColor.Yellow,
		2 => ConsoleColor.Magenta,
		3 => ConsoleColor.Green,
		4 => ConsoleColor.Blue,
		5 => ConsoleColor.Cyan,
		_ => ConsoleColor.White,
	};

	private ConsoleColor GetTileColorPart2(Dictionary<Point, List<Point>> basins, Dictionary<Point, ConsoleColor> basinColors, int value, Point pt)
	{
		var basin = basins.Where(x => x.Value.Contains(pt));
		if (basin.Count() == 1)
		{
			if (pt.Equals(basin.First().Key))
				return ConsoleColor.White;
			else
				return basinColors[basin.First().Key];
		}
		else
			return ConsoleColor.DarkGray;

	}

	// -------------------------------------------
	private ConsoleColor GetTileColorPart1(List<Point> lowestPoints, int value, Point pt)
	{
		if (lowestPoints.Contains(pt))
			return ConsoleColor.White;
		var lowestDistance = lowestPoints.Min(x => x.DistanceManhattan(pt));
		return lowestDistance switch
		{
			1 => ConsoleColor.Red,
			2 => ConsoleColor.Magenta,
			3 => ConsoleColor.Yellow,
			4 => ConsoleColor.Green,
			_ => ConsoleColor.DarkGray
		};
	}

	private Point? FellowPointToEnd(Dictionary<Point, Point> lowestDirection, Point point)
	{
		if (!lowestDirection.ContainsKey(point))
			return null;
		var next = lowestDirection[point];
		if (next.Equals(point))
			return point;
		return FellowPointToEnd(lowestDirection, next);
	}

	private void FindLowestDirectionFor(Grid<int> grid, Dictionary<Point, Point> lowestDirection, Point point)
	{
		var myValue = grid[point];
		int lowestNeighbordValue = myValue;
		Point lowestNeighbordPoint = point;
		var peeks = 0;
		var nbNeighbors = 0;

		foreach (var neighborPoint in grid.AreaAround(point, 1))
		{
			nbNeighbors++;
			var neighborValue = grid[neighborPoint];
			if (neighborValue == 9)
				peeks++;
			if (neighborValue < myValue)
			{
				lowestNeighbordValue = neighborValue;
				lowestNeighbordPoint = neighborPoint;
			}
		}
		if (peeks == nbNeighbors && myValue == 9)
			return;

		lowestDirection.Add(point, lowestNeighbordPoint);
	}

	private Dictionary<Point, Point> GetLowestDirections(Grid<int> grid)
	{
		var lowestDirection = new Dictionary<Point, Point>();
		foreach (var pt in grid.PointsAndValues())
			FindLowestDirectionFor(grid, lowestDirection, pt.Point);
		return lowestDirection;
	}

	private List<Point> GetLowestPoints(Grid<int> grid, Dictionary<Point, Point> lowestDirection)
	{
		var lowestPoints = new List<Point>();
		foreach (var pt in grid.PointsAndValues())
		{
			var lowestPoint = FellowPointToEnd(lowestDirection, pt.Point);
			if (!lowestPoint.HasValue)
				continue;

			var lowestValue = grid[lowestPoint.Value];
			if (lowestPoints.Contains(lowestPoint.Value))
				continue;

			lowestPoints.Add(lowestPoint.Value);
		}
		return lowestPoints;
	}
}