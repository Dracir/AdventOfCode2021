using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class PointHelper
{
	public static readonly Point[] _8DirectionPoints = new Point[]{
		new Point(0,1),
		new Point(1,1),
		new Point(1,0),
		new Point(1,-1),
		new Point(0,-1),
		new Point(-1,-1),
		new Point(-1,0),
		new Point(-1,1),
	};
}