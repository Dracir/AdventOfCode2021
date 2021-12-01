using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;


public class Day1 : DayBase
{
	public override long Part1(string input)
	{
		var values = InputParser.ListOfInts(input);
		int answer = 0;
		for (int i = 1; i < values.Length; i++)
		{
			if (values[i] > values[i - 1])
				answer++;
		}
		return answer;
	}

	public override long Part2(string input)
	{
		var values = InputParser.ListOfInts(input);
		var windows = new List<int>();
		for (int i = 2; i < values.Length; i++)
			windows.Add(values[i] + values[i - 1] + values[i - 2]);

		int answer = 0;
		for (int i = 1; i < windows.Count; i++)
			if (windows[i] > windows[i - 1])
				answer++;

		return answer;
	}

}