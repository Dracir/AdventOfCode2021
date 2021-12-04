using System.Text;
using static UnicodeCharacter;
using Console = ElfConsole;

public static class DrawingUtils
{

	public const int TITLE_START_X = 3;

	// h : includes top and bottom row
	public static void DrawBox(int x, int y, int w, int h, string title, ConsoleColor backgroundColor, ConsoleColor foregroundColor, ConsoleColor titleColor)
	{
		Console.BackgroundColor = backgroundColor;
		Console.ForegroundColor = foregroundColor;

		var strBuilder = new StringBuilder(w);
		strBuilder.Append(BOX_CORNER_ROUNDED_TOP_LEFT + new String(BOX_HORIZONTAL, w - 2) + BOX_CORNER_ROUNDED_TOP_RIGHT);
		strBuilder[TITLE_START_X] = BOX_CORNER_TOP_RIGHT;
		strBuilder[TITLE_START_X + title.Length + 1] = BOX_CORNER_TOP_LEFT;
		Console.WriteAt(strBuilder.ToString(), x, y);

		Console.ForegroundColor = titleColor;
		Console.WriteAt(title.ToString(), x + TITLE_START_X + 1, y);

		Console.ForegroundColor = foregroundColor;

		for (int i = y + 1; i < y + h - 1; i++)
			Console.WriteAt(BOX_VERTICAL + new String(' ', w - 2) + BOX_VERTICAL, x, i);

		Console.WriteAt(BOX_CORNER_ROUNDED_BOTTOM_LEFT + new String(BOX_HORIZONTAL, w - 2) + BOX_CORNER_ROUNDED_BOTTOM_RIGHT, x, y + h - 1);
	}
}