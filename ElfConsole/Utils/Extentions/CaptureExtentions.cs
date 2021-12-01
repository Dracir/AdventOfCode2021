using System;
using System.Text.RegularExpressions;

public static class CaptureExtentions
{
	public static string StringValue(this GroupCollection gc, int index)
	{
		return gc[index].Value;
	}
	public static int IntValue(this GroupCollection gc, int index)
	{
		return Int32.Parse(gc[index].Value);
	}
	public static long LongValue(this GroupCollection gc, int index)
	{
		return Int64.Parse(gc[index].Value);
	}

}