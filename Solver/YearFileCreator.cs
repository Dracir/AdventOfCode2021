using System.IO;

public static class YearFileCreator
{

	private static string MainFileText = @"using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;


public class Day{DayX} : DayBase
{
	public override long Part1(string input)
	{
		return 0;
	}

	public override long Part2(string input)
	{
		return 0;
	}
}
";


	public static void CreateYear()
	{
		for (int day = 19; day <= 25; day++)
		{
			var path = Path.Combine("DaysInput", $"Day{day}.txt");
			File.WriteAllText(path, "");

			path = Path.Combine("Days", $"Day{day}.cs");
			var txt = MainFileText.Replace("{DayX}", $"{day}");
			File.WriteAllText(path, txt);
		}
	}
}