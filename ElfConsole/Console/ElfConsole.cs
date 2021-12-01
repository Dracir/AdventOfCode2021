
using System;

public static class ElfConsole
{
	public static int Width { get { return Console.WindowWidth; } }
	public static int Height { get { return Console.WindowHeight - 1; } }
	public static int Right { get { return Console.WindowWidth - 1; } }
	public static int Bottom { get { return Console.WindowHeight - 2; } }

	private static int WriteLeft { get { return 0; } }
	private static int WriteRight { get { return Width - 0; } }
	private static int WriteTop { get { return 0; } }
	private static int WriteHeight { get { return Height - WriteTop; } }
	private static int WriteWidth { get { return Width; } }

	public static Point Position
	{
		get { return new Point(Console.CursorLeft, Console.CursorTop); }
		set
		{
			Console.CursorLeft = Math.Clamp(value.X, 0, Right);
			Console.CursorTop = Math.Clamp(value.Y, 0, Bottom);
		}
	}

	public static ConsoleColor ForegroundColor
	{
		get => Console.ForegroundColor;
		set => Console.ForegroundColor = value;
	}

	public static void ResetColor() => Console.ResetColor();

	public static void WriteAtLine(string value, int line, int linesWidth = 0) => WriteAt(value, 0, line, linesWidth);

	public static void WriteAt(string value, int x, int y, int linesWidth = 0)
	{
		if (value.Contains("\n"))
		{
			foreach (var line in value.Split("\n"))
			{
				var lineStr = line;
				if (linesWidth != 0)
					lineStr = lineStr.PadRight(linesWidth);
				y += WriteLine(lineStr, x, y);
			}
		}
		else
		{
			if (linesWidth != 0)
				WriteLine(value.PadRight(linesWidth), x, y);
			else
				WriteLine(value, x, y);
		}
	}

	public static void WriteAt(char value, int x, int y)
	{
		Position = new Point(WriteLeft + x, WriteTop + y);
		Console.Write(value);
	}

	private static int WriteLine(string value, int x, int y)
	{
		int lines = 1;
		foreach (var c in value)
		{
			if (y >= WriteHeight)
				return lines;
			if (x >= WriteWidth)
			{
				lines++;
				y++;
				x = 0;
			}
			Position = new Point(WriteLeft + x, WriteTop + y);
			Console.Write(c);
			x++;
		}

		return lines;
	}
	public static ConsoleKeyInfo ReadKey() => Console.ReadKey();
	public static void Clear() => Console.Clear();

	public static void SetTitle(int day, string title, int part)
	{
		//Console.ForegroundColor = ConsoleManager.Skin.FramesColor;
		var line = new String('=', Width - 2); // ═ slow

		ForegroundColor = ConsoleColor.Gray;
		WriteAtLine($"╔{line}╗", Height - 4);
		for (int y = Height - 3; y < Height; y++)
		{
			WriteAt("║", 0, y);
			WriteAt("║", Width - 1, y);

		}

		ForegroundColor = ConsoleColor.White;
		var titleText = $" Day {day}: {title} - Part {part} ";
		WriteAt(titleText, 2, Height - 4);

		Console.ResetColor();
	}
}