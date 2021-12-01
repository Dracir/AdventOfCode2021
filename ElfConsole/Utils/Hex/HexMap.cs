
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HexMap<T> : MultiDimentionalArray<T>
{

	public HexMap(T defaultValue) : base(defaultValue, 2)
	{
		CreateOnGet = true;
	}

	public T? this[int col, int row]
	{
		get => base[new int[] { col, row }];
		/*{
			var key = new int[] { col, row };
			if (base.KeyExist(key))
				return base[key];
			else
			{
				var newHex = new Hex<T>(col, row, this);
				base[key] = newHex;
				return newHex;
			}
		}*/
		set => base[new int[] { col, row }] = value;
	}

	public T? this[Point point]
	{
		get => base[new int[] { point.X, point.Y }];
		set => base[new int[] { point.X, point.Y }] = value;
	}

	public Hex<T> GetHex(int col, int row) => new Hex<T>(col, row, this, this[col, row]);

	public bool XInBound(int x) => x.Between(MinKeys[0], MaxKeys[0]);
	public bool YInBound(int y) => y.Between(MinKeys[1], MaxKeys[1]);

	public int Width => Size[0];
	public int Height => Size[1];




}

public class Hex<T>
{
	private T? _value;
	public T? Value
	{
		set
		{
			_value = value;
			Map[Q, R] = value;
		}
		get => _value;
	}
	public readonly HexMap<T> Map;


	public readonly Point Point;
	public int Q => Point.X;
	public int R => Point.Y;

	internal Hex(int q, int r, HexMap<T> map, T? value) : this(q, r, map)
	{
		Value = value;
	}

	protected Hex(int q, int r, HexMap<T> map)
	{
		Map = map;
		Point = new Point(q, r);
	}


	public Hex<T> East => Map.GetHex(Q + 1, R);
	public Hex<T> SouthEast => Map.GetHex(Q + ((Math.Abs(R) + 1) % 2), R - 1);
	public Hex<T> SouthWest => Map.GetHex(Q - (Math.Abs(R) % 2), R - 1);
	public Hex<T> West => Map.GetHex(Q - 1, R);
	public Hex<T> NorthEast => Map.GetHex(Q + ((Math.Abs(R) + 1) % 2), R + 1);
	public Hex<T> NorthWest => Map.GetHex(Q - (Math.Abs(R) % 2), R + 1);

	public Hex<T>[] GetNeighbors() => new Hex<T>[] { East, SouthEast, SouthWest, West, NorthEast, NorthWest };

	public KeyValuePair<int[], T?> GetKeyValuePair() => new KeyValuePair<int[], T?>(new int[] { Q, R }, Value);
}