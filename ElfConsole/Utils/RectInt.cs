public struct RectInt
{
	public int X;
	public int Y;
	public int Width;
	public int Height;
	public int Left => X;
	public int Bottom => Y;
	public int Top => Y + Height;
	public int Righ => X + Width;

	public RectInt(int x, int y, int w, int h)
	{
		X = x;
		Y = y;
		Width = w;
		Height = h;
	}
}
