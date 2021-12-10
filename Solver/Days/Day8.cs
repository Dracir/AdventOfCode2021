using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;


public class Day8 : DayBase
{

	public static readonly char[] SEGMENTS = { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
	public static readonly Dictionary<char, int[]> NUMBERS_FOR_SEGMENT = new Dictionary<char, int[]>() {
		{ 'a', new int[]{0, 2, 3, 5, 6, 7, 8, 9 } },
		{ 'b',new int[]{0, 4, 5, 6, 8, 9 } },
		{ 'c',new int[]{0, 1, 2, 3, 4, 7, 8, 9 } },
		{ 'd',new int[]{2, 3, 4, 5, 6, 8, 9 } },
		{ 'e',new int[]{0, 2, 6, 8 } },
		{ 'f',new int[]{0, 1, 3, 4, 5, 6, 7, 8, 9 } } ,
		{ 'g',new int[]{0, 2, 3, 5, 6, 8, 9 } } ,
	};

	public static readonly Dictionary<int, char[]> SEGMENT_FOR_NUMBER = new Dictionary<int, char[]>() {
		{ 0, new char[]{'a', 'b', 'c', 'e', 'f', 'g' } },
		{ 1, new char[]{'c', 'f'} },
		{ 2, new char[]{'a', 'c', 'd', 'e', 'g' } },
		{ 3, new char[]{'a', 'c', 'd', 'f', 'g' } },
		{ 4, new char[]{'b', 'c', 'd', 'f' } },
		{ 5, new char[]{'a', 'b', 'd', 'f', 'g' } },
		{ 6, new char[]{'a', 'b', 'd', 'e', 'f', 'g' } },
		{ 7, new char[]{'a', 'c', 'f', } },
		{ 8, new char[]{'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
		{ 9, new char[]{'a', 'b', 'c', 'd', 'f', 'g' } },
	};

	public static int CountSegmentNeededForNumber(int number) => SEGMENT_FOR_NUMBER[number].Length;

	public override long Part1(string input)
	{
		var displays = Parse(input);
		var lens = new List<int>() { 2, 3, 4, 7 };
		return displays.Sum(x => x.OutputValue.Count(y => lens.Contains(y.Length)));
	}

	public override long Part2(string input)
	{
		var console = new Day8Console.P2();
		var displays = Parse(input);
		var total = 0L;
		foreach (var display in displays)
		{
			console.SetDisplay(display);
			var configuration = GetConfiguration(display);
			console.SetConfiguration(configuration);
			var output = GetOutput(configuration, display);
			total += output;
		}
		return total;
	}

	private long GetOutput(Configuration configuration, DisplayData displayData)
	{
		var total = 0;
		for (int i = 0; i < displayData.OutputValue.Count; i++)
		{
			var coded = OrderedString(displayData.OutputValue[i]);
			var value = configuration.NumberSegments.First(x => OrderedString(x.Value) == coded).Key;
			total += value * (int)Math.Pow(10, 3 - i);
		}
		return total;
	}

	private string OrderedString(string values) => String.Concat(values.OrderBy(c => c));

	private Configuration GetConfiguration(DisplayData displayData)
	{
		Dictionary<char, List<char>> segmentMapping = CreateBasicMappingDictionnary();
		Dictionary<int, string> numberSegments = new Dictionary<int, string>();
		var patterns5segments = displayData.Patterns.Where(x => x.Length == 5).ToList();
		var patterns6segments = displayData.Patterns.Where(x => x.Length == 6).ToList();

		var pattern1 = displayData.Patterns.First(x => x.Length == 2);
		numberSegments.Add(1, pattern1);
		RemoveCandidatesFromSegment(segmentMapping, SEGMENT_FOR_NUMBER[1], Flip(pattern1.ToCharArray()));

		var pattern4 = displayData.Patterns.First(x => x.Length == 4);
		numberSegments.Add(4, pattern4);
		RemoveCandidatesFromSegment(segmentMapping, SEGMENT_FOR_NUMBER[4], Flip(pattern4.ToCharArray()));

		var pattern7 = displayData.Patterns.First(x => x.Length == 3);
		numberSegments.Add(7, pattern7);
		RemoveCandidatesFromSegment(segmentMapping, SEGMENT_FOR_NUMBER[7], Flip(pattern7.ToCharArray()));

		var pattern8 = displayData.Patterns.First(x => x.Length == 7);
		numberSegments.Add(8, pattern8);

		var pattern6 = patterns6segments.First(x => segmentMapping['c'].Except(x.ToCharArray()).Count() == 1);
		patterns6segments.Remove(pattern6);
		numberSegments.Add(6, pattern6);
		RemoveCandidatesFromSegment(segmentMapping, SEGMENT_FOR_NUMBER[6], Flip(pattern6.ToCharArray()));

		segmentMapping['c'].Remove(segmentMapping['f'].First());
		RemoveCandidateFromSegment(segmentMapping, SEGMENTS.Except(new char[] { 'c' }).ToArray(), segmentMapping['c'].First());
		RemoveCandidateFromSegment(segmentMapping, SEGMENTS.Except(new char[] { 'f' }).ToArray(), segmentMapping['f'].First());
		RemoveCandidateFromSegment(segmentMapping, SEGMENTS.Except(new char[] { 'a' }).ToArray(), segmentMapping['a'].First());
		// Know A, C, F, 

		var pattern5 = patterns5segments.First(x => pattern6.Except(x.ToCharArray()).Count() == 1);
		patterns5segments.Remove(pattern5);
		numberSegments.Add(5, pattern5);
		RemoveCandidatesFromSegment(segmentMapping, SEGMENT_FOR_NUMBER[5], Flip(pattern5.ToCharArray()));

		var segmentE = pattern6.Except(pattern5).First();
		segmentMapping['e'].Clear();
		segmentMapping['e'].Add(segmentE);

		RemoveCandidateFromSegment(segmentMapping, SEGMENTS.Except(new char[] { 'e' }).ToArray(), segmentE);
		// Know A, C, E, F 
		//Remaining : 0,2,3,9

		var pattern9 = patterns6segments.First(x => SEGMENTS.Except(x.ToCharArray()).First() == segmentE);
		numberSegments.Add(9, pattern9);
		RemoveCandidatesFromSegment(segmentMapping, SEGMENT_FOR_NUMBER[9], Flip(pattern9.ToCharArray()));

		var pattern0 = patterns6segments.First(x => x != pattern9);
		numberSegments.Add(0, pattern0);
		RemoveCandidatesFromSegment(segmentMapping, SEGMENT_FOR_NUMBER[0], Flip(pattern0.ToCharArray()));

		// Know A, B, C, E, F 
		//Remaining : 2,3
		var segmentB = segmentMapping['b'].First();
		var pattern3 = patterns5segments.First(
			x => SEGMENTS.Except(x.ToCharArray()).Intersect(new char[] { segmentE, segmentB }).Count() == 2);
		numberSegments.Add(3, pattern3);
		RemoveCandidatesFromSegment(segmentMapping, SEGMENT_FOR_NUMBER[3], Flip(pattern3.ToCharArray()));


		var pattern2 = patterns5segments.First(x => x != pattern3);
		numberSegments.Add(2, pattern2);
		RemoveCandidatesFromSegment(segmentMapping, SEGMENT_FOR_NUMBER[2], Flip(pattern2.ToCharArray()));


		var finalMapping = segmentMapping.ToDictionary(item => item.Key, item => item.Value.First());
		return new Configuration(finalMapping, numberSegments);
	}

	private void RemoveCandidatesFromSegment(Dictionary<char, List<char>> mapping, char[] keys, char[] values)
	{
		foreach (var key in keys)
		{
			foreach (var value in values)
				mapping[key].Remove(value);
		}
	}

	private void RemoveCandidateFromSegment(Dictionary<char, List<char>> mapping, char[] keys, char value)
	{
		foreach (var key in keys)
			mapping[key].Remove(value);
	}

	private char[] Flip(char[] values) => SEGMENTS.Where(x => !values.Contains(x)).ToArray();

	private Dictionary<char, List<char>> CreateBasicMappingDictionnary()
	{
		var map = new Dictionary<char, List<char>>();
		for (int i = 0; i < 7; i++)
			map[(char)('a' + i)] = new List<char>(SEGMENTS);
		return map;
	}

	private List<DisplayData> Parse(string input)
	{
		var displays = new List<DisplayData>();
		foreach (var line in input.Split("\n"))
		{
			var sections = line.Split('|');
			var patterns = sections[0].Trim().Split(" ").ToList();
			var outputValue = sections[1].Trim().Split(" ").ToList();
			displays.Add(new DisplayData(patterns, outputValue));
		}
		return displays;
	}

	public record DisplayData(List<string> Patterns, List<string> OutputValue);

	public record Configuration(Dictionary<char, char> SegmentMapping, Dictionary<int, string> NumberSegments);
}