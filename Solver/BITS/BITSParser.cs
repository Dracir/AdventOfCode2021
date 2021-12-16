
using System.Text;

public static class BITSParser
{

	public static BITSPacket ParseTransmition(string transmition)
	{
		var inputBits = ParseHexToBin(transmition);
		int pointer = 0;
		return ParseBITS(inputBits, ref pointer);
	}

	public static BITSPacket ParseBITS(string inputBits, ref int pointer)
	{
		var version = ParseBinToLong(inputBits[pointer..(pointer += 3)]);
		var typeID = ParseTypeID(inputBits[pointer..(pointer += 3)]);
		if (typeID == BITSTypeId.LITTERAL)
			return ParseLitteral(inputBits, ref pointer, version, typeID);
		else return ParseOperator(inputBits, ref pointer, version, typeID);
	}

	private static BITSLitteralPacket ParseLitteral(string inputBits, ref int pointer, long version, BITSTypeId typeID)
	{
		string group = "";
		string binValue = "";
		do
		{
			group = inputBits[pointer..(pointer += 5)];
			binValue += group[1..5];
		} while (group[0] == '1');
		var value = ParseBinToLong(binValue);
		return new BITSLitteralPacket(version, typeID, value);
	}

	private static BITSPacket ParseOperator(string inputBits, ref int pointer, long version, BITSTypeId typeID)
	{
		var lengthType = inputBits[pointer++];
		var subPackets = new List<BITSPacket>();
		var subPointer = 0;

		if (lengthType == '0')
		{
			var totalLengthInBits = ParseBinToToInt32(inputBits[pointer..(pointer += 15)]);
			var targetPointer = pointer + totalLengthInBits;
			do
			{
				subPackets.Add(ParseBITS(inputBits[pointer..targetPointer], ref subPointer));
			} while (subPointer < totalLengthInBits);
		}
		else
		{
			var numberOfSubPackets = ParseBinToToInt32(inputBits[pointer..(pointer += 11)]);
			while (subPackets.Count != numberOfSubPackets)
				subPackets.Add(ParseBITS(inputBits[pointer..], ref subPointer));
		}
		pointer += subPointer;
		return new BITSOperatorPacket(version, typeID, subPackets.ToArray());
	}

	private static BITSTypeId ParseTypeID(string bits)
	{
		var intValue = ParseBinToLong(bits);
		var value = Enum.GetValues(typeof(BITSTypeId))
						 .GetValue(intValue);
		if (value == null) // Should not happen please
			return BITSTypeId.LITTERAL;
		return (BITSTypeId)value;
	}

	private static long ParseBinToLong(string bits) => Convert.ToInt64(bits, 2);
	private static int ParseBinToToInt32(string bits) => Convert.ToInt32(bits, 2);

	private static string ParseHexToBin(string inputStr)
	{
		var bin = new StringBuilder();

		foreach (var hexCode in inputStr)
		{
			var c = hexCode switch
			{
				'1' => "0001",
				'2' => "0010",
				'3' => "0011",
				'4' => "0100",
				'5' => "0101",
				'6' => "0110",
				'7' => "0111",
				'8' => "1000",
				'9' => "1001",
				'A' => "1010",
				'B' => "1011",
				'C' => "1100",
				'D' => "1101",
				'E' => "1110",
				'F' => "1111",
				_ => "0000",
			};
			bin.Append(c);
		}

		return bin.ToString();
	}
}