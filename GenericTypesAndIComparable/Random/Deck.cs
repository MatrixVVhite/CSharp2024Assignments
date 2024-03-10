public class Deck<T> where T : struct, IComparable<T>
{
	private List<T> Active { get; set; }
	private List<T> Discarded { get; set; }
	public int Size { get; private init; }
	public int Remaining => Active.Count;

	public Deck(T[] values)
	{
		Size = values.Length;
		Active = values.ToList();
		Discarded = new List<T>(Size);
	}

	public bool TryDraw(out T drawn)
	{
		if (Remaining > 0)
		{
			drawn = Draw();
			return true;
		}
		drawn = default;
		return false;
	}

	public void Shuffle()
	{
		for (int i = 0; i < Remaining; i++)
			Utility.Swap<T>(ref Active[i], ref Active[Random.Shared.Next(Remaining)]);
	}

	public void ReShuffle()
	{
		MoveDiscardedToActive();
		Shuffle();
	}

	public T Peek() => Active.Last();

	private void MoveDiscardedToActive()
	{
		foreach (var item in Discarded)
			Active.Append(item);
		Discarded.Clear();
	}

	private T Draw()
	{
		T drawn = Active.Last();
		Active.RemoveAt(Remaining - 1);
		return drawn;
	}
}
