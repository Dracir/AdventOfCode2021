
public class BITSLitteralPacket : BITSPacket
{
	public long Value;

	public BITSLitteralPacket(long version, BITSTypeId typeId, long value) : base(version, typeId)
	{
		Value = value;
	}

	public override long GetValue() => Value;
}