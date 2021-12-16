using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryUtils;
using System.Text;

public class Day16 : DayBase
{
	public override long Part1(string inputStr)
	{
		var packet = BITSParser.ParseTransmition(inputStr);
		if (packet is BITSOperatorPacket ope)
		{
			int y = 0;
			foreach (var line in ope.SubPackets)
			{
				var str = $"Operator V{line.Version} -> {GetStr(line)}";
				Console.WriteLineAtLine(str, y++);
			}
		}
		return VersionSum(packet);
	}

	private string GetStr(BITSPacket packet)
	{
		if (packet is BITSLitteralPacket lit)
			return $"Litteral(Ver:{lit.Version}, val:{lit.Value})";
		else if (packet is BITSOperatorPacket ope)
			return string.Join(", ", ope.SubPackets.Select(x => GetStr(x)));
		else
			return "";

	}

	public static long VersionSum(BITSPacket rootPacket)
	{
		if (rootPacket is BITSOperatorPacket operatorPacket)
			return operatorPacket.SubPackets.Sum(x => VersionSum(x)) + rootPacket.Version;
		else
			return rootPacket.Version;
	}



	public override long Part2(string input)
	{
		var packet = BITSParser.ParseTransmition(input);
		return packet.GetValue();
	}
}