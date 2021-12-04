
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

	private static ConsoleColor CurrentForegroundColor;
	public static ConsoleColor ForegroundColor
	{
		get => Console.ForegroundColor;
		set
		{
			if (CurrentForegroundColor == value)
				return;
			Console.ForegroundColor = value;
			CurrentForegroundColor = value;
		}
	}


	private static ConsoleColor CurrentBackgroundColor;
	public static ConsoleColor BackgroundColor
	{
		get => Console.BackgroundColor;
		set
		{
			if (CurrentBackgroundColor == value)
				return;
			Console.BackgroundColor = value;
			CurrentBackgroundColor = value;
		}
	}

	public static void ResetColor()
	{
		BackgroundColor = ConsoleColor.Black;
		ForegroundColor = ConsoleColor.White;
	}

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
				WriteLine(lineStr, x, y++);
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

	private static void WriteLine(string value, int x, int y)
	{
		var text = value;
		if (x >= WriteRight)
			return;
		if (x + text.Length > WriteRight)
			text = text.Substring(WriteRight - x - 1) + '…';

		Position = new Point(x, y);
		Console.WriteLine(text);

	}
	public static ConsoleKeyInfo ReadKey() => Console.ReadKey();
	public static void Clear() => Console.Clear();

	public static void SetTitle(int day, string title, int part)
	{
		//Console.ForegroundColor = ConsoleManager.Skin.FramesColor;
		var line = new String('═', Width - 2); // ═ slow

		ForegroundColor = ConsoleColor.Gray;
		WriteAtLine($"╔{line}╗", Height - 3);
		for (int y = Height - 2; y < Height; y++)
		{
			WriteAt("║", 0, y);
			WriteAt("║", Width - 1, y);

		}

		ForegroundColor = ConsoleColor.White;
		var titleText = $" Day {day}: {title} - Part {part} ";
		WriteAt(titleText, 2, Height - 3);

		Console.ResetColor();
	}
}