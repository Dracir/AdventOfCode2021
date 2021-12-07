using System;
using System.Diagnostics;
using System.Threading;
using Console = ElfConsole;

class Program
{

	private static int _currentDay = 7;
	private static int _currentPart = 2;
	private static bool _useConsole = true;

	private static DayBase[] _days = new DayBase[26];

	static void Main(string[] args)
	{

		//YearFileCreator.CreateYear();
		RunSuperConsole();

	}

	private static void RunSuperConsole()
	{
		System.Console.CursorVisible = false;

		UpdateHeader(_currentDay, _days[_currentDay], _currentPart);
		_days[_currentDay] = NewDay(_currentDay);

		do
		{
			var consoleStr = _useConsole ? "[C] Stop Output" : "[C] Use Output";
			Console.WriteLinesAt($"[←]Previous  [→]Next  [↩]Run  [1]Part 1  [2]Part 2  {consoleStr}  [ESC]Quit", 2, Console.Height - 2);
			var userInput = Console.ReadKey();
			if (userInput.Key == ConsoleKey.D1)
			{
				_currentPart = 1;
				UpdateHeader(_currentDay, _days[_currentDay], _currentPart);
			}
			else if (userInput.Key == ConsoleKey.D2)
			{
				_currentPart = 2;
				UpdateHeader(_currentDay, _days[_currentDay], _currentPart);
			}
			else if (userInput.Key == ConsoleKey.RightArrow)
			{
				_currentDay++;
				if (_currentDay > 25) _currentDay = 0;
				UpdateHeader(_currentDay, _days[_currentDay], _currentPart);
				_days[_currentDay] = NewDay(_currentDay);
			}
			else if (userInput.Key == ConsoleKey.LeftArrow)
			{
				_currentDay--;
				if (_currentDay < 0) _currentDay = 25;
				UpdateHeader(_currentDay, _days[_currentDay], _currentPart);
				_days[_currentDay] = NewDay(_currentDay);
			}
			else if (userInput.Key == ConsoleKey.C)
			{
				_useConsole = !_useConsole;
			}
			else if (userInput.Key == ConsoleKey.Enter)
			{
				//UpdateHeader(_currentDay, _days[_currentDay], _currentPart);
				StartDay(_currentDay);
			}
			else if (userInput.Key == ConsoleKey.Escape)
			{
				break;
			}
		}
		while (true);

	}

	private static DayBase NewDay(int currentDay) => currentDay switch
	{
		1 => new Day1(),
		2 => new Day2(),
		3 => new Day3(),
		4 => new Day4(),
		5 => new Day5(),
		6 => new Day6(),
		7 => new Day7(),
		/*8 => new Day8(),
		9 => new Day9(),
		10 => new Day10(),
		11 => new Day11(),
		12 => new Day12(),
		13 => new Day13(),
		14 => new Day14(),
		15 => new Day15(),
		16 => new Day16(),
		17 => new Day17(),
		18 => new Day18(),
		19 => new Day19(),
		20 => new Day20(),
		21 => new Day21(),
		22 => new Day22(),
		23 => new Day23(),
		24 => new Day24(),
		25 => new Day25(),
		_ => new TestDay()*/
		_ => new Day1(),
	};

	private static void UpdateHeader(int day, DayBase dayBase, int part)
	{
		Console.Clear();
		Console.SetTitle(day, DaysTitles.GetDayTitle(day), part);
	}

	private static void StartDay(int currentDay)
	{
		var stopwatch = new Stopwatch();
		var answer = 0L;
		var input = DaysInputs.ReadInput(currentDay);
		stopwatch.Reset();
		stopwatch.Start();
		if (_currentPart == 1)
			answer = _days[currentDay].Part1(input);
		else
			answer = _days[currentDay].Part2(input);
		stopwatch.Stop();

		Console.ForegroundColor = ConsoleColor.Gray;
		var m = stopwatch.Elapsed.Minutes > 0 ? $"{stopwatch.Elapsed.Minutes}m" : "";
		var s = stopwatch.Elapsed.Seconds > 0 ? $"{stopwatch.Elapsed.Seconds}s" : "";
		var ms = stopwatch.Elapsed.Milliseconds > 0 ? $"{stopwatch.Elapsed.Milliseconds}ms" : "";
		Console.WriteLinesAt($"Time : {m}{s}{ms}", 2, Console.Height - 1);
		Console.WriteLinesAt($"Answer : {answer}", 40, Console.Height - 1);
	}
}