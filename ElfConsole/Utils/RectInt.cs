public struct RectInt
{
	public int X;
	public int Y;
	public int Width;
	public int Height;
	public int Left => X;
	public int Bottom => Y;
	public int Top => Y + Height - 1;
	public int Right => X + Width - 1;

	public RangeInt WidthRange => new RangeInt(0, Width);
	public RangeInt HeightRange => new RangeInt(0, Height);

	public RectInt(int x, int y, int w, int h)
	{
		X = x;
		Y = y;
		Width = w;
		Height = h;
	}

	public RectInt Growen(int left, int top, int right, int bottom)
	{
		return new RectInt(X - left, Y - bottom, Width + left + right, Height + bottom + top);
	}

	public bool IsOnBorder(Point pt)
	{
		if (!Contains(pt)) return false;

		return pt.X == X || pt.X == Right || pt.Y == Y || pt.Y == Top;
	}

	public void GrowToInclude(Point point)
	{
		if (X > point.X)
		{
			var growth = X - point.X;
			X -= growth;
			Width += growth;
		}
		else if (X + Width <= point.X)
		{
			var growth = point.X - X - Width + 1;
			Width += growth;
		}

		if (Y > point.Y)
		{
			var growth = Y - point.Y;
			Y -= growth;
			Height += growth;
		}
		else if (Y + Height <= point.Y)
		{
			var growth = point.Y - Y - Height + 1;
			Height += growth;
		}
	}

	public bool Contains(Point point) => Contains(point.X, point.Y);

	public bool Contains(int x, int y) => x >= X && x <= Right && y >= Y && y <= Top;
}
