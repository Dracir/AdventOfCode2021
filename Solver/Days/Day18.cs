using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ElfConsole;
using static BinaryTreeUtils;


public class Day18 : DayBase
{
	private static int line = 0;
	public override long Part1(string inputStr)
	{
		var first = inputStr.Split("\n").First();
		var input = ParseInput(inputStr);

		var number = input.First();
		for (int i = 1; i < input.Count; i++)
		{
			number = DoAddition(number, input[i]);
			Console.WriteLineAtLine(number.ToString() ?? "", line++);
		}

		var magnitude = CalculateMagnitude(number);
		return magnitude;
	}

	public override long Part2(string inputStr)
	{
		var first = inputStr.Split("\n").First();
		var input = ParseInput(inputStr);

		var magnitudes = input.PairUpSquare()
			.Where(x => !x.Item1.Equals(x.Item2))
			.Select(x => DoAddition(x.Item1.Clone(), x.Item2.Clone()))
			.Select(x => (x, CalculateMagnitude(x)))
			.OrderByDescending(x => x.Item2)
			.ToList();

		return magnitudes.Max(x => x.Item2);
	}

	private long CalculateMagnitude(BinaryTreeNodeBase<long>? number)
	{
		if (number is BinaryTreeLeaf<long> leaf)
			return leaf.Value;
		else if (number is BinaryTreeNode<long> node)
			return CalculateMagnitude(node.LeftNode) * 3 + CalculateMagnitude(node.RightNode) * 2;
		else
			return 0;
	}

	private static void TestReduce()
	{
		var before = "[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]";
		var after = "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]";
		var actualTree = InputParser.ParseBinaryTreeNode<long>(before, '[', ',', ']', x => long.Parse(x));
		var expectedTree = InputParser.ParseBinaryTreeNode<long>(after, '[', ',', ']', x => long.Parse(x));

		Console.WriteLineAtLine(actualTree.ToString() ?? "", line++);

		while (Reduce(actualTree))
		{
			Console.WriteLineAtLine(actualTree.ToString() ?? "", line++);
		}

		Console.WriteLineAtLine(actualTree.ToString() ?? "", line++);
		line++;
		Console.WriteLineAtLine(expectedTree.ToString() ?? "", line++);
	}

	private void TestAddition()
	{
		var a = "[[[[4,3],4],4],[7,[[8,4],9]]]";
		var b = "[1,1]";
		var after = "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]";

		var aNode = InputParser.ParseBinaryTreeNode<long>(a, '[', ',', ']', x => long.Parse(x));
		var bNode = InputParser.ParseBinaryTreeNode<long>(b, '[', ',', ']', x => long.Parse(x));
		var expectedTree = InputParser.ParseBinaryTreeNode<long>(after, '[', ',', ']', x => long.Parse(x));

		var additionTree = new BinaryTreeNode<long>(null, aNode, bNode);

		Console.WriteLineAtLine(additionTree.ToString() ?? "", line++);

		while (Reduce(additionTree))
		{
			Console.WriteLineAtLine(additionTree.ToString() ?? "", line++);
		}

		Console.WriteLineAtLine(additionTree.ToString() ?? "", line++);
		line++;
		Console.WriteLineAtLine(expectedTree.ToString() ?? "", line++);
	}

	public static BinaryTreeNodeBase<long> DoAddition(BinaryTreeNodeBase<long> aNode, BinaryTreeNodeBase<long> bNode, bool print = false)
	{
		var additionTree = new BinaryTreeNode<long>(null, aNode, bNode);

		while (Reduce(additionTree)) ;

		return additionTree;
	}

	private List<BinaryTreeNodeBase<long>> ParseInput(string inputStr)
	{
		return inputStr.Split("\n")
			.Select(x => InputParser.ParseBinaryTreeNode<long>(x, '[', ',', ']', x => Int64.Parse(x)))
			.ToList();
	}

	public static bool Reduce(BinaryTreeNodeBase<long> number)
	{
		var changed = false;
		changed = false;
		var pairOfLevel4 = FirstChildFromLeft(number, (nodeInfo) => nodeInfo.nestedLevel >= 4 && nodeInfo.Node is BinaryTreeNode<long>);
		if (pairOfLevel4 != null)
		{
			var lowestLeft = LowestLeftLeaf(pairOfLevel4);
			if (lowestLeft != null && lowestLeft.Parent != null)
			{
				ExplodeNode(lowestLeft.Parent);
				changed = true;

			}

		}

		if (!changed)
		{
			var leftestOver10 = FirstChildFromLeft(number, valueIsOver9);
			if (leftestOver10 != null)
			{
				DoSplitNode(leftestOver10);
				changed = true;
			}
		}

		return changed;
	}

	public static void DoSplitNode(BinaryTreeNodeBase<long> nodeOver10)
	{
		if (nodeOver10 is BinaryTreeLeaf<long> over10asLeaf && nodeOver10.Parent != null)
		{
			var newNode = new BinaryTreeNode<long>(nodeOver10.Parent, null, null);
			var leftValue = over10asLeaf.Value / 2;
			var rightValue = over10asLeaf.Value / 2 + over10asLeaf.Value % 2;
			var newLeft = new BinaryTreeLeaf<long>(newNode, leftValue);
			var newRight = new BinaryTreeLeaf<long>(newNode, rightValue);
			newNode.LeftNode = newLeft;
			newNode.RightNode = newRight;

			if (nodeOver10.Parent.LeftNode == nodeOver10)
				nodeOver10.Parent.LeftNode = newNode;
			else if (nodeOver10.Parent.RightNode == nodeOver10)
				nodeOver10.Parent.RightNode = newNode;
		}

	}

	public static bool valueIsOver9(BinaryTreeUtils.NodeInfo<long> nodeInfo)
	{
		if (nodeInfo.Node is BinaryTreeLeaf<long> leaf)
			return leaf.Value >= 10;
		return false;
	}

	public static void ExplodeNode(BinaryTreeNode<long> node)
	{
		long left = 0;
		if (node.LeftNode is BinaryTreeLeaf<long> leftLeaf)
			left = leftLeaf.Value;
		long right = 0;
		if (node.RightNode is BinaryTreeLeaf<long> rightLeaf)
			right = rightLeaf.Value;


		var nodeToTheLeft = GetFirstNodeToLeftOf(node, x => x.Node is BinaryTreeLeaf<long>);
		if (nodeToTheLeft is BinaryTreeLeaf<long> leftNode)
			leftNode.Value += left;

		var nodeToTheRight = GetFirstNodeToRightOf(node, x => x.Node is BinaryTreeLeaf<long>);
		if (nodeToTheRight is BinaryTreeLeaf<long> rightNode)
			rightNode.Value += right;

		var newNode = new BinaryTreeLeaf<long>(node.Parent, 0);
		if (node.Parent != null)
		{
			if (node.Parent.LeftNode == node)
				node.Parent.LeftNode = newNode;
			else if (node.Parent.RightNode == node)
				node.Parent.RightNode = newNode;
		}
	}


}
