using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;


public class Day15 : DayBase
{
	public override long Part1(string input)
	{
		var grid = InputParser.ParseIntGrid(input);

		var open = new List<Point>();
		var close = new HashSet<Point>();
		open.Add(new Point(0, 0));

		Dictionary<Point, int> pointsFCost = new Dictionary<Point, int>();
		Dictionary<Point, Point> pointsParent = new Dictionary<Point, Point>();
		var target = grid.TopRight;

		foreach (var item in grid.PointsAndValues())
			pointsFCost.Add(item.Point, int.MaxValue);

		pointsFCost[Point.ZERO] = 0;

		while (open.Count > 0)
		{
			var current = open.MinBy(x => pointsFCost[x]);
			open.Remove(current);
			close.Add(current);

			if (target.Equals(current))
				break;

			foreach (var neighbour in grid.AreaAround(current, 1))
			{
				if (close.Contains(neighbour))
					continue;

				var newFCost = pointsFCost[current] + grid[neighbour];
				if (pointsFCost[neighbour] > newFCost || !open.Contains(neighbour))
				{
					pointsFCost[neighbour] = newFCost;
					pointsParent[neighbour] = current;
					open.AddIfMissing(neighbour);
				}
			}
		}

		return pointsFCost[target];
	}

	public override long Part2(string input)
	{
		var grid = ParsePart2Input(input);

		var open = new List<Point>();
		var close = new HashSet<Point>();
		open.Add(new Point(0, 0));

		Dictionary<Point, int> pointsFCost = new Dictionary<Point, int>();
		Dictionary<Point, Point> pointsParent = new Dictionary<Point, Point>();
		var target = grid.TopRight;

		foreach (var item in grid.PointsAndValues())
			pointsFCost.Add(item.Point, int.MaxValue);

		pointsFCost[Point.ZERO] = 0;

		while (open.Count > 0)
		{
			var current = open.MinBy(x => pointsFCost[x]);
			open.Remove(current);
			close.Add(current);

			if (target.Equals(current))
				break;

			foreach (var neighbour in grid.AreaAround(current, 1))
			{
				if (close.Contains(neighbour))
					continue;

				pointsFCost.AddIfMissing(neighbour, 0);

				var newFCost = pointsFCost[current] + grid[neighbour];
				if (pointsFCost[neighbour] > newFCost || !open.Contains(neighbour))
				{
					pointsFCost[neighbour] = newFCost;
					pointsParent[neighbour] = current;
					open.AddIfMissing(neighbour);
				}
			}
		}

		return pointsFCost[target];
	}

	private Grid<int> ParsePart2Input(string input)
	{
		var values = InputParser.ParseInt2DArray(input, '\n', null);
		var width = values.GetLength(0);
		var grid = new Grid<int>(0, new RangeInt(0, width * 5), new RangeInt(0, width * 5));
		grid.AddGrid(0, 0, values, GridPlane.XY);
		for (int tileX = 0; tileX < 5; tileX++)
		{
			for (int tileY = 0; tileY < 5; tileY++)
			{
				if (tileX == 0 && tileY == 0)
					continue;

				for (int x = 0; x < width; x++)
					for (int y = 0; y < width; y++)
					{
						var key = new Point(tileX * width + x, tileY * width + y);

						var relatedRiskLevelKey = new Point(tileX * width + x, tileY * width + y);
						if (tileX == 0) relatedRiskLevelKey.Y -= width;
						else relatedRiskLevelKey.X -= width;
						var relatedValue = grid[relatedRiskLevelKey];

						var value = relatedValue + 1;
						grid[key] = value > 9 ? 1 : value;
					}
			}
		}
		return grid;
	}
}