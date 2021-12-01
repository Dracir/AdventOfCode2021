using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;


public static class NodeUtils
{
	public static int NodesThatContainKey(NodeGroup group, string key)
	{
		return group.Nodes.Count(keyValue => NodeContainsKey(group, keyValue.Value, key));
	}

	public static bool NodeContainsKey(NodeGroup group, Node node, string key)
	{
		foreach (var child in node.Children)
		{
			if (child.Name == key)
				return true;
			else if (NodeContainsKey(group, group.Nodes[child.Name], key))
				return true;
		}
		return false;
	}

	public static Node MakeTreeOfNode(NodeGroup group, string key)
	{
		var node = new Node(key, 1);
		foreach (var child in group.Nodes[key].Children)
		{
			var childNode = MakeTreeOfNode(group, child.Name);
			childNode.Quantity = child.Quantity;
			childNode.Parent = node;

			node.Children.Add(childNode);
		}

		return node;
	}

	public static int CountTreeQuantities(Node tree)
	{
		var quantity = 1;
		foreach (var child in tree.Children)
			if (child.Children.Count == 0)
				quantity += child.Quantity;
			else
				quantity += child.Quantity * CountTreeQuantities(child);
		return quantity;
	}
}