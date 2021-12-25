using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;


public class Day25 : DayBase
{
	public override long Part1(string input)
	{
		var grid = Parse(input);
		int step = 0;
		var renderer = new GridRenderer<char>(x => x, new RectInt(0, 0, 100, 70));
		renderer.Grid = grid;
		renderer._GetTileColor = (x) => ConsoleColor.Gray;
		//renderer.Update();

		var changed = true;
		while (changed)
		{
			step++;
			changed = false;

			var change = GetGameOfLifeStepChanges(grid, '>');
			ApplyChanges(grid, change);
			changed |= change.Count > 0;

			change = GetGameOfLifeStepChanges(grid, 'v');
			ApplyChanges(grid, change);
			changed |= change.Count > 0;
			//renderer.Update();
		}

		return step;
	}

	private void ApplyChanges(Grid<char> grid, Dictionary<Point, char> changes)
	{
		foreach (var change in changes)
			grid[change.Key] = change.Value;
	}

	public static Dictionary<Point, char> GetGameOfLifeStepChanges(Grid<char> array, char herd)
	{
		var changes = new Dictionary<Point, char>();
		var neighbour = Point.ZERO;

		foreach (var pt in array.PointsAndValues())
		{
			if (pt.Value != herd) continue;

			if (pt.Value == '>')
			{
				if (pt.Point.X < array.MaxX)
					neighbour = new Point(pt.Point.X + 1, pt.Point.Y);
				else
					neighbour = new Point(0, pt.Point.Y);
				if (array[neighbour] == '.')
				{
					changes.Add(neighbour, '>');
					changes.Add(pt.Point, '.');
				}
			}
			else if (pt.Value == 'v')
			{
				if (pt.Point.Y < array.MaxY)
					neighbour = new Point(pt.Point.X, pt.Point.Y + 1);
				else
					neighbour = new Point(pt.Point.X, 0);
				if (array[neighbour] == '.')
				{
					changes.Add(neighbour, 'v');
					changes.Add(pt.Point, '.');
				}
			}
		}

		return changes;
	}

	private Grid<char> Parse(string input)
	{
		var charArray = InputParser.ParseChar2DArray(input, '\n');
		var grid = new Grid<char>('.', new RangeInt(0, charArray.GetLength(0) - 1), new RangeInt(0, charArray.GetLength(1) - 1));
		grid.AddGrid(0, 0, charArray, GridPlane.XY);
		return grid;
	}

	public override long Part2(string input)
	{
		return 0;
	}
}
