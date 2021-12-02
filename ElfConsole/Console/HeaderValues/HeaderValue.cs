using System;
using System.IO;
using System.Collections.Generic;

public abstract class HeaderValue
{
	public readonly Point Position;
	protected ConsoleColor Color;

	protected HeaderValue(Point position, ConsoleColor color)
	{
		Position = position;
		Color = color;
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

		ElfConsole.ForegroundColor = Color;
		ElfConsole.WriteAt(value, Position.X, Position.Y);

		ElfConsole.Position = p;

	}
}