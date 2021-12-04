using System;

public struct Point
{
	public static Point ZERO = new Point(0, 0);
	public int X;
	public int Y;

	public Point Up { get { return new Point(X, Y + 1); } }
	public Point Down { get { return new Point(X, Y - 1); } }
	public Point Left { get { return new Point(X - 1, Y); } }
	public Point Right { get { return new Point(X + 1, Y); } }

	public Point(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	public static Point operator *(Point pt, int multiplier)
		=> new Point(pt.X * multiplier, pt.Y * multiplier);

	public static Point operator +(Point a, Point b)
		=> new Point(a.X + b.X, a.Y + b.Y);

	public int DistanceManhattan(Point p2)
	{
		return Math.Abs(X - p2.X) + Math.Abs(Y - p2.Y);
	}



	public Point RotateLeft() => new Point(-Y, X);
	public Point RotateRight() => new Point(Y, -X);

	public override int GetHashCode() => HashCode.Combine(X, Y);

	public override string ToString() => $"({X}, {Y})";

	public override bool Equals(object? obj)
	{
		return obj is Point point &&
			   X == point.X && Y == point.Y;
	}

}