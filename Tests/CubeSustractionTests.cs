using NUnit.Framework;
using static Day22;

namespace Tests;

[TestFixture]
public class CubeSustractionsTests
{
	[TestCase(-1, -2, -3, 1, 2, 3)]
	[TestCase(0, 1, 2, 10, 10, 10)]
	[TestCase(-10, -10, -10, 10, 10, 10)]
	public void SubstractACubeByItself_ShouldReturnNoCubes(int x0, int y0, int z0, int x1, int y1, int z1)
	{
		var a = new Cube(x0, y0, z0, x1, y1, z1);
		var b = new Cube(x0, y0, z0, x1, y1, z1);

		var result = SubstractCube(a, b);

		Assert.IsEmpty(result);
	}

	[TestCase]
	public void A10by12Cube_Is27OfSize()
	{
		var cube = new Cube(10, 10, 10, 12 + 1, 12 + 1, 12 + 1);
		Assert.AreEqual(27, cube.Size());
	}

}