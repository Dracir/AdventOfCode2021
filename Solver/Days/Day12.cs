using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;


public class Day12 : DayBase
{
	public override long Part1(string input)
	{
		var nodes = NewMethod(input);
		var paths = 0;
		foreach (var path in nodes["start"])
			paths += GetPathsToEnd(nodes, path, new string[0], false);

		return paths;
	}

	public override long Part2(string input)
	{
		var nodes = NewMethod(input);
		var paths = 0;
		foreach (var path in nodes["start"])
			paths += GetPathsToEnd(nodes, path, new string[0], true);

		return paths;
	}

	private int GetPathsToEnd(Dictionary<string, List<string>> nodes, string currentNode, string[] smallVisited, bool canVisiteOneTwice)
	{
		if (currentNode == "start")
			return 0;
		if (currentNode == "end")
			return 1;

		var isSmallCaverne = char.IsLower(currentNode[0]);
		if (isSmallCaverne && smallVisited.Contains(currentNode))
			if (!canVisiteOneTwice)
				return 0;
			else canVisiteOneTwice = false;

		int paths = 0;
		var a = smallVisited.ToList();
		a.Add(currentNode);
		var newSmallVisited = a.ToArray();
		foreach (var path in nodes[currentNode])
			paths += GetPathsToEnd(nodes, path, newSmallVisited, canVisiteOneTwice);

		return paths;
	}

	private static Dictionary<string, List<string>> NewMethod(string input)
	{
		Dictionary<string, List<string>> nodes = new Dictionary<string, List<string>>();

		var paths = input.Split("\n");
		foreach (var path in paths)
		{
			var splited = path.Split("-");
			var from = splited[0];
			var to = splited[1];

			if (!nodes.ContainsKey(from))
				nodes.Add(from, new List<string>());
			var parent = nodes[from];

			if (!nodes.ContainsKey(to))
				nodes.Add(to, new List<string>());
			var child = nodes[to];

			child.Add(from);
			parent.Add(to);
		}
		return nodes;
	}
}