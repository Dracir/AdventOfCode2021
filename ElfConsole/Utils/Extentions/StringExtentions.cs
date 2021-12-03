using System;


public static class StringExtentions
{

	public static IEnumerable<int> CountCharPerCol(this IEnumerable<string> values, char charToCount)
	{
		var counts = new int[values.First().Length];

		foreach (var line in values)
		{
			for (int i = 0; i < line.Length; i++)
				if (line[i] == charToCount)
					counts[i]++;
		}
		return counts.Reverse();
	}


}
