using System;
using System.IO;
using System.Collections.Generic;
using static ValueToUTFBars;

public class BlockFillBar : HeaderValue
{
	private int _width;
	private Styles _style;

	public BlockFillBar(Point position, int width, ConsoleColor color, Styles style) : base(position, color)
	{
		_width = width;
		_style = style;
	}

	protected override int TotalWidth => _width;

	public override void SetValue(int value)
	{
	}

	public override void SetValue(float value)
	{
		if (_width == 1)
			WriteValue(ValueToUTFBars.GetChar(value, _style).ToString());
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
			WriteValue(new string(chars));
		}
	}


	public override void SetValue(string value)
	{
	}

}