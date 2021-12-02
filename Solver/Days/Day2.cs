using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static Point;


public class Day2 : DayBase
{

	FormatedHeaderValue Horizontal;
	FormatedHeaderValue Depth;
	FormatedHeaderValue Aim;

	FormatedHeaderValue CmdHorizontal;
	FormatedHeaderValue CmdDepth;

	public Day2()
	{
		DrawingUtils.DrawBox(0, 0, 22, 4, "Course", ConsoleColor.Black, ConsoleColor.DarkBlue, ConsoleColor.White);
		Console.ForegroundColor = ConsoleColor.Gray;
		Console.WriteAt("Horizontal: ", 1, 1);
		Console.WriteAt("Depth     : ", 1, 2);
		Console.WriteAt("Aim       : ", 1, 3);
		Horizontal = new FormatedHeaderValue(new Point(13, 1), 8, ConsoleColor.White);
		Depth = new FormatedHeaderValue(new Point(13, 2), 8, ConsoleColor.White);
		Aim = new FormatedHeaderValue(new Point(13, 3), 8, ConsoleColor.White);

		DrawingUtils.DrawBox(0, 5, 22, 3, "Command", ConsoleColor.Black, ConsoleColor.DarkRed, ConsoleColor.White);
		Console.ForegroundColor = ConsoleColor.Gray;
		Console.WriteAt("Horizontal: ", 1, 6);
		Console.WriteAt("Depth     : ", 1, 7);
		CmdHorizontal = new FormatedHeaderValue(new Point(13, 6), 8, ConsoleColor.White);
		CmdDepth = new FormatedHeaderValue(new Point(13, 7), 8, ConsoleColor.White);
	}

	public override long Part1(string input)
	{
		var movement = input.Split("\n")
			.Select(str => parseDir(str))
			.Aggregate(new Course(0, 0, 0),
				(course, cmd) => new Course(course.horizontal + cmd.forward, course.depth + cmd.depth, 0));
		return movement.horizontal * movement.depth;
	}

	public override long Part2(string input)
	{
		var movement = input.Split("\n")
			.Select(str => parseDir(str))
			.Aggregate(new Course(0, 0, 0),
				(course, cmd) =>
				{
					var c = new Course(course.horizontal + cmd.forward,
											course.depth + cmd.forward * course.aim, course.aim + cmd.depth);
					UpdateHeaders(c, cmd);
					return c;
				});
		return movement.horizontal * movement.depth;
	}

	private void UpdateHeaders(Course c, (int forward, int depth) cmd)
	{
		Horizontal.SetValue(c.horizontal);
		Depth.SetValue(c.depth);
		Aim.SetValue(c.aim);
		CmdHorizontal.SetValue(cmd.forward);
		CmdDepth.SetValue(cmd.depth);
	}

	private (int forward, int depth) parseDir(string str)
	{
		if (str.StartsWith("forward"))
			return (int.Parse(str.Split(' ')[1]), 0);
		else if (str.StartsWith("down"))
			return (0, int.Parse(str.Split(' ')[1]));
		else // assume up
			return (0, -int.Parse(str.Split(' ')[1]));
	}

	record Course(int horizontal, int depth, int aim);

}