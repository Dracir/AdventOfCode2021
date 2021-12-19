using System.Collections.Generic;
using NUnit.Framework;


namespace Tests.BinaryTree;

[TestFixture]
public class ParseBinaryTreeNode
{
	[TestCase]
	public void ParseBinaryTreeLeaf()
	{
		var receivedValues = new List<string>();

		var tree = InputParser.ParseBinaryTreeNode<int>("[9,[8,7]]", '[', ',', ']',
			x =>
			{
				receivedValues.Add(x);
				return 0;
			});

		TestUtility.ContainsSameInAnyOrder(new string[] { "9", "8", "7" }, receivedValues, Comparer<string>.Default);
	}

}