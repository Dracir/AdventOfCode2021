using System.Collections.Generic;
using NUnit.Framework;

namespace Tests.BinaryTree;

[TestFixture]
public class GetFirstNodeToLeftOf
{
	[TestCase]
	public void WhenLeftIsJustAValue()
	{
		var tree = InputParser.ParseBinaryTreeNode<int>("[9,[8,7]]", '[', ',', ']', x => int.Parse(x));
		var rightLeft = BinaryTreeUtils.Traverse(tree, new bool[] { true, false });
		var treeLeft = BinaryTreeUtils.Traverse(tree, new bool[] { false });

		if (rightLeft != null)
		{
			var leftOfRight = BinaryTreeUtils.GetFirstNodeToLeftOf(rightLeft, x => x.Node is BinaryTreeLeaf<int>);
			Assert.AreEqual(treeLeft, leftOfRight);
		}
		else
			Assert.Fail("Parsing Failed");
	}

	[TestCase]
	public void WhenLeftIsANode()
	{
		var tree = InputParser.ParseBinaryTreeNode<int>("[[1,9],[8,5]]", '[', ',', ']', x => int.Parse(x));
		var rightLeft = BinaryTreeUtils.Traverse(tree, new bool[] { true, false });
		var treeLeftRight = BinaryTreeUtils.Traverse(tree, new bool[] { false, true });

		if (rightLeft != null)
		{
			var leftOfRight = BinaryTreeUtils.GetFirstNodeToLeftOf(rightLeft, x => x.Node is BinaryTreeLeaf<int>);
			Assert.AreEqual(treeLeftRight, leftOfRight);
		}
		else
			Assert.Fail("Parsing Failed");
	}

}