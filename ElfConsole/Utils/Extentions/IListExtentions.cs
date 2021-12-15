using System;
using System.Collections.Generic;
using System.Linq;

public static class IListExtentions
{
	public static void AddIfMissing<T>(this IList<T> list, T value)
	{
		if (!list.Contains(value))
			list.Add(value);
	}
}