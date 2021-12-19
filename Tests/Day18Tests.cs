using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Day18Tests
{
	[TestCase("[[4,3],4]", "[0,7]", new bool[] { false })]
	[TestCase("[[[4,3],4],4]", "[[0,7],4]", new bool[] { false, false })]
	[TestCase("[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", new bool[] { false, true, true, true })]
	public void ExplodeNode(string before, string after, bool[] traversal)
	{
		var actualTree = InputParser.ParseBinaryTreeNode<long>(before, '[', ',', ']', x => long.Parse(x));
		var expectedTree = InputParser.ParseBinaryTreeNode<long>(after, '[', ',', ']', x => long.Parse(x));
		var actual = BinaryTreeUtils.Traverse(actualTree, traversal);

		if (actualTree is BinaryTreeNode<long> actualTreeNode && actual is BinaryTreeNode<long> actualNode)
		{
			Day18.ExplodeNode(actualNode);
			Assert.AreEqual(expectedTree, actualTree);

		}
		else
			Assert.Fail();

	}

	[TestCase("[10,0]", "[[5,5],0]", new bool[] { false })]
	[TestCase("[0,10]", "[0,[5,5]]", new bool[] { true })]
	public void SplitNode(string before, string after, bool[] traversal)
	{
		var actualTree = InputParser.ParseBinaryTreeNode<long>(before, '[', ',', ']', x => long.Parse(x));
		var expectedTree = InputParser.ParseBinaryTreeNode<long>(after, '[', ',', ']', x => long.Parse(x));
		var actual = BinaryTreeUtils.Traverse(actualTree, traversal);

		if (actualTree is BinaryTreeNode<long> actualTreeNode && actual is BinaryTreeLeaf<long> actualNode)
		{
			Day18.DoSplitNode(actualNode);
			Assert.AreEqual(expectedTree, actualTree);
		}
		else
			Assert.Fail();

	}

	[TestCase("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")] //single explode example1
	[TestCase("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")] //single explode example2
	[TestCase("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")] //single explode example3
	[TestCase("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")] //single explode example4
	[TestCase("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]//single explode example5

	[TestCase("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[7,[[8,4],9]]],[1,1]]")] // First Addition Exemple line 2
	[TestCase("[[[[0,7],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[15,[0,13]]],[1,1]]")] // First Addition Exemple line 3
	[TestCase("[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")] // First Addition Exemple line 6
	public void ReduceShouldExplode(string before, string after)
	{
		var actualTree = InputParser.ParseBinaryTreeNode<long>(before, '[', ',', ']', x => long.Parse(x));
		var expectedTree = InputParser.ParseBinaryTreeNode<long>(after, '[', ',', ']', x => long.Parse(x));

		Day18.Reduce(actualTree);
		Assert.AreEqual(expectedTree, actualTree);
	}

	[TestCase("[[[[0,7],4],[15,[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,13]]],[1,1]]")] //First Addition Exemple line 4
	[TestCase("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]", "[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]")] //First Addition Exemple line 5
	public void ReduceShouldSplit(string before, string after)
	{
		var actualTree = InputParser.ParseBinaryTreeNode<long>(before, '[', ',', ']', x => long.Parse(x));
		var expectedTree = InputParser.ParseBinaryTreeNode<long>(after, '[', ',', ']', x => long.Parse(x));

		Day18.Reduce(actualTree);
		Assert.AreEqual(expectedTree, actualTree);
	}

	[TestCase("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
	public void ShouldHaveNoReductions(string before)
	{
		var actualTree = InputParser.ParseBinaryTreeNode<long>(before, '[', ',', ']', x => long.Parse(x));
		var expectedTree = InputParser.ParseBinaryTreeNode<long>(before, '[', ',', ']', x => long.Parse(x));

		while (Day18.Reduce(actualTree)) ;

		Assert.AreEqual(expectedTree, actualTree);
	}

	[TestCase("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")] //First Addition Exemple line 5
	public void FullReduction(string before, string after)
	{
		var actualTree = InputParser.ParseBinaryTreeNode<long>(before, '[', ',', ']', x => long.Parse(x));
		var expectedTree = InputParser.ParseBinaryTreeNode<long>(after, '[', ',', ']', x => long.Parse(x));

		while (Day18.Reduce(actualTree)) ;

		Assert.AreEqual(expectedTree, actualTree);
	}


	[TestCase("[[[[4,3],4],4],[7,[[8,4],9]]]", "[1,1]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")] //First Addition Exemple line 5
	public void Addition(string a, string b, string after)
	{
		var aNode = InputParser.ParseBinaryTreeNode<long>(a, '[', ',', ']', x => long.Parse(x));
		var bNode = InputParser.ParseBinaryTreeNode<long>(b, '[', ',', ']', x => long.Parse(x));
		var expectedTree = InputParser.ParseBinaryTreeNode<long>(after, '[', ',', ']', x => long.Parse(x));

		var additionTree = new BinaryTreeNode<long>(null, aNode, bNode);

		while (Day18.Reduce(additionTree)) ;

		Assert.AreEqual(expectedTree, additionTree);
	}
}