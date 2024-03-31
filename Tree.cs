using System.Collections;

namespace TheTreeDataStructure
{
	public class Tree<T> : IEnumerable<T>
	{
		public Node root;

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

			public Node (T value)
			{
				this.value = value;
				children = null;
			}

			public Node(T value, params T[] children)
			{
				this.value = value;
				this.children = children.Select((t, _) => new Node(t)).ToList();
			}

			public void AddChild(T val)
			{
				children ??= new(1);
				children.Add(new(val));
			}
		}
		#endregion

		#region ENUMERATION
		/// <summary>
		/// Returns an enumerator for this tree.
		/// </summary>
		/// <param name="depthFirst">Determines if the enumeration goes depth-first or breadth-first.</param>
		/// <returns>An enumerator for this tree.</returns>
		public Enumerator GetTreeEnumerator(bool depthFirst = false) => new(this, depthFirst);

		public IEnumerator<T> GetEnumerator() => GetTreeEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetTreeEnumerator();

		public class Enumerator : IEnumerator<T>
		{
			protected List<T> _values;
			protected int _index;

			public T Current => _values[_index];
			object IEnumerator.Current => Current;

			public Enumerator(Tree<T> tree, bool depthFirst)
			{
				_values = new(1);
				SetNodes(tree.root, depthFirst);
				_index = 0;
			}

			public void Dispose() { }

			public bool MoveNext()
			{
				_index++;
				return _index < _values.Count;
			}

			public void Reset()
			{
				_index = 0;
			}

			private void SetNodes(Node node, bool depthFirst)
			{
				if (depthFirst)
					SetNodesDepthFirst(node);
				else
					SetNodesBreadthFirst(node);
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
					Tree<T>.Enumerator.GetNodesWithDepth(child, ref nodes, currentDepth++);
			}
		}
		#endregion
	}
}
