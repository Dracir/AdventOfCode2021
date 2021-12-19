
public class BinaryTreeLeaf<T> : BinaryTreeNodeBase<T>
{
	public T? Value;

	public BinaryTreeLeaf(BinaryTreeNode<T>? parent, T? value = default(T))
	: base(parent)
	{
		Value = value;
	}

	public override string ToString()
	{
		return Value == null ? "" : Value.ToString() ?? "";
	}

	public override BinaryTreeNodeBase<T> Clone()
	{
		return new BinaryTreeLeaf<T>(null, Value);
	}
}