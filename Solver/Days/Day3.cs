using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;


public class Day3 : DayBase
{
	public override long Part1(string input)
	{
		var p1Console = new Day3Console.P1();

		var values = input.Split("\n");
		var nbRepports = values.Length;

		var oneCount = values.CountCharPerCol('1');
		p1Console.Set1Bits(oneCount, nbRepports);

		var gammaBits = oneCount.Select(x => x > nbRepports - x);
		var gamma = BitArrayToBinary(gammaBits);
		var epsilonBits = oneCount.Select(x => x < nbRepports - x);
		var epsilon = BitArrayToBinary(epsilonBits);
		p1Console.SetGammaEpsilon(gammaBits, gamma, epsilonBits, epsilon);

		return gamma * epsilon;
	}

	public override long Part2(string input)
	{
		var values = input.Split("\n");

		var oxygenGeneratorRatingBits = Search(values, oxygenGeneratorBitCriteria, 0);
		var co2ScrubberRatingBits = Search(values, co2ScrubberBitCriteria, 0);

		var oxygenGeneratorRating = StringToBinary(oxygenGeneratorRatingBits, '1');
		var co2ScrubberRating = StringToBinary(co2ScrubberRatingBits, '1');

		return oxygenGeneratorRating * co2ScrubberRating;
	}

	private char oxygenGeneratorBitCriteria((int oneCount, int total) p)
	{
		if (p.oneCount == p.total - p.oneCount)
			return '1';
		return p.oneCount > p.total - p.oneCount ? '1' : '0';
	}

	private char co2ScrubberBitCriteria((int oneCount, int total) p)
	{
		if (p.oneCount == p.total - p.oneCount)
			return '0';
		return p.oneCount > p.total - p.oneCount ? '0' : '1';
	}

	private string Search(IEnumerable<string> values, Func<(int oneCount, int total), char> bitCriteria, int position)
	{
		if (values.Count() == 1)
			return values.First();

		var oneCount = values.Count(x => x[position] == '1');
		var valueToKeep = bitCriteria((oneCount, values.Count()));
		var keepedValues = values.Where(x => x[position] == valueToKeep);

		return Search(keepedValues.ToList(), bitCriteria, ++position);
	}

}