using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;

public class Day6 : DayBase
{
	public override long Part1(string input)
	{
		var lanternFishSchool = InputParser.ListOfInts(input);
		long population = 0;

		foreach (var lanternFish in lanternFishSchool)
			population += CalculatePopulation(lanternFish, 80);
		return population;
	}

	private ConcurrentDictionary<(int timer, int day), long> TimerDayPoluation = new ConcurrentDictionary<(int timer, int day), long>();

	private long CalculatePopulation(int timer, int day)
	{
		if (TimerDayPoluation.ContainsKey((timer, day)))
			return TimerDayPoluation[(timer, day)];

		long myKids = 0;
		for (int i = timer + 1; i <= day; i += 7)
			myKids += CalculatePopulation(8, day - i);

		TimerDayPoluation[(timer, day)] = 1 + myKids;
		return 1 + myKids;
	}

	public override long Part2(string input)
	{
		var lanternFishSchool = InputParser.ListOfInts(input);
		long[] populations = new long[6];
		Parallel.For(1, 6, (i) => populations[i] = CalculatePopulation(i, 256));

		long totalPopulation = 0;
		foreach (var lanternFish in lanternFishSchool)
			totalPopulation += populations[lanternFish];

		return totalPopulation;
	}
}