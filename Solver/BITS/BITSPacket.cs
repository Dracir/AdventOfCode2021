
public abstract class BITSPacket
{
	public long Version;
	public BITSTypeId TypeId;

	protected BITSPacket(long version, BITSTypeId typeId)
	{
		Version = version;
		TypeId = typeId;
	}

	public abstract long GetValue();

}