using System;
using System.Collections.Generic;
using System.Linq;

public static class IDictionnaryExtentions
{
	public static void AddIfMissing<T, V>(this IDictionary<T, V> dictionnary, T key, V value)
	{
		if (!dictionnary.ContainsKey(key))
			dictionnary[key] = value;
	}

}