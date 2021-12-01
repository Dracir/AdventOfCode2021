using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;


public struct NodeGroup
{
	public Dictionary<string, Node> Nodes;

	public NodeGroup(Dictionary<string, Node> nodes)
	{
		Nodes = nodes;
	}
}

public class Node
{
	public string Name;
	public int Quantity;
	public Node? Parent;
	public List<Node> Children = new List<Node>();

	public Node(string name, int quantity = 1)
	{
		Name = name;
		Quantity = quantity;
	}
}