
using System.Collections.Generic;

public interface IGrid<T>
{
	int UsedMinX { get; }
	int UsedMinY { get; }
	int UsedMaxX { get; }
	int UsedMaxY { get; }
	int MinX { get; }
	int MinY { get; }
	int MaxX { get; }
	int MaxY { get; }
	int UsedWidth { get; }
	int UsedHeight { get; }
	int FullWidth { get; }
	int FullHeight { get; }

	Point TopLeft { get; }
	Point TopRight { get; }
	Point BottomLeft { get; }
	Point BottomRight { get; }
	Point Center { get; }

	T this[Point key] { get; set; }
	T this[int x, int y] { get; set; }


	IEnumerable<Point> Points();
	IEnumerable<Point> AreaSquareAround(Point pt, int radiusDistance);
	IEnumerable<Point> AreaAround(Point pt, int manhattanDistance);
	IEnumerable<int> ColumnIndexs();
	IEnumerable<int> RowIndexs();
	T[,] ToArray();

	void AddGrid(int leftX, int bottomY, T[,] grid, GridAxes axes);

	bool XInBound(int x);
	bool YInBound(int y);
	bool PointInBound(Point pt);

}
public enum GridAxes { XY, YX };