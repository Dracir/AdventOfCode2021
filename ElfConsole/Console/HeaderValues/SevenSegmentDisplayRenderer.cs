using System;
using System.IO;
using System.Collections.Generic;

public class SevenSegmentDisplayRenderer
{
	private readonly int _x;
	private readonly int _y;
	private readonly int _horizontalWidth;
	private readonly int _verticalSegmentHeight;
	private readonly ConsoleColor onColor;
	private readonly ConsoleColor offCcolor;

	//public static readonly char HORIZONTAL = '━';
	//public static readonly char VERTICAL = '┃';
	public static readonly char HORIZONTAL = '━';
	public static readonly char VERTICAL = '┃';

	public char _TopSegmentChar = HORIZONTAL;
	public char _TopLeftSegmentChar = VERTICAL;
	public char _TopRightSegmentChar = VERTICAL;
	public char _MiddleSegmentChar = HORIZONTAL;
	public char _BottomLeftSegmentChar = VERTICAL;
	public char _BottomRightSegmentChar = VERTICAL;
	public char _BottomSegmentChar = HORIZONTAL;

	public SevenSegmentDisplayRenderer(Point position, int horizontalWidth, int verticalSegmentHeight, ConsoleColor onCcolor, ConsoleColor offCcolor)
	{
		_x = position.X;
		_y = position.Y;
		this._horizontalWidth = horizontalWidth;
		this._verticalSegmentHeight = verticalSegmentHeight;
		this.onColor = onCcolor;
		this.offCcolor = offCcolor;
	}

	public void SetValue(int value)
	{
		var on = value >= 0 && value <= 9 ? onColor : offCcolor;
		ElfConsole.ForegroundColor = new int[] { 1, 4 }.Contains(value) ? offCcolor : on;
		DrawLine(_TopSegmentChar, _x + 1, _y);

		var y = _y + 1;
		for (int i = 0; i < _verticalSegmentHeight; i++)
		{
			ElfConsole.ForegroundColor = new int[] { 1, 2, 3, 7 }.Contains(value) ? offCcolor : on;
			DrawChar(_TopLeftSegmentChar, _x, y + i);

			ElfConsole.ForegroundColor = new int[] { 5, 6 }.Contains(value) ? offCcolor : on;
			DrawChar(_TopRightSegmentChar, _x + 1 + _horizontalWidth, y + i);
		}

		y += _verticalSegmentHeight;
		ElfConsole.ForegroundColor = new int[] { 0, 1, 7 }.Contains(value) ? offCcolor : on;
		DrawLine(_MiddleSegmentChar, _x + 1, y);

		y += 1;
		for (int i = 0; i < _verticalSegmentHeight; i++)
		{
			ElfConsole.ForegroundColor = new int[] { 1, 3, 4, 5, 7, 9 }.Contains(value) ? offCcolor : on;
			DrawChar(_BottomLeftSegmentChar, _x, y + i);

			ElfConsole.ForegroundColor = new int[] { 2 }.Contains(value) ? offCcolor : on;
			DrawChar(_BottomRightSegmentChar, _x + 1 + _horizontalWidth, y + i);
		}

		y += _verticalSegmentHeight;
		ElfConsole.ForegroundColor = new int[] { 1, 4, 7 }.Contains(value) ? offCcolor : on;
		DrawLine(_BottomSegmentChar, _x + 1, y);
	}

	private void DrawLine(char c, int x, int y) => ElfConsole.WriteLineAt(new string(c, _horizontalWidth), x, y);
	private void DrawChar(char c, int x, int y) => ElfConsole.WriteCharAt(c, x, y);
}