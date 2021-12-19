using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using System;

public static class TestUtility
{
	// -------------------------------------------

	public static void AreBetweenExclusive(Point from, Point to, Point actual)
	{
		if (actual.X < from.X || actual.X > to.X)
			Assert.Fail($"Not between, it's outside X\nExpected between:\t{from} and {to}\nBut was:\t{actual}");
		else if (actual.Y < from.Y || actual.Y > to.Y)
			Assert.Fail($"Not between, it's outside Y\nExpected between:\t{from} and {to}\nBut was:\t{actual}");
		else if ((actual.X > from.X && actual.X < to.X) || (actual.Y > from.Y && actual.Y < to.Y))
			Assert.Pass();
		else
			Assert.Fail($"Not between\nExpected between:\t{from} and {to}\nBut was:\t{actual}");
	}

	public static void AreBetweenInclusive(Point from, Point to, Point actual)
	{
		if (actual.X >= from.X && actual.X <= to.X && actual.Y >= from.Y && actual.Y <= to.Y)
			Assert.Pass();
		else
			Assert.Fail($"Not between\nExpected between:\t{from} and {to}\nBut was:\t{actual}");
	}

	public static void AreEquals(Point[,] expected, Point[,] actual, float epsilon)
	{

		if (expected.GetLength(0) != actual.GetLength(0) || expected.GetLength(1) != actual.GetLength(1))
		{
			Assert.Fail($"Size differ\nExpected:\t[{expected.GetLength(0)},{expected.GetLength(1)}]\nBut was:\t[{actual.GetLength(0)},{actual.GetLength(1)}]");
			return;
		}


		for (int x = 0; x < expected.GetLength(0); x++)
			for (int y = 0; y < expected.GetLength(1); y++)
			{
				if (Math.Abs(expected[x, y].X - actual[x, y].X) >= epsilon || Math.Abs(expected[x, y].Y - actual[x, y].Y) >= epsilon)
				{
					Assert.Fail($"Expected and actual are both <UnityEngine.Vector2[{expected.GetLength(0)},{expected.GetLength(1)}]>\n"
										+ $"Values differ at index [{x},{y}]\nExpected:\t{expected[x, y].X},{expected[x, y].Y}\nBut was:\t{actual[x, y].X},{actual[x, y].Y}");
				}

			}
	}
	public static void ContainsSameInAnyOrder<T>(IEnumerable<T> expectedList, IEnumerable<T> actualList, Comparer<T> comparer)
	{
		var remainingToFind = expectedList.ToList();
		var unknonwn = new List<T>();
		foreach (var actual in actualList)
		{
			var found = false;
			foreach (var expected in remainingToFind)
			{
				//Debug.Log($"Compare {actual}, {expected} = {comparer.Compare(actual, expected) }");
				if (comparer.Compare(actual, expected) == 0)
				{
					remainingToFind.Remove(expected);
					found = true;
					break;
				}
			}
			if (!found)
				unknonwn.Add(actual);
		}

		if (remainingToFind.Count != 0 || unknonwn.Count != 0)
		{
			var sizeTxt = (expectedList.Count() == actualList.Count()) ?
				$"Expected and actual are both <IEnumerable.T[{expectedList.Count()}]>\n" :
				$"Size differ, Expected: [{expectedList.Count()}], But was: [{actualList.Count()}]\n";
			var missingTxt = string.Join(", ", remainingToFind.Select(x => x == null ? "null" : x.ToString()));
			var unknownText = string.Join(", ", unknonwn.Select(x => x == null ? "null" : x.ToString()));
			Assert.Fail(sizeTxt
						+ $"Missing values: {missingTxt}\n"
						+ $"Unknown values: {unknownText}\n");
		}
	}

	/*public static void ContainsSameInAnyOrder(IEnumerable<Point> expectedList, IEnumerable<Point> actualList, float epsilon = float.Epsilon)
	{
		if (expectedList.Count() != actualList.Count())
		{
			Assert.Fail($"Size differ\nExpected:\t[{expectedList.Count()}]\nBut was:\t[{actualList.Count()}]");
			return;
		}

		var remainingToFind = expectedList.ToList();
		var unknonwn = new List<Point>();
		foreach (var actual in actualList)
		{
			var found = false;
			foreach (var expected in remainingToFind)
			{
				if (Approximately(actual, expected, epsilon))
				{
					remainingToFind.Remove(expected);
					found = true;
					break;
				}
			}
			if (!found)
				unknonwn.Add(actual);
		}

		if (remainingToFind.Count != 0 || unknonwn.Count != 0)
		{
			var missingTxt = string.Join(", ", remainingToFind.Select(x => x.ToString()));
			var unknownText = string.Join(", ", unknonwn.Select(x => x.ToString()));
			Assert.Fail($"Expected and actual are both <UnityEngine.Vector2[{expectedList.Count()}]>\n"
						+ $"Missing values: {missingTxt}\n"
						+ $"Unknown values: {unknownText}\n");
		}
	}*/

	/*public static void AreDictionariesEqual<TKey, TValue>(Dictionary<TKey, TValue> expected, Dictionary<TKey, TValue> actual)
	{
		var dc = new DictionaryComparer<TKey, TValue>();
		if (dc.Equals(expected, actual))
			Assert.Pass();

		var expectedValues = string.Join(", ", expected.Select(x => $"{{{x.Key},{x.Value}}}"));
		var actualValues = string.Join(", ", actual.Select(x => $"{{{x.Key},{x.Value}}}"));
		Assert.Fail($"Expected values: {expectedValues}\n"
					+ $"Actual values: {actualValues}\n");
	}

	public class DictionaryComparer<TKey, TValue> :
			IEqualityComparer<Dictionary<TKey, TValue>>
	{
		private IEqualityComparer<TValue> valueComparer;
		public DictionaryComparer(IEqualityComparer<TValue> valueComparer = null)
		{
			this.valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
		}
		public bool Equals(Dictionary<TKey, TValue> x, Dictionary<TKey, TValue> y)
		{
			if (x.Count != y.Count)
				return false;
			if (x.Keys.Except(y.Keys).Any())
				return false;
			if (y.Keys.Except(x.Keys).Any())
				return false;
			foreach (var pair in x)
				if (!valueComparer.Equals(pair.Value, y[pair.Key]))
					return false;
			return true;
		}

		public int GetHashCode(Dictionary<TKey, TValue> obj)
		{
			throw new NotImplementedException();
		}
	}*/

}