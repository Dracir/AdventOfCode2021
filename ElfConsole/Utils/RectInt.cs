public struct RectInt
{
	public int X;
	public int Y;
	public int Width;
	public int Height;
	public int Left => X;
	public int Bottom => Y;
	public int Top => Y + Height;
	public int Right => X + Width;

	public RangeInt WidthRange => new RangeInt(0, Width);
	public RangeInt HeightRange => new RangeInt(0, Height);

	public RectInt(int x, int y, int w, int h)
	{
		X = x;
		Y = y;
		Width = w;
		Height = h;
	}

	public bool Contains(Point point) => Contains(point.X, point.Y);

	public bool Contains(int x, int y) => x >= X && x <= X + Width && y >= Y && y <= Y + Height;
}
