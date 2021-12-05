
public enum Direction { North = 0, Est = 1, South = 2, West = 3 }

public static class DirectionExtentions
{
	public static Direction Left(this Direction direction) => (Direction)(((int)direction + 3) % 4);
	public static Direction Right(this Direction direction) => (Direction)(((int)direction + 1) % 4);
	public static Point ToPoint(this Direction direction) => direction switch
	{
		Direction.North => new Point(0, -1),
		Direction.Est => new Point(1, 0),
		Direction.South => new Point(0, 1),
		Direction.West => new Point(-1, 0),
		_ => new Point(0, 0),
	};
}
