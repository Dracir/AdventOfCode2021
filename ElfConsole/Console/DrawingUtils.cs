using System.Text;
using static UnicodeCharacter;
using Console = ElfConsole;

public static class DrawingUtils
{

	public const int TITLE_START_X = 2;

	// h : includes top and bottom row
	public static void DrawBox(int x, int y, int w, int h, string title, ConsoleColor? backgroundColor, ConsoleColor foregroundColor, ConsoleColor titleColor)
	{
		if (backgroundColor.HasValue)
			Console.BackgroundColor = backgroundColor.Value;
		Console.ForegroundColor = foregroundColor;

		var strBuilder = new StringBuilder(w);
		strBuilder.Append(BOX_CORNER_ROUNDED_TOP_LEFT + new String(BOX_HORIZONTAL, w - 2) + BOX_CORNER_ROUNDED_TOP_RIGHT);
		strBuilder[TITLE_START_X] = BOX_CORNER_TOP_RIGHT;
		strBuilder[TITLE_START_X + title.Length + 1] = BOX_CORNER_TOP_LEFT;
		Console.WriteLinesAt(strBuilder.ToString(), x, y);

		Console.ForegroundColor = titleColor;
		Console.WriteLinesAt(title.ToString(), x + TITLE_START_X + 1, y);

		Console.ForegroundColor = foregroundColor;

		for (int i = y + 1; i < y + h - 1; i++)
			Console.WriteLinesAt(BOX_VERTICAL + new String(' ', w - 2) + BOX_VERTICAL, x, i);

		Console.WriteLinesAt(BOX_CORNER_ROUNDED_BOTTOM_LEFT + new String(BOX_HORIZONTAL, w - 2) + BOX_CORNER_ROUNDED_BOTTOM_RIGHT, x, y + h - 1);
	}

	public static void DrawString_WithHighlightDiff(string before, string after, int x, int y, ConsoleColor normalColor, ConsoleColor highlight, int maxChangeLength)
	{
		var str = new StringBuilder();
		var beforeIndex = -1;
		var afterIndex = -1;
		while (afterIndex < after.Length)
		{
			//GetAllSame
			while (++beforeIndex < before.Length && ++afterIndex < after.Length && before[beforeIndex] == after[afterIndex])
				str.Append(after[afterIndex]);

			if (str.Length != 0)
			{
				Console.ForegroundColor = normalColor;
				Console.WriteLineAt(str.ToString(), x, y);
				x += str.Length;
				str.Clear();
			}

			var nextMatching = TryFindNextMatchingIndex(before, after, beforeIndex, afterIndex, maxChangeLength);
			if (nextMatching.beforeMove != 0)
				beforeIndex += nextMatching.beforeMove - 1;

			if (nextMatching.afterMove != 0)
			{
				str.Append(after[afterIndex..(afterIndex + nextMatching.afterMove - 1)]);
				afterIndex += nextMatching.afterMove - 1;
				Console.ForegroundColor = highlight;
				Console.WriteLineAt(str.ToString(), x, y);
				x += str.Length;
				str.Clear();
			}

		}
	}

	private static (int beforeMove, int afterMove) TryFindNextMatchingIndex(string before, string after, int beforeIndex, int afterIndex, int maxChangeLength)
	{
		var bi = beforeIndex;
		var ai = afterIndex;
		int sameInRow = 0;

		while (++bi < before.Length && bi - beforeIndex <= maxChangeLength && before[beforeIndex] != after[afterIndex])
			sameInRow++;
		if (sameInRow <= maxChangeLength)
			return (bi - beforeIndex, 0);

		sameInRow = 0;
		while (++ai < after.Length && ai - afterIndex <= maxChangeLength && before[beforeIndex] != after[afterIndex])
			sameInRow++;
		if (sameInRow <= maxChangeLength)
			return (0, ai - afterIndex);

		return (0, 0);
	}
}