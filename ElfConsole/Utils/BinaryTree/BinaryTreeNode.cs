
public class BinaryTreeNode<T> : BinaryTreeNodeBase<T>
{
	protected BinaryTreeNodeBase<T>? _leftNode;
	protected BinaryTreeNodeBase<T>? _rightNode;
	public T? Value;


	public BinaryTreeNodeBase<T>? LeftNode
	{
		set
		{
			if (_leftNode == value)
				return;
			var lastValue = _leftNode;
			_leftNode = value;
			if (_leftNode != null)
				_leftNode.Parent = this;
			if (lastValue != null)
				lastValue.Parent = null;
		}
		get => _leftNode;
	}

	public BinaryTreeNodeBase<T>? RightNode
	{
		set
		{
			if (_rightNode == value)
				return;
			var lastValue = _rightNode;
			if (_rightNode != null)
				_rightNode.Parent = null;
			_rightNode = value;
			if (_rightNode != null)
				_rightNode.Parent = this;
			if (lastValue != null)
				lastValue.Parent = null;
		}
		get => _rightNode;
	}


	public BinaryTreeNode(BinaryTreeNode<T>? parent, BinaryTreeNodeBase<T>? leftNode, BinaryTreeNodeBase<T>? rightNode, T? value = default(T))
	: base(parent)
	{
		LeftNode = leftNode;
		if (LeftNode != null)
			LeftNode.Parent = this;
		RightNode = rightNode;
		if (RightNode != null)
			RightNode.Parent = this;
		Value = value;
	}

	public override string ToString()
	{
		var left = LeftNode == null ? "" : LeftNode.ToString() ?? "";
		var right = RightNode == null ? "" : RightNode.ToString() ?? "";
		return $"[{left},{right}]";
	}

	public override BinaryTreeNodeBase<T> Clone()
	{
		BinaryTreeNodeBase<T>? left = null;
		BinaryTreeNodeBase<T>? right = null;

		if (_leftNode != null)
			left = _leftNode.Clone();
		if (_rightNode != null)
			right = _rightNode.Clone();

		var clone = new BinaryTreeNode<T>(null, left, right, Value);
		if (left != null)
			left.Parent = clone;
		if (right != null)
			right.Parent = clone;
		return clone;
	}
}