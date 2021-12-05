public class Line
{
	public Point A;
	public Point B;

	public Line(Point a, Point b)
	{
		A = a;
		B = b;
	}

	public int MinX => Math.Min(A.X, B.X);
	public int MinY => Math.Min(A.Y, B.Y);
	public int MaxX => Math.Max(A.X, B.X);
	public int MaxY => Math.Max(A.Y, B.Y);
	public int Width => Math.Abs(A.X - B.X) + 1;
	public int Height => Math.Abs(A.Y - B.Y) + 1;

	public bool IsHorizontal => A.Y == B.Y;
	public bool IsVertical => A.X == B.X;
	public bool IsDiagonal => Width == Height;
	public StarDirection? GetStarDirection()
	{
		if (!IsDiagonal) return null;
		if (A.Y < B.Y && A.X > B.X) return StarDirection.NorthWest;
		if (A.Y < B.Y && A.X < B.X) return StarDirection.NorthEast;
		if (A.Y > B.Y && A.X > B.X) return StarDirection.SouthWest;
		if (A.Y > B.Y && A.X < B.X) return StarDirection.SouthEast;
		return null;
	}

	public IEnumerable<Point> Points()
	{
		if (IsHorizontal)
		{
			foreach (var x in Enumerable.Range(MinX, Width))
				yield return new Point(x, A.Y);
		}
		else if (IsVertical)
		{
			foreach (var y in Enumerable.Range(MinY, Height))
				yield return new Point(A.X, y);
		}
		else if (IsDiagonal)
		{
			var xFactor = A.X > B.X ? -1 : 1;
			var yFactor = A.Y > B.Y ? -1 : 1;
			foreach (var i in Enumerable.Range(0, Height))
				yield return new Point(A.X + xFactor * i, A.Y + yFactor * i);
		}
	}

}