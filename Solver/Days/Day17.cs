using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;


public class Day17 : DayBase
{
	public override long Part1(string input)
	{
		var target = new RectInt(135, -102, 20, 102 - 78);
		//var target = new RectInt(20, -10, 10, 5);
		var best = 0;

		var validXVelocities = GetValidXVelocities(target);
		var validYVelocities = GetValidYVelocities(target, false);
		foreach (var velocityX in validXVelocities)
		{
			foreach (var velocityY in validYVelocities)
			{
				var touched = HitsTarget(target, new Point(velocityX, velocityY), out var y);
				if (touched)
					best = Math.Max(best, y);
			}
		}
		return best;
	}

	private List<int> GetValidXVelocities(RectInt target)
	{
		var valid = new List<int>();
		for (int velocityX = 1; velocityX < target.X + target.Width + -target.Y; velocityX++) // target.X - target.Width ???
		{
			var x = 0;
			var simulationVelocity = velocityX;
			while (x < target.Right && simulationVelocity != 0)
			{
				x += simulationVelocity;
				if (simulationVelocity != 0)
					simulationVelocity -= Math.Sign(simulationVelocity);
				if (x >= target.Left && x <= target.Right)
				{
					valid.Add(velocityX);
					break;
				}
			}

		}
		return valid;
	}

	private List<int> GetValidYVelocities(RectInt target, bool includeDown)
	{
		var valid = new List<int>();
		var minVeloY = includeDown ? target.Bottom - 1 : 1;
		for (int velocityY = minVeloY; velocityY < 1000; velocityY++)
		{
			var y = 0;
			var simulationVelocity = velocityY;
			while (y > target.Bottom)
			{
				y += simulationVelocity;
				simulationVelocity -= 1;
				if (y >= target.Bottom && y <= target.Top)
				{
					valid.Add(velocityY);
					break;
				}
			}

		}
		return valid;
	}

	public bool HitsTarget(RectInt target, Point velocity, out int highestY)
	{
		var x = 0;
		var y = 0;
		highestY = 0;

		do
		{
			x += velocity.X;
			y += velocity.Y;
			if (velocity.X != 0)
				velocity.X -= Math.Sign(velocity.X);
			velocity.Y -= 1;

			if (highestY < y)
				highestY = y;
			if (target.Contains(x, y))
				return true;

		} while (y > target.Bottom);

		return false;
	}

	public override long Part2(string input)
	{
		var target = new RectInt(135, -102, 20, 102 - 78);
		//var target = new RectInt(20, -10, 10, 5);
		var count = 0;

		var validXVelocities = GetValidXVelocities(target);
		var validYVelocities = GetValidYVelocities(target, true);
		foreach (var velocityX in validXVelocities)
		{
			foreach (var velocityY in validYVelocities)
			{
				var touched = HitsTarget(target, new Point(velocityX, velocityY), out var y);
				if (touched)
					count++;
			}
		}
		return count;
	}
}