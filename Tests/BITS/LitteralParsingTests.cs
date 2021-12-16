using NUnit.Framework;

namespace Tests;

[TestFixture]
public class LitteralParsingTests
{
	[TestCase("D2FE28", 6, 2021)]
	public void ParseLitteral(string hexTransmition, int version, long value)
	{
		var packet = BITSParser.ParseTransmition(hexTransmition);

		Assert.AreEqual(version, packet.Version);
		Assert.AreEqual(BITSTypeId.LITTERAL, packet.TypeId);
		var litteral = (BITSLitteralPacket)packet;
		Assert.AreEqual(value, litteral.Value);
	}

	[TestCase("38006F45291200", 2)]
	public void ParseOperatorHiearchy_LengthType0(string hexTransmition, int nbSubPackets)
	{
		var packet = BITSParser.ParseTransmition(hexTransmition);

		Assert.AreNotEqual(BITSTypeId.LITTERAL, packet.TypeId);
		var operatorPacket = (BITSOperatorPacket)packet;
		Assert.AreEqual(nbSubPackets, operatorPacket.SubPackets.Length);
	}

	[TestCase("EE00D40C823060", 3)]
	public void ParseOperatorHiearchy_LengthType1(string hexTransmition, int nbSubPackets)
	{
		var packet = BITSParser.ParseTransmition(hexTransmition);

		Assert.AreNotEqual(BITSTypeId.LITTERAL, packet.TypeId);
		var operatorPacket = (BITSOperatorPacket)packet;
		Assert.AreEqual(nbSubPackets, operatorPacket.SubPackets.Length);
	}

	[TestCase("8A004A801A8002F478", 16)]
	[TestCase("620080001611562C8802118E34", 12)]
	[TestCase("C0015000016115A2E0802F182340", 23)]
	[TestCase("A0016C880162017C3686B18A3D4780", 31)]
	public void VersionSum(string hexTransmition, int expectedSum)
	{
		var packet = BITSParser.ParseTransmition(hexTransmition);
		var sum = Day16.VersionSum(packet);

		Assert.AreEqual(expectedSum, sum);
	}

	[TestCase("C200B40A82", 3)] // SUM
	[TestCase("04005AC33890", 54)] // Product
	[TestCase("880086C3E88112", 7)]//Min
	[TestCase("CE00C43D881120", 9)]//max
	[TestCase("D8005AC2A8F0", 1)]// lessthen
	[TestCase("F600BC2D8F", 0)]// moreThen
	[TestCase("9C005AC2F8F0", 0)]// equal
	[TestCase("9C0141080250320F1802104A08", 1)]// 1 + 3 = 2 * 2
	public void GetValues(string hexTransmition, int expectedValue)
	{
		var packet = BITSParser.ParseTransmition(hexTransmition);
		var sum = packet.GetValue();

		Assert.AreEqual(expectedValue, sum);
	}
}