using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;

public class Day5 : DayBase
{
	private static Point offset = new Point(400, 475);
	// private static Point offset = new Point(0, 0);
	public override long Part1(string input)
	{
		var lines = InputParser.ListOfLines(input, '\n', ',', " -> ");
		var size = new Point(1000, 1000);
		var grid = new Grid<int>(0, new RangeInt(0, size.X), new RangeInt(0, size.Y));
		var renderer = new GridRenderer<int>(GetPreviewChar, new RectInt(0, 0, 211, 51));
		renderer.Offset = offset;
		renderer.Grid = grid;

		var horizontalVerticalLines = lines.Where(x => x.IsHorizontal || x.IsVertical);
		foreach (var line in horizontalVerticalLines)
		{
			grid.ApplyLine(line, AddValue);
			renderer.Update();
		}

		return grid.PointsAndValues().Count(x => x.Value >= 2);
	}

	public override long Part2(string input)
	{
		var lines = InputParser.ListOfLines(input, '\n', ',', " -> ");
		var size = new Point(1000, 1000);
		// var size = new Point(10, 10);
		var grid = new Grid<int>(0, new RangeInt(0, size.X), new RangeInt(0, size.Y));
		var preview = new GridRenderer<int>(GetPreviewChar, new RectInt(0, 0, 211, 51));
		// var preview = new GridRenderer<int>(GetPreviewChar, new RectInt(11, 11, 10, 10));
		preview.Offset = offset;
		preview.Grid = grid;
		preview._GetTileColor = GetTileColor;

		foreach (var line in lines)
		{
			grid.ApplyLine(line, AddValue);
			preview.Update();
		}

		return grid.PointsAndValues().Count(x => x.Value >= 2);
	}

	private ConsoleColor GetTileColor(int arg) => arg switch
	{
		0 => ConsoleColor.Gray,
		1 => ConsoleColor.DarkBlue,
		2 => ConsoleColor.DarkRed,
		3 => ConsoleColor.DarkGreen,
		4 => ConsoleColor.Blue,
		5 => ConsoleColor.Red,
		6 => ConsoleColor.Green,
		_ => ConsoleColor.White,
	};

	private char GetPreviewChar(int value) => value == 0 ? '.' : value <= 9 ? value.ToString()[0] : '!';
	private int AddValue((int currentValue, Point position) p) => p.currentValue + 1;

}