
public abstract class BinaryTreeNodeBase<T>
{
	protected BinaryTreeNode<T>? _parent;
	public BinaryTreeNode<T>? Parent
	{
		set
		{
			if (_parent == value)
				return;
			var lastParent = _parent;
			_parent = value;
			if (lastParent != null)
			{
				if (lastParent.LeftNode == this)
					lastParent.LeftNode = null;
				else if (lastParent.RightNode == this)
					lastParent.RightNode = null;
			}
		}
		get => _parent;
	}

	public BinaryTreeNodeBase(BinaryTreeNode<T>? parent = null)
	{
		Parent = parent;
	}

	public override bool Equals(object? obj)
	{
		if (obj == null)
			return false;
		if (obj is BinaryTreeNodeBase<T> other)
			return ToString() == other.ToString();

		return false;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public abstract BinaryTreeNodeBase<T> Clone();
}