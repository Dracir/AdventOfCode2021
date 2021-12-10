using static Day8;
using Console = ElfConsole;

public static class Day8Console
{
	public class P2
	{
		static readonly int DisplayX = 1;
		static readonly int DisplayY = 7;
		static readonly int DisplayInputSignalX = 15;
		static readonly int DisplayValueWidth = 10;
		private FormatedPrintedValue CurrentPattern;
		private FormatedPrintedValue CurrentOutputString;
		private FormatedPrintedValue[] CurrentValues;
		private SevenSegmentDisplayRenderer[] ValueDisplays;

		private DisplayData? LastDiplay;
		public P2()
		{
			//Input row
			DrawingUtils.DrawBox(0, 0, 78, 4, "Input Row", null, ConsoleColor.Gray, ConsoleColor.White);
			CurrentPattern = new FormatedPrintedValue(new Point(18, 1), 59, ConsoleColor.Gray);
			CurrentOutputString = new FormatedPrintedValue(new Point(18, 2), 59, ConsoleColor.Gray);
			Console.WriteLineAt("Signal patterns:", 1, 1);
			Console.WriteLineAt("Output value:", 1, 2);

			//DisplayOutputInput :/
			DrawingUtils.DrawBox(DisplayX, DisplayY, DisplayValueWidth, 10, "Value", null, ConsoleColor.Gray, ConsoleColor.White);

			//Display
			DrawingUtils.DrawBox(DisplayX + DisplayValueWidth, DisplayY + 8, 7 * 4, 9, "Display", null, ConsoleColor.Gray, ConsoleColor.White);
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLineAt("╯abcdefg╰", DisplayX + DisplayValueWidth + DisplayInputSignalX, DisplayY + 8);
			Console.WriteLinesAt("a\nb\nc\nd\ne\nf\ng", DisplayX + DisplayValueWidth - 1, DisplayY + 1);

			CurrentValues = new FormatedPrintedValue[4];
			for (int i = 0; i < 4; i++)
				CurrentValues[i] = new FormatedPrintedValue(new Point(DisplayX + 1, DisplayY + 1 + i), 7, ConsoleColor.Gray);

			ValueDisplays = new SevenSegmentDisplayRenderer[4];
			for (int i = 0; i < 4; i++)
			{
				var pt = new Point(DisplayX + DisplayValueWidth + 1 + i * 6, DisplayY + 9);
				ValueDisplays[i] = new SevenSegmentDisplayRenderer(pt, 3, 2, ConsoleColor.Green, ConsoleColor.DarkGray);
			}
		}

		public void SetDisplay(DisplayData display)
		{
			LastDiplay = display;
			CurrentPattern.SetValue(string.Join(",", display.Patterns));
			CurrentOutputString.SetValue(string.Join(",", display.OutputValue));

			for (int i = 0; i <= 6; i++)
				Console.WriteLineAt(new String(' ', DisplayInputSignalX + 8), DisplayX + DisplayValueWidth, DisplayY + i);
			for (int i = 0; i < 4; i++)
			{
				CurrentValues[i].SetValue(display.OutputValue[i]);
				ValueDisplays[i].SetValue(-1);
			}
		}

		public void SetConfiguration(Configuration configuration)
		{
			//Draw Plain Segment
			Console.ForegroundColor = ConsoleColor.Gray;
			for (int i = 7 - 1; i >= 0; i--)
				DrawSegment(configuration, (char)('a' + i));

			//Lets start drawing the numbers
			if (LastDiplay == null)//WHAT
				return;

			var total = 0;
			for (int i = 0; i < LastDiplay.OutputValue.Count; i++)
			{
				var digitStr = LastDiplay.OutputValue[i];
				if (i != 0)
					CurrentValues[i - 1]._Color = ConsoleColor.Gray;

				CurrentValues[i]._Color = ConsoleColor.Green;

				Console.ForegroundColor = ConsoleColor.Red;
				foreach (var seg in OrderedString(digitStr).Reverse())
					DrawSegment(configuration, seg);

				int value = GetValue(configuration, digitStr);
				ValueDisplays[i].SetValue(value);

				Console.ForegroundColor = ConsoleColor.Gray;
				for (int j = 7 - 1; j >= 0; j--)
					DrawSegment(configuration, (char)('a' + j));

				total += value * (int)Math.Pow(10, 3 - i);
			}
			CurrentValues[3]._Color = ConsoleColor.Gray;
		}

		private int GetValue(Configuration configuration, string digitStr)
		{
			var coded = OrderedString(digitStr);
			var value = configuration.NumberSegments.First(x => OrderedString(x.Value) == coded).Key;
			return value;
		}

		private void DrawSegments(string digitStr, Configuration configuration)
		{
			foreach (var seg in OrderedString(digitStr).Reverse())
				DrawSegment(configuration, seg);
		}

		private string OrderedString(string values) => String.Concat(values.OrderBy(c => c));

		private static void DrawSegment(Configuration configuration, char from)
		{
			var x = DisplayValueWidth + DisplayX;
			var i = from - (int)'a';
			var to = configuration.SegmentMapping[from];
			var targetY = (int)to - (int)'a';

			var line = new String('-', DisplayInputSignalX + 1 + targetY) + '╮';
			Console.WriteLineAt(line, x, DisplayY + 1 + i);
			for (int j = 6; j > i; j--)
				Console.WriteCharAt('|', x + DisplayInputSignalX + 1 + targetY, DisplayY + 1 + j);
		}
	}
}

