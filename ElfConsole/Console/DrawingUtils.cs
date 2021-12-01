using System.Text;
using static UnicodeCharacter;
public static class DrawingUtils
{

	public const int TITLE_START_X = 3;

	public static void DrawBox(int x, int y, int w, int h, string title, ConsoleColor backgroundColor, ConsoleColor foregroundColor, ConsoleColor titleColor)
	{
		Console.BackgroundColor = backgroundColor;
		Console.ForegroundColor = foregroundColor;
		Console.CursorLeft = x;
		Console.CursorTop = y;

		var strBuilder = new StringBuilder(w);
		strBuilder.Append(BOX_CORNER_ROUNDED_TOP_LEFT + new String(BOX_HORIZONTAL, w - 2) + BOX_CORNER_ROUNDED_TOP_RIGHT);
		strBuilder[TITLE_START_X] = BOX_CORNER_TOP_RIGHT;
		strBuilder[TITLE_START_X + title.Length + 1] = BOX_CORNER_TOP_LEFT;
		Console.Write(strBuilder);

		Console.CursorLeft = x + TITLE_START_X + 1;
		Console.ForegroundColor = titleColor;
		Console.Write(title);

		Console.CursorLeft = x;
		Console.ForegroundColor = foregroundColor;

		for (int i = y + 1; i < y + h; i++)
		{
			Console.CursorLeft = x;
			Console.CursorTop = i;
			Console.Write(BOX_VERTICAL + new String(' ', w - 2) + BOX_VERTICAL);

		}

		Console.CursorLeft = x;
		Console.CursorTop = y + h;
		Console.Write(BOX_CORNER_ROUNDED_BOTTOM_LEFT + new String(BOX_HORIZONTAL, w - 2) + BOX_CORNER_ROUNDED_BOTTOM_RIGHT);
	}
}