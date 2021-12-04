using Console = ElfConsole;

public static class Day3Console
{
	private static int baseX = 10;
	private static int gammaY = 0;
	private static int epsilonY = 5;

	public class P1
	{
		private ArrayPrintedValue _gammaCount;
		private ArrayPrintedValue _gammaBits;
		private ArrayPrintedValue _epsilonCount;
		private ArrayPrintedValue _epsilonBits;

		public P1()
		{
			DrawingUtils.DrawBox(0, gammaY, 58, 5, "Gamma Rate", ConsoleColor.Black, ConsoleColor.Gray, ConsoleColor.White);
			Console.WriteAt("1 bits :", 1, gammaY + 1);
			Console.WriteAt("bit    :", 1, gammaY + 2);
			Console.WriteAt("value  :", 1, gammaY + 3);
			_gammaCount = new ArrayPrintedValue(new Point(baseX, gammaY + 1), 3, 12, 1, ConsoleColor.Gray);
			_gammaBits = new ArrayPrintedValue(new Point(baseX, gammaY + 2), 3, 12, 1, ConsoleColor.Gray);

			DrawingUtils.DrawBox(0, epsilonY, 58, 5, "Epsilon Rate", ConsoleColor.Black, ConsoleColor.Gray, ConsoleColor.White);
			Console.WriteAt("1 bits :", 1, epsilonY + 1);
			Console.WriteAt("bit    :", 1, epsilonY + 2);
			Console.WriteAt("value  :", 1, epsilonY + 3);
			_epsilonCount = new ArrayPrintedValue(new Point(baseX, epsilonY + 1), 3, 12, 1, ConsoleColor.Gray);
			_epsilonBits = new ArrayPrintedValue(new Point(baseX, epsilonY + 2), 3, 12, 1, ConsoleColor.Gray);
		}

		public void SetGammaEpsilon(IEnumerable<bool> gammaBits, long gamma, IEnumerable<bool> epsilonBits, long epsilon)
		{
			_gammaBits.SetValue(gammaBits.Select(x => x ? 1 : 0).ToArray());
			_epsilonBits.SetValue(epsilonBits.Select(x => x ? 1 : 0).ToArray());
			Console.WriteAt(gamma.ToString(), 10, gammaY + 3);
			Console.WriteAt(epsilon.ToString(), 10, epsilonY + 3);
		}

		public void Set1Bits(IEnumerable<int> oneCount, int totalValues)
		{
			_gammaCount.SetValue(oneCount.ToArray());
			_epsilonCount.SetValue(oneCount.Select(x => totalValues - x).ToArray());
		}
	}

}