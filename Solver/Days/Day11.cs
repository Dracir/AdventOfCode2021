using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;

public class Day11 : DayBase
{
	public override long Part1(string input)
	{
		var values = InputParser.ParseIntGrid(input, '\n', null);
		var grid = new Grid<int>(0, new RangeInt(0, 9), new RangeInt(0, 9));
		grid.AddGrid(0, 0, values, GridAxes.XY);
		var gridRenderer = new GridRenderer<int>(x => x.ToString()[0], new RectInt(0, 0, 10, 10));
		gridRenderer.Grid = grid;
		gridRenderer._GetTileColor = (x) => x == 0 ? ConsoleColor.White : ConsoleColor.Gray;

		var flashes = 0L;
		for (int i = 0; i < 100; i++)
		{
			flashes += DoStep(grid);
			//gridRenderer.Update();
		}

		return flashes;
	}

	private int DoStep(Grid<int> grid)
	{
		foreach (var pt in grid.Points())
			grid[pt] += 1;


		var unflashed = grid.Points().ToList();
		var flashedThisItteration = new List<Point>();
		var flashed = new List<Point>();

		do
		{
			flashedThisItteration.Clear();
			foreach (var pt in unflashed)
			{
				if (grid[pt] > 9)
				{
					foreach (var adjacent in grid.AreaSquareAround(pt, 1))
						grid[adjacent] += 1;
					flashedThisItteration.Add(pt);
					flashed.Add(pt);
				}
			}
			foreach (var toRemove in flashedThisItteration)
				unflashed.Remove(toRemove);

		} while (flashedThisItteration.Count != 0);


		foreach (var toReset in flashed)
			grid[toReset] = 0;
		return flashed.Count;
	}

	public override long Part2(string input)
	{
		var values = InputParser.ParseIntGrid(input, '\n', null);
		var grid = new Grid<int>(0, new RangeInt(0, 9), new RangeInt(0, 9));
		grid.AddGrid(0, 0, values, GridAxes.XY);
		var gridRenderer = new GridRenderer<int>(x => x.ToString()[0], new RectInt(0, 0, 10, 10));
		gridRenderer.Grid = grid;
		gridRenderer._GetTileColor = (x) => x == 0 ? ConsoleColor.White : ConsoleColor.Gray;
		var steps = 0;
		var flashes = 0;
		do
		{
			steps++;
			flashes = DoStep(grid);
		} while (flashes != 100);

		return steps;
	}
}