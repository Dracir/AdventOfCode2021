using System;
using System.Collections.Generic;
using System.Linq;

public static class Array2DUtils
{
	public static IEnumerable<Point> Range2D(int length0, int length1)
	{
		for (int i = 0; i < length0; i++)
		{
			for (int j = 0; j < length1; j++)
			{
				yield return new Point(i, j);
			}
		}
	}
}