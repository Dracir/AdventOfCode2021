using System.Collections.Generic;
using NUnit.Framework;

namespace Tests.BinaryTree;

[TestFixture]
public class GetFirstNodeToRightOf
{
	[TestCase]
	public void WhenRightIsJustAValue()
	{
		var tree = InputParser.ParseBinaryTreeNode<int>("[9,[8,7]]", '[', ',', ']', x => int.Parse(x));
		var rightLeft = BinaryTreeUtils.Traverse(tree, new bool[] { true, false });
		var treeLeft = BinaryTreeUtils.Traverse(tree, new bool[] { false });

		if (treeLeft != null)
		{
			var rightofLeft = BinaryTreeUtils.GetFirstNodeToRightOf(treeLeft, x => x.Node is BinaryTreeLeaf<int>);
			Assert.AreEqual(rightLeft, rightofLeft);
		}
		else
			Assert.Fail("Parsing Failed");
	}

	[TestCase]
	public void WhenRightIsANode()
	{
		var tree = InputParser.ParseBinaryTreeNode<int>("[[1,9],[8,5]]", '[', ',', ']', x => int.Parse(x));
		var rightLeft = BinaryTreeUtils.Traverse(tree, new bool[] { true, false });
		var treeLeftRight = BinaryTreeUtils.Traverse(tree, new bool[] { false, true });

		if (treeLeftRight != null)
		{
			var leftOfRight = BinaryTreeUtils.GetFirstNodeToRightOf(treeLeftRight, x => x.Node is BinaryTreeLeaf<int>);
			Assert.AreEqual(rightLeft, leftOfRight);
		}
		else
			Assert.Fail("Parsing Failed");
	}

}