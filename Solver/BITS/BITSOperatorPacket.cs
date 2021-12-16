
public class BITSOperatorPacket : BITSPacket
{
	public BITSPacket[] SubPackets;

	public BITSOperatorPacket(long version, BITSTypeId typeId, BITSPacket[] subPackets) : base(version, typeId)
	{
		SubPackets = subPackets;
	}

	public override long GetValue() => TypeId switch
	{
		BITSTypeId.SUM => GetSum(),
		BITSTypeId.PRODUCT => GetProduct(),
		BITSTypeId.MINIMUM => GetMinimum(),
		BITSTypeId.MAXIMUM => GetMaximum(),
		BITSTypeId.GREATER_THAN => SubPackets[0].GetValue() > SubPackets[1].GetValue() ? 1 : 0,
		BITSTypeId.LESS_THEN => SubPackets[0].GetValue() < SubPackets[1].GetValue() ? 1 : 0,
		BITSTypeId.EQUAL_TO => SubPackets[0].GetValue() == SubPackets[1].GetValue() ? 1 : 0,
		_ => 0
	};

	private long GetProduct() => SubPackets.Product(x => x.GetValue());

	private long GetSum() => SubPackets.Sum(x => x.GetValue());
	private long GetMinimum() => SubPackets.Min(x => x.GetValue());
	private long GetMaximum() => SubPackets.Max(x => x.GetValue());
}