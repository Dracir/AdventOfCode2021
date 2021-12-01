using System;
using System.IO;
using System.Collections.Generic;

public abstract class ValuePrintBase
{
	public readonly Point Position;
	protected ConsoleColor BackgroundColor;
	protected ConsoleColor ForegroundColor;

	protected ValuePrintBase(Point position, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
	{
		Position = position;
		BackgroundColor = backgroundColor;
		ForegroundColor = foregroundColor;
	}

	protected abstract int TotalWidth { get; }

	public abstract void SetValue(int value);
	public abstract void SetValue(float value);
	public abstract void SetValue(string value);



	protected void WriteValue(string value)
	{
		var width = TotalWidth;
		var p = ElfConsole.Position;

		if (value.Length < width)
			value = value.PadRight(width);
		else if (value.Length > width)
			value = value.ToString().Substring(0, width);

		// Console.ForegroundColor = ConsoleManager.Skin.HeaderValueColor;
		ElfConsole.WriteAt(value, Position.X, Position.Y);
		Console.ResetColor();

		ElfConsole.Position = p;

	}
}