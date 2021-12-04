using System;
using System.IO;
using System.Collections.Generic;

public class FormatedPrintedValue
{
	public readonly ConsoleColor _Color;

	private readonly Point _position;
	private int _width;
	private string? Format;

	public FormatedPrintedValue(Point position, int width, ConsoleColor color, string? format = null)
	{
		this._position = position;
		_width = width;
		this._Color = color;
		Format = format;
	}


	public void SetValue(int value)
	{
		if (Format != null)
			WriteString(value.ToString(Format));
		else
			WriteString(FormatDefault(value.ToString()));
	}

	public void SetValue(float value)
	{
		if (Format != null)
			WriteString(value.ToString(Format));
		else
			WriteString(FormatDefault(value.ToString()));
	}

	public void SetValue(string value)
	{
		if (Format != null)
			WriteString(string.Format(Format, value));
		else
			WriteString(FormatDefault(value));
	}

	private string FormatDefault(string value)
	{
		if (value.Length > _width)
			return value.ToString().Substring(0, _width - 1) + '…';
		else
			return value;
	}

	private void WriteString(string value)
	{

		if (value.Length < _width)
			value = value.PadRight(_width);
		else if (value.Length > _width)
			value = value.ToString().Substring(0, _width - 1) + '…';

		var p = ElfConsole.Position;
		ElfConsole.ForegroundColor = _Color;

		ElfConsole.WriteAt(value, _position.X, _position.Y);

		ElfConsole.Position = p;
	}

}