using Console = ElfConsole;

public static class Day4Console
{
	private static int nbBoardPerRow = 14;
	private static int boardStartX = 0;
	private static int boardStartY = 0;
	private static int drawStart = 48;

	public class P1
	{
		private GridPrintedValue[,] _boards;
		private ArrayPrintedValue _drawnedNumbers;
		private List<int> _currentNumbers = new List<int>();

		public P1(Day4.Day4Input input)
		{
			var nbRows = (int)Math.Ceiling(1.0d * input.boardsNumbers.Count / nbBoardPerRow);
			_boards = new GridPrintedValue[nbRows, nbBoardPerRow];
			for (int i = 0; i < input.boardsNumbers.Count; i++)
			{
				int x = i % nbBoardPerRow;
				int y = i / nbBoardPerRow;
				var bx = boardStartX + x * 15;
				var by = boardStartY + y * 6;
				DrawingUtils.DrawBox(bx, by, 15, 6, $"Bingo{x},{y}", ConsoleColor.Black, ConsoleColor.Gray, ConsoleColor.White);
				_boards[y, x] = new GridPrintedValue(new Point(bx + 1, by + 1), 2, 5, 1, 5, ConsoleColor.DarkGray);
				_boards[y, x].SetValue(input.boardsNumbers[i]);
			}

			DrawingUtils.DrawBox(0, drawStart, 80, 3, $"Draw", ConsoleColor.Black, ConsoleColor.Gray, ConsoleColor.White);
			_drawnedNumbers = new ArrayPrintedValue(new Point(1, drawStart + 1), 2, 100, 1, ConsoleColor.White);
		}

		public void WinnerBoard(int index)
		{
			int x = index % nbBoardPerRow;
			int y = index / nbBoardPerRow;
			var bx = boardStartX + x * 15;
			var by = boardStartY + y * 6;
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.BackgroundColor = ConsoleColor.White;
			Console.WriteLinesAt($"Bingo{x},{y}", bx + 4, by);
			Console.BackgroundColor = ConsoleColor.Black;
		}

		public void OnNewNumberDrawn(Day4.Day4Input input, int v)
		{
			_currentNumbers.Add(v);
			_drawnedNumbers.SetValue(_currentNumbers.ToArray());

			for (int i = 0; i < input.boardsNumbers.Count; i++)
			{
				int x = i % nbBoardPerRow;
				int y = i / nbBoardPerRow;
				for (int cx = 0; cx < 5; cx++)
					for (int cy = 0; cy < 5; cy++)
						if (input.boardsSelection[i][cx, cy])
							_boards[y, x].SetColor(cx, cy, ConsoleColor.White);
			}
		}
	}
}

