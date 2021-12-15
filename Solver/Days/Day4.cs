using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;

public class Day4 : DayBase
{
	public override long Part1(string inputStr)
	{
		var input = ParseInput(inputStr);
		var p1Console = new Day4Console.P1(input);
		int i = -1;
		while (input.winnerBoards.Count == 0)
		{
			i++;
			input = DrawNumber(input, input.numberDraw[i]);
			p1Console.OnNewNumberDrawn(input, input.numberDraw[i]);
			input = MarkWinnerBoard(input);
		}

		p1Console.WinnerBoard(input.winnerBoards.First());
		return ScoreBoard(input, input.winnerBoards.First()) * input.numberDraw[i];
	}

	public override long Part2(string inputStr)
	{
		var input = ParseInput(inputStr);
		var p1Console = new Day4Console.P1(input);
		int i = -1;
		while (input.winnerBoards.Count != input.boardsNumbers.Count)
		{
			i++;
			input = DrawNumber(input, input.numberDraw[i]);
			p1Console.OnNewNumberDrawn(input, input.numberDraw[i]);
			input = MarkWinnerBoard(input);
		}
		var dis = input.winnerBoards.Distinct();
		p1Console.WinnerBoard(input.winnerBoards.Last());
		return ScoreBoard(input, input.winnerBoards.Last()) * input.numberDraw[i];
	}

	private static long ScoreBoard(Day4Input input, int winnerBoard)
	{
		var numbers = input.boardsNumbers[winnerBoard];
		var selections = input.boardsSelection[winnerBoard];
		long score = 0;
		for (int i = 0; i < 5; i++)
			for (int j = 0; j < 5; j++)
				if (!selections[i, j])
					score += numbers[i, j];
		return score;
	}

	private Day4Input MarkWinnerBoard(Day4Input input)
	{
		for (int bi = 0; bi < input.boardsSelection.Count; bi++)
		{
			if (input.winnerBoards.Contains(bi))
				continue;
			var board = input.boardsSelection[bi];
			for (int i = 0; i < 5; i++)
			{
				if (board[i, 0] && board[i, 1] && board[i, 2] && board[i, 3] && board[i, 4])
				{
					input.winnerBoards.Add(bi);
					break;
				}
				if (board[0, i] && board[1, i] && board[2, i] && board[3, i] && board[4, i])
				{
					input.winnerBoards.Add(bi);
					break;
				}
			}
		}
		return input;
	}

	private Day4Input DrawNumber(Day4Input input, int numberDrawn)
	{
		for (int boardI = 0; boardI < input.boardsNumbers.Count; boardI++)
			Array2DUtils.Range2D(5, 5).ForEach(indexes =>
			{
				if (input.boardsNumbers[boardI][indexes.X, indexes.Y] == numberDrawn)
					input.boardsSelection[boardI][indexes.X, indexes.Y] = true;
			});
		return input;
	}

	private Day4Input ParseInput(string inputStr)
	{
		var sections = inputStr.Split("\n\n");
		var numberDraw = InputParser.ListOfInts(sections[0]).ToList();
		var boards = new List<int[,]>();
		var boardsSelection = new List<bool[,]>();
		for (int i = 1; i < sections.Length; i++)
		{
			boards.Add(InputParser.ParseInt2DArray(sections[i].Replace("  ", " ").Replace("\n ", "\n").Trim(), '\n', ' '));
			boardsSelection.Add(new bool[5, 5]);
		}

		return new Day4Input(numberDraw, boards, boardsSelection, new List<int>());
	}


	public record Day4Input(List<int> numberDraw, List<int[,]> boardsNumbers, List<bool[,]> boardsSelection, List<int> winnerBoards);
}