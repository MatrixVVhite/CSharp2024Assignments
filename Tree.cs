using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace TheTreeDataStructure
{
	public class Tree<T> : IEnumerable<T>
	{
		public Node root;
		public bool enumerateBreadthFirst = false;

		public Tree(T val) : this(new Node(val)) { }

		public Tree(Node root)
		{
			this.root = root;
		}

		#region NODE
		public struct Node
		{
			public T value;
			public List<Node>? children;

			public readonly bool HasChildren => children != null;

			public readonly bool IsLeaf => !HasChildren;

			public Node ([DisallowNull] T value)
			{
				this.value = value;
				children = null;
			}

			public Node([DisallowNull] T value, params T[] children)
			{
				this.value = value;
				this.children = children.Select((t, _) => new Node(t)).ToList();
			}

			public Node([DisallowNull] T value, params Node[] children)
			{
				this.value = value;
				this.children = children.ToList();
			}

			public void AddChild(Node node)
			{
				children ??= new(1);
				children.Add(node);
			}

			public void AddChild([DisallowNull] T val)
			{
				AddChild(new Node(val));
			}

			public override string ToString() => value.ToString();
		}
		#endregion

		#region ENUMERATION
		public IEnumerator<T> GetEnumerator() => new Enumerator(this, enumerateBreadthFirst);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public class Enumerator : IEnumerator<T>
		{
			protected List<T> _values;
			protected int _index;

			public T Current => _values[_index];
			object IEnumerator.Current => Current;

			public Enumerator(Tree<T> tree, bool breadthFirst)
			{
				_values = new(1);
				SetNodes(tree.root, breadthFirst);
				_index = -1;
			}

			public void Dispose() { }

			public bool MoveNext()
			{
				_index++;
				return _index < _values.Count;
			}

			public void Reset()
			{
				_index = -1;
			}

			private void SetNodes(Node node, bool breadthFirst)
			{
				if (breadthFirst)
					SetNodesBreadthFirst(node);
				else
					SetNodesDepthFirst(node);
			}

			private void SetNodesDepthFirst(Node node)
			{
				_values.Add(node.value);
				if (node.IsLeaf)
					return;
				foreach (Node child in node.children!)
					SetNodesDepthFirst(child);
			}

			private void SetNodesBreadthFirst(Node node)
			{
				List<(Node, int)> nodes = new();
				Tree<T>.Enumerator.GetNodesWithDepth(node, ref nodes, 0);
				_values = nodes.OrderBy(ni => ni.Item2).Select(ni => ni.Item1.value).ToList();
			}

			private static void GetNodesWithDepth(Node currentNode, ref List<(Node, int)> nodes, int currentDepth)
			{
				nodes.Add((currentNode, currentDepth));
				if (currentNode.IsLeaf)
					return;
				foreach (Node child in currentNode.children!)
					Tree<T>.Enumerator.GetNodesWithDepth(child, ref nodes, currentDepth+1);
			}
		}
		#endregion
	}
}
