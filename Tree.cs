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
		public Enumerator GetTreeEnumerator(bool depthFirst = false) => depthFirst ? new EnumeratorDepth(this) : new EnumeratorBreadth(this);

		public IEnumerator<T> GetEnumerator() => GetTreeEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetTreeEnumerator();

		public abstract class Enumerator : IEnumerator<T>
		{
			protected Tree<T> _tree;
			protected Stack<Node> _nodes;
			protected int _childIndex;

			public T Current => CurrentNode.value;
			protected Node CurrentNode => _nodes.Peek();
			object IEnumerator.Current => Current;

			public Enumerator(Tree<T> tree)
			{
				_tree = tree;
				_nodes = new(1);
				_nodes.Push(_tree.root);
				_childIndex = 0;
			}

			public void Dispose() { }

			public abstract bool MoveNext();

			public void Reset()
			{
				_nodes.Clear();
				_nodes.Push(_tree.root);
			}
		}

		private class EnumeratorDepth : Enumerator
		{
			public EnumeratorDepth(Tree<T> tree) : base(tree) { }

			public override bool MoveNext()
			{
				if (CurrentNode.children == null)
				{
					
				}
			}
		}

		private class EnumeratorBreadth : Enumerator
		{
			public EnumeratorBreadth(Tree<T> tree) : base(tree) { }

			public override bool MoveNext()
			{
				throw new NotImplementedException();
			}
		}
		#endregion
	}
}
