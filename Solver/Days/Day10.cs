using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;


public class Day10 : DayBase
{
	public override long Part1(string input)
	{
		return input.Split("\n")
			.Select(line => (line, GetCorruptedChar(line)))
			.Sum(x => GetScore(x.Item2)); ;
	}

	private long GetScore(char? value)
	{
		if (!value.HasValue)
			return 0;
		return value switch
		{
			')' => 3,
			']' => 57,
			'}' => 1197,
			'>' => 25137,
			_ => 0
		};
	}

	private static char? GetCorruptedChar(string line)
	{
		var stack = new Stack<char>();
		foreach (var c in line)
		{
			switch (c)
			{
				case '(':
				case '[':
				case '{':
				case '<':
					stack.Push(c);
					break;
				case ')':
					if (stack.Pop() != '(')
						return ')';
					break;
				case '}':
					if (stack.Pop() != '{')
						return '}';
					break;
				case ']':
					if (stack.Pop() != '[')
						return ']';
					break;
				case '>':
					if (stack.Pop() != '<')
						return '>';
					break;
			}
		}

		return null;
	}

	public override long Part2(string input)
	{
		var lineCompletion = input.Split("\n")
		.Select(line => (line, GetCorruptedChar(line)))
		.Where(x => !x.Item2.HasValue)
		.Select(x => x.line)
		.Select(x => GetCompletingSequence(x))
		.Select(x => Score(x))
		.ToList();



		return lineCompletion.OrderBy(x => x).Skip(lineCompletion.Count / 2).First();
	}

	private long Score(string line)
	{
		var total = 0L;
		foreach (var c in line)
		{
			total *= 5;
			if (c == ')') total += 1;
			else if (c == ']') total += 2;
			else if (c == '}') total += 3;
			else if (c == '>') total += 4;
		}
		return total;
	}

	private string GetCompletingSequence(string line)
	{
		var completing = "";
		var stack = new Stack<char>();
		foreach (var c in line)
		{
			switch (c)
			{
				case '(':
				case '[':
				case '{':
				case '<':
					stack.Push(c);
					break;
				case ')':
				case '}':
				case ']':
				case '>':
					stack.Pop();
					break;
			}
		}
		while (stack.Count != 0)
			completing += GetReverse(stack.Pop());

		return completing;
	}
	public static char GetReverse(char c) => c switch
	{
		'(' => ')',
		')' => '(',
		'{' => '}',
		'}' => '{',
		'[' => ']',
		']' => '[',
		'<' => '>',
		'>' => '<',
		_ => ' '
	};
}