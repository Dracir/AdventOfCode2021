
public static class BinaryTreeUtils
{
	public static BinaryTreeNodeBase<T>? Traverse<T>(BinaryTreeNodeBase<T> root, bool[] goRight)
	{
		var currentNode = root;

		for (int i = 0; i < goRight.Length; i++)
		{
			if (currentNode is BinaryTreeNode<T> treeNode)
				currentNode = goRight[i] ? treeNode.RightNode : treeNode.LeftNode;
			else
				return null;
		}
		return currentNode;

	}

	public static BinaryTreeNodeBase<T>? GetFirstNodeToLeftOf<T>(BinaryTreeNodeBase<T> root, Func<NodeInfo<T>, bool> condition)
	{
		if (root.Parent == null || root.Parent.LeftNode == null)
			return null;

		if (root.Parent.LeftNode == root)
			return GetFirstNodeToLeftOf(root.Parent, condition);

		return FirstChildFromRight(root.Parent.LeftNode, condition);
	}

	public static BinaryTreeNodeBase<T>? GetFirstNodeToRightOf<T>(BinaryTreeNodeBase<T> root, Func<NodeInfo<T>, bool> condition)
	{
		if (root.Parent == null || root.Parent.RightNode == null)
			return null;

		if (root.Parent.RightNode == root)
			return GetFirstNodeToRightOf(root.Parent, condition);

		return FirstChildFromLeft(root.Parent.RightNode, condition);
	}

	public record NodeInfo<T>(BinaryTreeNodeBase<T> Node, int nestedLevel);

	public static BinaryTreeNodeBase<T>? FirstChildFromLeft<T>(BinaryTreeNodeBase<T> root, Func<NodeInfo<T>, bool> condition, int nestedLevel = 0)
	{
		if (condition(new NodeInfo<T>(root, nestedLevel)))
			return root;
		if (root is BinaryTreeNode<T> rootNode)
		{
			if (rootNode.LeftNode != null)
			{
				var left = FirstChildFromLeft(rootNode.LeftNode, condition, nestedLevel + 1);
				if (left != null)
					return left;
			}
			if (rootNode.RightNode != null)
			{
				var right = FirstChildFromLeft(rootNode.RightNode, condition, nestedLevel + 1);
				if (right != null)
					return right;
			}
		}

		return null;
	}

	public static BinaryTreeNodeBase<T>? FirstChildFromRight<T>(BinaryTreeNodeBase<T> root, Func<NodeInfo<T>, bool> condition, int nestedLevel = 1)
	{
		if (condition(new NodeInfo<T>(root, nestedLevel)))
			return root;
		if (root is BinaryTreeNode<T> rootNode)
		{
			if (rootNode.RightNode != null)
			{
				var right = FirstChildFromRight(rootNode.RightNode, condition, nestedLevel + 1);
				if (right != null)
					return right;
			}
			if (rootNode.LeftNode != null)
			{
				var left = FirstChildFromRight(rootNode.LeftNode, condition, nestedLevel + 1);
				if (left != null)
					return left;
			}
		}

		return null;
	}

	public static BinaryTreeLeaf<T>? LowestLeftLeaf<T>(BinaryTreeNodeBase<T> root)
	{
		return FirstChildFromLeft(root, (nodeInfo) => nodeInfo.Node is BinaryTreeLeaf<T>) as BinaryTreeLeaf<T>;
	}

	public static BinaryTreeLeaf<T>? LowestRighttLeaf<T>(BinaryTreeNodeBase<T> root)
	{
		return FirstChildFromRight(root, (nodeInfo) => nodeInfo.Node is BinaryTreeLeaf<T>) as BinaryTreeLeaf<T>;
	}
}