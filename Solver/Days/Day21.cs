using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;
using System.Collections.Concurrent;

public class Day21 : DayBase
{
	public override long Part1(string input)
	{
		var p1 = 10;
		var p2 = 7;
		var score1 = 0;
		var score2 = 0;
		var dice = 1;
		var nbRolls = 0;
		while (score1 < 1000 && score2 < 1000)
		{
			nbRolls += 3;
			Roll(ref p1, ref score1, ref dice);
			if (score1 >= 1000)
				break;

			nbRolls += 3;
			Roll(ref p2, ref score2, ref dice);
		};
		return nbRolls * Math.Min(score1, score2);
	}

	private void Roll(ref int position, ref int score, ref int dice)
	{
		var move = RollDeterministicDice(ref dice);
		position = (position + move - 1) % 10 + 1;
		score += position;
	}

	private int RollDeterministicDice(ref int dice)
	{
		var move = 0;
		for (int i = 0; i < 3; i++)
		{
			move += dice++;
			if (dice == 101)
				dice = 1;
		}

		return move;
	}

	public static Dictionary<int, int> DiceRollCount = new Dictionary<int, int>();

	//too low
	//234386639
	public override long Part2(string input)
	{
		var p1 = 10;
		var p2 = 7;
		for (int i = 1; i <= 3; i++)
			for (int j = 1; j <= 3; j++)
				for (int k = 1; k <= 3; k++)
				{
					DiceRollCount.AddIfMissing(i + j + k, 0);
					DiceRollCount[i + j + k]++;
				}

		var wins = CalculateWins(p1, p2, 0, 0);
		return Math.Max(wins.winsP1, wins.winsP2);
	}

	private (long winsP1, long winsP2) CalculateWins(int p1, int p2, int score1, int score2)
	{
		var wins1 = 0L;
		var wins2 = 0L;
		//if (score1 == 20)
		//	return (3 * 3 * 3, 0);

		foreach (var item in DiceRollCount)
		{
			var newp1 = (p1 + item.Key - 1) % 10 + 1;
			var newScore1 = score1 + newp1;
			if (newScore1 >= 21)
				wins1 += item.Value;
			else
			{
				(var newWinsP2, var newWinsP1) = CalculateWins(p2, newp1, score2, newScore1);
				wins1 += newWinsP1 * item.Value;
				wins2 += newWinsP2 * item.Value;
			}
		}
		return (wins1, wins2);

	}
}
