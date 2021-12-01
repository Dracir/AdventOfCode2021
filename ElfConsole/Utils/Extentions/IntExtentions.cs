using System;


public static class IntExtentions
{
	public static int DigitCount(this int value)
	{
		return value == 0 ? 1 : (int)Math.Floor(Math.Log10(Math.Abs(value)) + 1);
	}

	public static bool Between(this int value, int min, int max) => value >= min && value <= max;


}
