using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;

public class Day13 : DayBase
{
	public override long Part1(string inputStr)
	{
		(var grid, var instructions) = ParseInput(inputStr);
		var renderer = new GridRenderer<int>(GetTileCharacter, new RectInt(0, 0, 100, 70));
		renderer.Grid = grid;
		renderer._GetTileColor = getColor;
		renderer.Update();

		var fold = instructions.First();
		if (fold.Item1 == GridAxe.Y)
			DoYFold(grid, fold.Item2);
		else if (fold.Item1 == GridAxe.X)
			DoXFold(grid, fold.Item2);
		renderer.Update();

		return grid.PointsAndValues().Count(x => x.Value == 1);
	}

	public override long Part2(string inputStr)
	{
		(var grid, var instructions) = ParseInput(inputStr);
		var renderer = new GridRenderer<int>(GetTileCharacter, new RectInt(0, 0, 100, 70));
		renderer.Grid = grid;
		renderer._GetTileColor = getColor;
		renderer.Update();

		foreach (var fold in instructions)
		{
			if (fold.Item1 == GridAxe.Y)
				DoYFold(grid, fold.Item2);
			else if (fold.Item1 == GridAxe.X)
				DoXFold(grid, fold.Item2);
			renderer.Update();
		}

		return grid.PointsAndValues().Count(x => x.Value == 1);
	}



	private static void DoXFold(Grid<int> grid, int foldY)
	{
		foreach (var pt in grid.Points())
		{
			if (pt.X > foldY && grid[pt] == 1)
			{
				grid[foldY - (pt.X - foldY), pt.Y] = 1;
				grid[pt] = 0;
			}
		}
		for (int y = 0; y < grid.UsedHeight; y++)
			grid[foldY, y] = -2;
	}

	private static void DoYFold(Grid<int> grid, int foldY)
	{
		foreach (var pt in grid.Points())
		{
			if (pt.Y > foldY && grid[pt] == 1)
			{
				grid[pt.X, foldY - (pt.Y - foldY)] = 1;
				grid[pt] = 0;
			}
		}
		for (int x = 0; x < grid.UsedWidth; x++)
			grid[x, foldY] = -1;
	}

	private char GetTileCharacter(int arg) => arg switch
	{
		-2 => '|',
		-1 => '-',
		0 => '.',
		1 => '#',
		_ => ' ',
	};

	private ConsoleColor getColor(int arg)
	{
		return ConsoleColor.Gray;
	}

	private (Grid<int> grid, List<(GridAxe, int)> instructions) ParseInput(string inputStr)
	{
		var split = inputStr.Split("\n\n");
		var pts = InputParser.ListOfPoints(split[0], '\n', ',');
		var maxX = pts.Max(pt => pt.X);
		var maxY = pts.Max(pt => pt.Y);

		var grid = new Grid<int>(0, new RangeInt(0, maxX), new RangeInt(0, maxY));
		foreach (var pt in pts)
			grid[pt] = 1;

		var instructions = split[1].Split("\n")
			.Select(Line => Line.Split('='))
			.Select(splited => (splited[0].Last() == 'x' ? GridAxe.X : GridAxe.Y, int.Parse(splited[1])))
			.ToList();

		return (grid, instructions);
	}
}