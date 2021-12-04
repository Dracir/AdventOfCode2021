using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class InputParser
{
	// Summary:
	//		List of ints either lines or 1 line separated with ,
	public static int[] ListOfInts(string input)
	{
		if (input.Contains("\n"))
			return ListOfInts(input, '\n');
		else
			return ListOfInts(input, ',');
	}
	public static long[] ListOfLong(string input)
	{
		input = input.Replace("\r", "");
		if (input.Contains("\n"))
			return ListOfLong(input, '\n');
		else
			return ListOfLong(input, ',');
	}
	public static string[] ListOfStrings(string input)
	{
		if (input.Contains("\n"))
			return ListOfStrings(input, '\n');
		else
			return ListOfStrings(input, ',');
	}
	public static string[] ListOfStrings(string input, char separator) => input.Split(separator).ToArray();

	public static int[] ListOfDigitNoSeparator(string input) => input.Select(x => int.Parse(x.ToString())).ToArray();
	public static int[] ListOfInts(string input, char separator) => input.Split(separator).Select(x => int.Parse(x)).ToArray();

	public static long[] ListOfLong(string input, char separator) => input.Split(separator).Select(x => long.Parse(x)).ToArray();

	/* a range in the format of "number-number" for exemple 130254-678275*/
	public static RangeInt ParseRangeInt(string input)
	{
		var split = input.Split('-');
		return new RangeInt(Int32.Parse(split[0]), Int32.Parse(split[1]));
	}

	//Exemple
	//..##.......
	//#...#...#..
	//.#....#..#.
	//..#.#...#.#
	//.#...##..#.
	// trueCharacter = #
	public static bool[,] ParseBoolGrid(string input, char separator, char trueCharacter)
	{
		var lines = input.Split(separator);
		var grid = new bool[lines.Length, lines[0].Length];

		for (int y = 0; y < lines.Length; y++)
			for (int x = 0; x < lines[y].Length; x++)
				grid[y, x] = lines[y][x] == trueCharacter;

		return grid;
	}

	//Exemple
	//..##.......
	//#...#...#..
	//.#....#..#.
	//..#.#...#.#
	//.#...##..#.
	public static char[,] ParseCharGrid(string input, char lineSeparator)
	{
		var lines = input.Split(lineSeparator);
		var grid = new char[lines[0].TrimEnd().Length, lines.Length];

		for (int y = 0; y < lines.Length; y++)
		{
			var line = lines[y].TrimEnd();
			for (int x = 0; x < line.Length; x++)
				grid[x, y] = line[x];
		}

		return grid;
	}

	//Exemple
	//14 21 17 24  4
	//10 16 15  9 19
	//18  8 23 26 20
	//22 11 13  6  5
	//2  0 12  3  7
	public static int[,] ParseIntGrid(string input, char lineSeparator, char? colSeparator)
	{
		var lines = input.Split(lineSeparator);
		var grid = new int[lines[0].TrimEnd().Length, lines.Length];

		for (int y = 0; y < lines.Length; y++)
		{
			var line = lines[y].TrimEnd();
			if (colSeparator != null)
			{
				var lineValues = line.Split((char)colSeparator);
				for (int x = 0; x < lineValues.Length; x++)
					grid[x, y] = int.Parse(lineValues[x]);
			}
			else
			{
				for (int x = 0; x < line.Length; x++)
					grid[x, y] = line[x];
			}
		}

		return grid;
	}

	// -------------------------------------------
	/**
	Exemple: 
		light red bags contain 1 bright white bag, 2 muted yellow bags.
		dark orange bags contain 3 bright white bags, 4 muted yellow bags.
		keyValueSeparator = "contain"
		valuesSeparator = ", "
		quantityCapture = "(\d+)"
		faded blue bags contain no other bags.
		keyCapture = "([a-z] [a-z]) bag"
		emptyCapture = "no other bags"
	*/
	public static NodeGroup ParseDictionnaryNodes(string input, string keyValueSeparator, string valuesSeparator, string quantityCapture, string keyCapture, string emptyCapture)
	{
		var dic = new Dictionary<string, Node>();
		foreach (var line in input.Split("\n"))
		{
			if (string.IsNullOrEmpty(line))
				continue;

			var sections = line.Split(keyValueSeparator);
			var key = Regex.Match(sections[0], keyCapture).Groups[1].Value;
			var values = sections[1].Split(valuesSeparator);
			var list = new List<Node>();
			if (!Regex.IsMatch(sections[1], emptyCapture))
				foreach (var value in values)
				{
					var quantity = Regex.Match(value, quantityCapture).Groups.IntValue(1);
					var valueKey = Regex.Match(value, keyCapture).Groups[1].Value;
					list.Add(new Node(valueKey, quantity));
				}

			var node = new Node(key, 1);
			node.Children = list;
			dic.Add(key, node);
		}
		return new NodeGroup(dic);
	}

	/*public struct NodeItem
	{
		public string Key;
		public int Quantity;

		public NodeItem(string key, int quantity)
		{
			Key = key;
			Quantity = quantity;
		}
	}*/

	// -------------------------------------------

	public static GrowingGrid<char> ReadGrowingGrid(string input, char lineseparator, char defaultValue)
	{
		var grid = ParseCharGrid(input, lineseparator);
		var xRange = new Point(0, grid.GetLength(0));
		var yRange = new Point(0, grid.GetLength(1));
		var growingGrid = new GrowingGrid<char>(defaultValue, xRange, yRange, grid.Length, true, true);
		growingGrid.AddGrid(0, 0, grid, GridAxes.XY);
		return growingGrid;
	}

	/*public static Tree ReadTree(string input, char lineSeparator, char linkSeparator)
	{
		var outputNodes = new List<TreeNode>();
		var comNode = new TreeNode("COM");

		var nodeDick = new Dictionary<string, TreeNode>();
		var links = input.Split(lineSeparator).Select(x =>
		{
			var split = x.Split(linkSeparator);
			return (split[0], split[1]);
		}).ToList();

		outputNodes.Add(comNode);
		nodeDick.Add("COM", comNode);

		//Debug.Log(string.Join(",", links.Select(x => x.Item1 + " ) " + x.Item2).ToArray()));
		foreach (var link in links)
		{
			var node = new TreeNode(link.Item2);
			outputNodes.Add(node);
			nodeDick.Add(link.Item2, node);
		}

		//foreach (var item in nodeDick)
		//	Debug.Log($"Key: {item.Key}, Value: {item.Value}");

		//Debug.Log(string.Join(",", outputNodes.Select(x => x.Name).ToArray()));

		foreach (var link in links)
		{
			var parentId = link.Item1;
			if (!nodeDick.ContainsKey(parentId))
			{
				//Debug.Log($"Unknown item {parentId}");
				continue;
			}
			//else
			//Debug.Log($"Trouvé item {parentId}");

			//Debug.Log(string.Join(",", nodeDick.Select(x => x.Key).ToArray()));
			var parent = nodeDick[parentId];
			var child = nodeDick[link.Item2];
			child.Parent = parent;
			parent.Children.Add(child);
		}

		return new Tree(nodeDick, comNode);
	}
*/
}