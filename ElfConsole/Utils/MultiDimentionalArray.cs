using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class MultiDimentionalArray<Value>
{
	private Dictionary<int[], Value?> _values = new Dictionary<int[], Value?>(new ArrayByValueComparer());
	public Value DefaultValue;

	private int[] _minKeyValues;
	private int[] _maxKeyValues;

	public bool CreateOnGet;

	public int[] Size
	{
		get
		{
			var size = new int[_minKeyValues.Length];
			for (int i = 0; i < _minKeyValues.Length; i++)
				size[i] = _maxKeyValues[i] - _minKeyValues[i] + 1;
			return size;
		}
	}

	public bool KeyExist(int[] key) => _values.ContainsKey(key);

	public MultiDimentionalArray(Value defaultValue, int startingDimentions)
	{
		DefaultValue = defaultValue;
		_minKeyValues = new int[startingDimentions];
		_maxKeyValues = new int[startingDimentions];
	}

	public int[] MinKeys => _minKeyValues;
	public int[] MaxKeys => _maxKeyValues;

	public IEnumerable<(int[] Key, Value? Value)> PointsAndValues() => PointsAndValues(MinKeys, MaxKeys);
	public IEnumerable<(int[] Key, Value? Value)> PointsAndValues(int[] minIndex, int[] maxIndex)
	{
		var nbKeys = minIndex.Length;
		var widths = new int[nbKeys];
		var widthCummulative = new int[nbKeys];

		for (int i = 0; i < nbKeys; i++)
			widths[i] = maxIndex[i] - minIndex[i] + 1;

		widthCummulative[nbKeys - 1] = 1;
		for (int i = nbKeys - 2; i >= 0; i--)
			widthCummulative[i] = widthCummulative[i + 1] * widths[i + 1];

		var totalWidth = widths.Product(x => x);
		var key = new int[nbKeys];
		for (int id = 0; id < totalWidth; id++)
		{
			var idRemainder = id;
			for (int i = 0; i < nbKeys - 1; i++)
			{
				var value = (int)(idRemainder / widthCummulative[i]);
				idRemainder -= value * widthCummulative[i];
				key[i] = minIndex[i] + value;
			}
			key[nbKeys - 1] = minIndex[nbKeys - 1] + idRemainder;
			yield return ((int[])key.Clone(), this[key]);
		}
	}

	public IEnumerable<(int[] Key, Value? Value)> AreaSquareAround(int[] pt, int radiusDistance)
	{
		var nbKeys = _minKeyValues.Length;
		var minIndex = pt.Select(x => x - radiusDistance).ToArray();
		var maxIndex = pt.Select(x => x + radiusDistance).ToArray();
		foreach (var value in PointsAndValues(minIndex, maxIndex))
			yield return value;
	}


	protected virtual Value? this[int[] key]
	{
		get
		{
			if (_values.ContainsKey(key))
				return _values[key];
			else if (CreateOnGet)
			{
				this[key] = DefaultValue;
				return _values[key];
			}
			else
				return DefaultValue;
		}
		set
		{
			_values[key] = value;
			for (int i = 0; i < key.Length; i++)
			{
				if (key[i] < _minKeyValues[i])
					_minKeyValues[i] = key[i];
				else if (key[i] > _maxKeyValues[i])
					_maxKeyValues[i] = key[i];
			}
		}
	}
}


public class ArrayByValueComparer : IEqualityComparer<int[]>
{
	public bool Equals(int[]? x, int[]? y) => AreEquals(x, y);
	public static bool AreEquals(int[]? x, int[]? y)
	{
		if (x == null && y == null)
			return true;
		if (x == null || y == null)
			return false;
		if (x.Length != y.Length)
			return false;

		for (int i = 0; i < x.Length; i++)
		{
			if (x[i] != y[i])
				return false;
		}
		return true;
	}



	public int GetHashCode(int[] obj)
	{
		int result = 17;
		for (int i = 0; i < obj.Length; i++)
		{
			unchecked
			{
				result = result * 23 + obj[i];
			}
		}
		return result;
	}
}