using NUnit.Framework;

namespace Tests;

[TestFixture]
public class OtherTest
{
	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void a()
	{
		Assert.Fail();
	}
	[Test]
	public void b()
	{
		Assert.Pass();
	}
}