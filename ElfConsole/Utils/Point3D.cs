using System;

public struct Point3D
{
	public static Point3D ZERO = new Point3D(0, 0, 0);
	public int X;
	public int Y;
	public int Z;

	public Point3D(int x, int y, int z)
	{
		this.X = x;
		this.Y = y;
		this.Z = z;
	}

	public int DistanceManhattan(Point3D p2)
	{
		return Math.Abs(X - p2.X) + Math.Abs(Y - p2.Y) + Math.Abs(Z - p2.Z);
	}

	public override bool Equals(object? obj)
	{
		return obj is Point3D d &&
			   X == d.X &&
			   Y == d.Y &&
			   Z == d.Z;
	}

	public override int GetHashCode() => HashCode.Combine(X, Y, Z);

	public override string ToString() => $"({X}, {Y}, {Z})";
}