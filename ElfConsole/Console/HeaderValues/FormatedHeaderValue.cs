using System;
using System.IO;
using System.Collections.Generic;

public class FormatedHeaderValue : HeaderValue
{
	private int Width;
	private string? Format;

	public FormatedHeaderValue(Point position, int width, ConsoleColor color, string? format = null) : base(position, color)
	{
		Width = width;
		Format = format;
	}

	protected override int TotalWidth => Width;

	public override void SetValue(int value)
	{
		if (Format != null)
			WriteValue(value.ToString(Format));
		else
			WriteValue(FormatDefault(value.ToString()));
	}

	public override void SetValue(float value)
	{
		if (Format != null)
			WriteValue(value.ToString(Format));
		else
			WriteValue(FormatDefault(value.ToString()));
	}

	public override void SetValue(string value)
	{
		if (Format != null)
			WriteValue(string.Format(Format, value));
		else
			WriteValue(FormatDefault(value));
	}

	private string FormatDefault(string value)
	{
		if (value.Length > Width)
			return value.ToString().Substring(0, Width);
		else
			return value;
	}

}