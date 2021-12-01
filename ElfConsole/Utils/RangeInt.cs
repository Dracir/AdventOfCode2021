using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class RangeInt
{
	public int Min;
	public int Max;

	public RangeInt(int min, int max)
	{
		this.Min = min;
		this.Max = max;
	}

	public bool Contains(int value) => value >= Min && value <= Max;

}