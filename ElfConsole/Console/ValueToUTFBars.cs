


public static class ValueToUTFBars
{
	public enum Styles { Horizontal, Vertical, Shades, Circle, CenteredVerticalBar }

	public static char HorizontalBar(float percent) => HorizontalBar((int)(percent * 8));
	public static char HorizontalBar(int value) => value switch
	{
		0 => ' ',
		1 => '▏',
		2 => '▎',
		3 => '▍',
		4 => '▌',
		5 => '▋',
		6 => '▊',
		7 => '▉',
		8 => '█',
		_ => '█'
	};


	public static char VerticalBar(float percent) => VerticalBar((int)(percent * 8));
	public static char VerticalBar(int value) => value switch
	{
		0 => ' ',
		1 => '▁',
		2 => '▂',
		3 => '▃',
		4 => '▄',
		5 => '▅',
		6 => '▆',
		7 => '▇',
		8 => '█',
		_ => '█'
	};


	public static char CenteredVerticalBar(float percent) => CenteredVerticalBar((int)(percent * 4));
	public static char CenteredVerticalBar(int value) => value switch
	{
		0 => ' ',
		1 => '❘',
		2 => '❙',
		3 => '❚',
		4 => '█',
		_ => '█'
	};



	public static char Shades(float percent) => Shades((int)(percent * 4));
	public static char Shades(int value) => value switch
	{
		0 => ' ',
		1 => '░',
		2 => '▒',
		3 => '▓',
		4 => '█',
		_ => '█'
	};


	public static char Circle(float percent) => Circle((int)(percent * 4));
	public static char Circle(int value) => value switch
	{
		0 => '○',
		1 => '◔',
		2 => '◑',
		3 => '◕',
		4 => '●',
		_ => '●'
	};


	public static char GetChar(float value, Styles style) => style switch
	{
		Styles.Horizontal => HorizontalBar(value),
		Styles.Vertical => VerticalBar(value),
		Styles.Shades => Shades(value),
		Styles.Circle => Circle(value),
		Styles.CenteredVerticalBar => CenteredVerticalBar(value),
		_ => HorizontalBar(value),
	};



}
//public 