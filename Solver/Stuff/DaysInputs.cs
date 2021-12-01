using System.IO;

public static class DaysInputs
{
	public static string D1 => ReadInput(1);
	public static string D2 => ReadInput(2);
	public static string D3 => ReadInput(3);
	public static string D4 => ReadInput(4);
	public static string D5 => ReadInput(5);
	public static string D6 => ReadInput(6);
	public static string D7 => ReadInput(7);
	public static string D8 => ReadInput(8);
	public static string D9 => ReadInput(9);
	public static string D10 => ReadInput(10);
	public static string D11 => ReadInput(11);
	public static string D12 => ReadInput(12);
	public static string D13 => ReadInput(13);
	public static string D14 => ReadInput(14);
	public static string D15 => ReadInput(15);
	public static string D16 => ReadInput(16);
	public static string D17 => ReadInput(17);
	public static string D18 => ReadInput(18);
	public static string D19 => ReadInput(19);
	public static string D20 => ReadInput(20);
	public static string D21 => ReadInput(21);
	public static string D22 => ReadInput(22);
	public static string D23 => ReadInput(23);
	public static string D24 => ReadInput(24);
	public static string D25 => ReadInput(25);

	public static string ReadInput(int day) => File.ReadAllText($"DaysInput/Day{day}.txt").Replace("\r", "");
	public static string ReadInput(string fileName) => File.ReadAllText($"DaysInput/{fileName}.txt").Replace("\r", "");

}