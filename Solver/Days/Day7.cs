using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;


public class Day7 : DayBase
{
	public override long Part1(string input)
	{
		var values = InputParser.ListOfInts(input);
		int minPosition = int.MaxValue;
		int minFuel = int.MaxValue;
		for (int position = 0; position < values.Max(); position++)
		{
			var fuel = 0;
			foreach (var crab in values)
				fuel += Math.Abs(crab - position);
			if (minFuel > fuel)
			{
				minFuel = fuel;
				minPosition = position;
			}
		}
		return minFuel;
	}

	public override long Part2(string input)
	{
		var values = InputParser.ListOfInts(input);
		int minPosition = int.MaxValue;
		long minFuel = long.MaxValue;
		for (int position = 0; position < values.Max(); position++)
		{
			var fuel = 0L;
			foreach (var crab in values)
				fuel += FuelCost(Math.Abs(crab - position));
			if (minFuel > fuel)
			{
				minFuel = fuel;
				minPosition = position;
			}
		}
		return minFuel;
	}

	private long FuelCost(int steps)
	{
		var cost = 0;
		for (int i = 1; i <= steps; i++)
			cost += i;
		return cost;
	}
}