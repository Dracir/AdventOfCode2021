using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;


public class Day22 : DayBase
{
	public override long Part1(string input)
	{
		var rebootSteps = ParseSteps(input);
		Dictionary<Point3D, bool> reactorCore = new Dictionary<Point3D, bool>();
		foreach (var step in rebootSteps)
		{
			for (int x = Math.Max(-50, step.xRange.Min); x <= Math.Min(50, step.xRange.Max); x++)
			{
				for (int y = Math.Max(-50, step.yRange.Min); y <= Math.Min(50, step.yRange.Max); y++)
				{
					for (int z = Math.Max(-50, step.zRange.Min); z <= Math.Min(50, step.zRange.Max); z++)
					{
						reactorCore.AddIfMissing(new Point3D(x, y, z), false);
						reactorCore[new Point3D(x, y, z)] = step.TurnOn;
					}
				}
			}
		}
		var on = reactorCore.Count(x => x.Value);
		return on;
	}

	public override long Part2(string input)
	{
		var rebootSteps = ParseSteps(input);
		var coreCubes = new Cube[0];

		foreach (var step in rebootSteps)
		{
			var newCore = new List<Cube>();
			var newCube = new Cube(step.xRange.Min, step.yRange.Min, step.zRange.Min, step.xRange.Max + 1, step.yRange.Max + 1, step.zRange.Max + 1);
			foreach (var currentCube in coreCubes)
				newCore.AddRange(SubstractCube(currentCube, newCube));
			if (step.TurnOn)
				newCore.Add(newCube);
			coreCubes = newCore.ToArray();
		}
		var on = coreCubes.Sum(x => x.Size());
		return on;
	}

	public static List<Cube> SubstractCube(Cube cube, Cube subtraction)
	{
		var result = new List<Cube>();

		if (!cube.Intersect(subtraction))
			return new List<Cube>() { cube };
		else if (subtraction.Contains(cube))
			return result;

		var xs = new int[] { cube.x0, cube.x1, subtraction.x0, subtraction.x1 }.ToList();
		xs.Sort();
		var ys = new int[] { cube.y0, cube.y1, subtraction.y0, subtraction.y1 }.ToList();
		ys.Sort();
		var zs = new int[] { cube.z0, cube.z1, subtraction.z0, subtraction.z1 }.ToList();
		zs.Sort();

		foreach ((var x0, var x1) in xs.Zip(xs.Skip(1)))
			foreach ((var y0, var y1) in ys.Zip(xs.Skip(1)))
				foreach ((var z0, var z1) in zs.Zip(xs.Skip(1)))
				{
					var cubeCandidate = new Cube(x0, y0, z0, x1, y1, z1);
					if (cube.Contains(cubeCandidate) && !cubeCandidate.Intersect(subtraction))
						result.Add(cubeCandidate);
				}


		return result;
	}


	public List<RebootStep> ParseSteps(string input)
	{
		var result = new List<RebootStep>();
		foreach (var line in input.Split("\n"))
		{
			var splitted = line.Split(" ");
			var on = splitted[0] == "on";
			splitted = splitted[1].Split(",");
			var x = ParseRange(splitted[0]);
			var y = ParseRange(splitted[1]);
			var z = ParseRange(splitted[2]);
			result.Add(new RebootStep(on, x, y, z));
		}

		return result;
	}

	private RangeInt ParseRange(string v)
	{
		var rangePart = v.Split("=")[1];
		var splitted = rangePart.Split("..");
		var a = int.Parse(splitted[0]);
		var b = int.Parse(splitted[1]);

		return new RangeInt(Math.Min(a, b), Math.Max(a, b));
	}

	//on x = 10..10, y = 10..10, z = 10..10
	public record RebootStep(bool TurnOn, RangeInt xRange, RangeInt yRange, RangeInt zRange);

	public record Cube(int x0, int y0, int z0, int x1, int y1, int z1)
	{
		public bool Contains(Day22.Cube other)
		{
			return this.x0 <= other.x0 &&
			this.x1 >= other.x1 &&
			this.y0 <= other.y0 &&
			this.y1 >= other.y1 &&
			this.z0 <= other.z0 &&
			this.z1 >= other.z1;
		}
		public bool Intersect(Day22.Cube other)
		{
			return this.x1 > other.x0 && other.x1 > this.x0 &&
			this.y1 > other.y0 && other.y1 > this.y0 &&
			this.z1 > other.z0 && other.z1 > this.z0;
		}
		public long Size()
		{
			return (this.x1 - this.x0) * (this.y1 - this.y0) * (this.z1 - this.z0);
		}

		public override string ToString() { return $"({x0},{y0},{z0})-({x1},{y1},{z1})"; }
	}
}

