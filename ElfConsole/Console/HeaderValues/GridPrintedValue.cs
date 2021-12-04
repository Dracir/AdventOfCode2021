using System;
using System.IO;
using System.Collections.Generic;

public class GridPrintedValue
{
	private int _width;
	private ArrayPrintedValue[] _arrayPrintedValues;

	public GridPrintedValue(Point position, int cellWidth, int nbCells, int cellSpacing, int nbRows, ConsoleColor color, string? format = null)
	{
		_width = cellWidth;
		_arrayPrintedValues = new ArrayPrintedValue[nbRows];
		for (int i = 0; i < nbRows; i++)
			_arrayPrintedValues[i] = new ArrayPrintedValue(position + new Point(0, i), cellWidth, nbCells, cellSpacing, color, format);
	}


	public void SetValue(int[,] values)
	{
		for (int i = 0; i < MathF.Min(values.GetLength(0), _arrayPrintedValues.Length); i++)
		{
			var row = Enumerable.Range(0, values.GetLength(1))
				.Select(x => values[i, x]).ToArray();
			_arrayPrintedValues[i].SetValue(row);
		}
	}

	public void SetValue(float[,] values)
	{
		for (int i = 0; i < MathF.Min(values.GetLength(0), _arrayPrintedValues.Length); i++)
		{
			var row = Enumerable.Range(0, values.GetLength(1))
				.Select(x => values[i, x]).ToArray();
			_arrayPrintedValues[i].SetValue(row);
		}
	}

	public void SetValue(string[,] values)
	{
		for (int i = 0; i < MathF.Min(values.Length, _arrayPrintedValues.Length); i++)
		{
			var row = Enumerable.Range(0, values.GetLength(1))
				.Select(x => values[i, x]).ToArray();
			_arrayPrintedValues[i].SetValue(row);
		}
	}
}