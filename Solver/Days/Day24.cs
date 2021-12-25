using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;
using System.Diagnostics;

public class Day24 : DayBase
{
	public override long Part1(string inputStr)
	{
		var instructions = ParseInput(inputStr);
		for (long i = 99999999999999; i >= 99911111111111; i--)
		{
			if (i.ToString().Contains('0')) continue;

			var valid = IsValid(i);
			if (valid)
				return i;
		}
		return 0;
	}

	public override long Part2(string input)
	{
		return 0;
	}

	private bool IsValid(Instruction[] instructions, long input)
	{
		var sw = new Stopwatch();
		sw.Start();
		var inputDigits = input.ToString().Select(x => x - '0').ToArray();
		int inputIndex = input.ToString().Length - 1;

		var variables = new long[4] { 0, 0, 0, 0 };
		foreach (var instruction in instructions)
		{
			var b = instruction.param2IsVariable ? variables[instruction.param2] : instruction.param2;
			switch (instruction.opCode)
			{
				case OP_CODE.Inp:
					variables[instruction.param1] = inputDigits[inputIndex--];
					break;

				case OP_CODE.Add:
					variables[instruction.param1] += b;
					break;

				case OP_CODE.Mul:
					variables[instruction.param1] *= b;
					break;

				case OP_CODE.Div:
					if (b < 0)
						return false;
					variables[instruction.param1] /= b;
					break;

				case OP_CODE.Mod:
					if (b < 0 || variables[instruction.param1] < 0)
						return false;
					variables[instruction.param1] = variables[instruction.param1] % b;
					break;

				case OP_CODE.Eql:
					variables[instruction.param1] = variables[instruction.param1] == b ? 1 : 0;
					break;
			}

		}

		sw.Stop();
		return variables[2] == 0;
	}
	private bool IsValid(long input)
	{
		//var sw = new Stopwatch();
		//sw.Start();
		long x = 0, y = 0, z = 0, w = 0;
		var inputDigits = input.ToString().Select(x => x - '0').ToArray();
		int inputIndex = input.ToString().Length - 1;

		w = inputDigits[inputIndex--];
		//Tout reviens à 0

		w = inputDigits[inputIndex--];
		x = 1;
		y = w + 12;
		z = y;

		w = inputDigits[inputIndex--];
		x = 1;
		y = 25 + 1;
		z = z * y;
		y = 0 + w + 15;
		y *= x;
		z += y;

		w = inputDigits[inputIndex--];
		x = z % 26;
		z /= 26;
		x -= 9;
		x = x == w ? 0 : 1;
		y = 25 * x + 1;
		z *= y;
		y = w + 12;
		y *= x;
		z += y;

		w = inputDigits[inputIndex--];
		x = z % 26;
		z /= 26;
		x -= 7;
		x = x == w ? 0 : 1;
		y = 25 * x + 1;
		z *= y;
		y = w + 15;
		y *= x;
		z += y;

		w = inputDigits[inputIndex--];
		x = z % 26 + 11;
		x = x == w ? 0 : 1;
		y = 25 * x + 1;
		z *= y;
		y = w + 2;
		y *= x;
		z += y;

		w = inputDigits[inputIndex--];
		x = z % 26;
		z = 0;
		x--;
		x = x == w ? 0 : 1;
		y = 25 * x + 1;
		z *= y;
		y = w + 11;
		y *= x;
		z += y;

		w = inputDigits[inputIndex--];
		x = z % 26;
		z = 0;
		x -= 16;
		x = x == w ? 0 : 1;
		y = 25 * x + 1;
		z *= y;
		y = w + 15;
		y *= x;
		z += y;

		w = inputDigits[inputIndex--];
		x = z % 26 + 11;
		x = x == w ? 0 : 1;
		y = 25 * x + 1;
		z *= y;
		y = w + 10;
		y *= x;
		z += y;

		w = inputDigits[inputIndex--];
		x = z % 26;
		z %= 26;
		x -= 15;
		x = x == w ? 0 : 1;
		y = 25 * x + 1;
		z *= y;
		y = w + 2;
		y *= x;
		z += y;

		w = inputDigits[inputIndex--];
		x = z % 26 + 10;
		x = x == w ? 0 : 1;
		y = 25 * x + 1;
		z *= y;
		y = w * x;
		z += y;

		w = inputDigits[inputIndex--];
		x = z % 26 + 12;
		x = x == w ? 0 : 1;
		y = 25 * x + 1;
		z *= y;
		y = w * x;
		z += y;

		w = inputDigits[inputIndex--];
		x = z % 26 - 4;
		z /= 26;
		x = x == w ? 0 : 1;
		y = 25 * x + 1;
		z *= y;
		y = w + 15;
		y *= x;
		z += y;

		w = inputDigits[inputIndex--];
		x = z % 26;
		z /= 26;
		x = x == w ? 0 : 1;
		y = 25 * x + 1;
		z *= y;
		y = w + 15;
		y *= x;
		z += y;

		//sw.Stop();
		return z == 0;
	}

	private Instruction[] ParseInput(string inputStr)
	{
		var split = inputStr.Split("\n");
		var instruction = new Instruction[split.Length];
		for (int i = 0; i < split.Length; i++)
		{
			var instructionSplitted = split[i].Split(" ");
			var opCode = instructionSplitted[0] switch
			{
				"" => OP_CODE.Add,
				_ => OP_CODE.Add
			};
			var param1 = VariableNameToIndex(instructionSplitted[1]);
			int param2 = 0;
			if (instructionSplitted.Length == 3)
			{
				var param2IsVariable = !char.IsDigit(instructionSplitted[2][0]);
				if (param2IsVariable)
					param2 = VariableNameToIndex(instructionSplitted[2]);
				else
					param2 = int.Parse(instructionSplitted[2]);
				instruction[i] = new Instruction(opCode, param1, param2, param2IsVariable);
			}
			else
				instruction[i] = new Instruction(opCode, param1, 0, false);

		}

		return instruction;
	}

	private int VariableNameToIndex(string v) => v switch
	{
		"x" => 0,
		"y" => 1,
		"z" => 2,
		_ => 3,
	};

}



public record Instruction(OP_CODE opCode, int param1, int param2, bool param2IsVariable);

public enum OP_CODE { Inp, Add, Mul, Div, Mod, Eql };