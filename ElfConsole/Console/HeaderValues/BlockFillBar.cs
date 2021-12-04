using System;
using System.IO;
using System.Collections.Generic;
using static ValueToUTFBars;

public class BlockFillBar
{
	public readonly ConsoleColor _Color;

	private readonly Point _position;
	private int _width;
	private Styles _style;

	public BlockFillBar(Point position, int width, ConsoleColor color, Styles style)
	{
		_width = width;
		_style = style;
	}

	public void SetValue(float value)
	{
		if (_width == 1)
			WriteString(ValueToUTFBars.GetChar(value, _style).ToString());
		else
		{
			var chars = new Char[_width];
			var percentPerChar = 1f / (_width * 1f);
			for (int i = 1; i <= _width; i++)
			{
				var rightPercent = i * percentPerChar;
				var leftPercent = (i - 1) * percentPerChar;

				if (value >= rightPercent)
					chars[i - 1] = ValueToUTFBars.GetChar(1f, _style);
				else if (value < leftPercent)
					chars[i - 1] = ValueToUTFBars.GetChar(0, _style);
				else
					chars[i - 1] = ValueToUTFBars.GetChar((value - leftPercent) / percentPerChar, _style);
			}
			WriteString(new string(chars));
		}
	}

	private void WriteString(string value)
	{

		if (value.Length < _width)
			value = value.PadRight(_width);
		else if (value.Length > _width)
			value = value.ToString().Substring(0, _width);

		var p = ElfConsole.Position;
		ElfConsole.ForegroundColor = _Color;

		ElfConsole.WriteAt(value, _position.X, _position.Y);

		ElfConsole.Position = p;
	}

}