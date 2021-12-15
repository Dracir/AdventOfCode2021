using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;

public class Day14 : DayBase
{
	public override long Part1(string inputStr)
	{
		(var polymer, var rules) = Parse(inputStr);
		for (int step = 0; step < 10; step++)
		{
			polymer = DoStep(polymer, rules);
			Console.WriteLineAt($"Step {step}", 0, step);
		}
		var letterCounts = polymer.ToCharArray()
			.GroupBy(x => x)
			.Select(x => (x.Key, x.Count()));


		return letterCounts.Max(x => x.Item2) - letterCounts.Min(x => x.Item2);
	}

	private string DoStep(string polymer, Dictionary<string, char> rules)
	{
		var newPolymer = "" + polymer[0];
		for (int i = 0; i < polymer.Length - 1; i++)
		{
			var key = polymer[i] + "" + polymer[i + 1];
			if (!rules.ContainsKey(key))
				continue;
			newPolymer += rules[key] + "" + polymer[i + 1];
		}

		return newPolymer;
	}

	private (string, Dictionary<string, char>) Parse(string inputStr)
	{
		var splitted = inputStr.Split("\n\n");
		var polymer = splitted[0];
		var rules = splitted[1].Split("\n")
			.Select(x => x.Split("->"))
			.Select(x => (x[0].Trim(), x[1].Trim()[0]))
			.ToDictionary(x => x.Item1, y => y.Item2);
		return (polymer, rules);
	}

	public override long Part2(string inputStr)
	{
		(var polymer, var rules) = Parse(inputStr);
		var elementcounts = new Dictionary<char, long>();

		elementcounts.Add(polymer[0], 1);

		for (int i = 0; i < polymer.Length - 1; i++)
		{
			var key = polymer[i] + "" + polymer[i + 1];
			var countsForPair = CalculateElementsCounts(key, rules, 40);
			AddToDictionnary(elementcounts, countsForPair);
			elementcounts[polymer[i]]--;
		}

		return elementcounts.Max(x => x.Value) - elementcounts.Min(x => x.Value);
	}

	private Dictionary<(string pair, int totalSteps), Dictionary<char, long>> PairStep_ElementsCount
		= new Dictionary<(string pair, int totalSteps), Dictionary<char, long>>();

	private Dictionary<char, long> CalculateElementsCounts(string pair, Dictionary<string, char> rules, int stepsToDo)
	{
		var key = (pair, stepsToDo);
		if (PairStep_ElementsCount.ContainsKey(key))
			return PairStep_ElementsCount[key];

		var elementCounts = new Dictionary<char, long>();
		if (stepsToDo == 0)
		{
			elementCounts[pair[0]] = 1;
			if (!elementCounts.ContainsKey(pair[1]))
				elementCounts[pair[1]] = 0;
			elementCounts[pair[1]] += 1;
			return elementCounts;
		}

		var newPolymer1 = pair[0] + "" + rules[pair];
		var newPolymer2 = rules[pair] + "" + pair[1];

		var countsForPoly1 = CalculateElementsCounts(newPolymer1, rules, stepsToDo - 1);
		var countsForPoly2 = CalculateElementsCounts(newPolymer2, rules, stepsToDo - 1);

		AddToDictionnary(elementCounts, countsForPoly1);
		AddToDictionnary(elementCounts, countsForPoly2);

		elementCounts[rules[pair]] -= 1;
		PairStep_ElementsCount[key] = elementCounts;

		return elementCounts;
	}

	private static void AddToDictionnary(Dictionary<char, long> elementCounts, Dictionary<char, long> from)
	{
		foreach (var item in from)
		{
			if (!elementCounts.ContainsKey(item.Key))
				elementCounts.Add(item.Key, 0);
			elementCounts[item.Key] += item.Value;
		}
	}
}