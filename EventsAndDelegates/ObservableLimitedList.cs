using System.Collections;

public class ObservableLimitedList<T> : IList<T>
{
	public event Action<T> ListChanged;
	private List<T> InternalList { get; set; }
	private Predicate<T> AddCondition { get; set; }

	public int Count => InternalList.Count;

	public bool IsReadOnly => ((ICollection<T>)InternalList).IsReadOnly;

	public T this[int index] { get => InternalList[index]; set => InternalList[index] = value; }

	public ObservableLimitedList(Predicate<T> addCondition, int capacity = 10)
	{
		InternalList = new List<T>(capacity);
		AddCondition = addCondition;
	}

	public int IndexOf(T item) => InternalList.IndexOf(item);

	public void Insert(int index, T item) => TryInsert(index, item);

	public bool TryInsert(int index, T item)
	{
		if (AddCondition.Invoke(item))
		{
			InternalList.Insert(index, item);
			ListChanged.Invoke(item);
			return true;
		}
		return false;
	}

	public void RemoveAt(int index) => InternalList.RemoveAt(index);

	public void Add(T item) => TryAdd(item);

	public bool TryAdd(T item)
	{
		if (AddCondition.Invoke(item))
		{
			InternalList.Add(item);
			ListChanged.Invoke(item);
			return true;
		}
		return false;
	}

	public void Clear()
	{
		ListChanged.Invoke(default);
		InternalList.Clear();
	}

	public bool Contains(T item) => InternalList.Contains(item);

	public void CopyTo(T[] array, int arrayIndex) => InternalList.CopyTo(array, arrayIndex);

	public bool Remove(T item)
	{
		if (InternalList.Remove(item))
		{
			ListChanged.Invoke(item);
			return true;
		}
		return false;
	}

	public IEnumerator<T> GetEnumerator() => InternalList.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)InternalList).GetEnumerator();
}
