namespace GenericTypesAndIComparable.Structs
{
    public class Deck<T> where T : struct, IComparable<T>
    {
        private List<T> Active { get; set; }
        private List<T> Discarded { get; set; }
        public int Size => Active.Capacity;
        public int Remaining => Active.Count;

        public Deck(IEnumerable<T> values)
        {
            Active = values.ToList();
            Discarded = new List<T>(Size);
        }

        public bool TryDraw(out T drawn)
        {
            if (Remaining > 0)
            {
                drawn = Draw();
                Discarded.Add(drawn);
                return true;
            }
            drawn = default;
            return false;
        }

        public void Shuffle() => Active.Shuffle();

        public void ReShuffle()
        {
            MoveDiscardedToActive();
            Shuffle();
        }

        public T Peek() => Active.Last();

        private void MoveDiscardedToActive()
        {
            foreach (var item in Discarded)
                Active.Add(item);
            Discarded.Clear();
        }

        private T Draw()
        {
            T drawn = Active.Last();
            Active.RemoveAt(Remaining - 1);
            return drawn;
        }
    }
}