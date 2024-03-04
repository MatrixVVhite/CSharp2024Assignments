namespace Berzerkers.Random
{
	public struct Bag : IRandomProvider<int>
	{
		public int Min { get; private set; }

		public int Max { get; private set; }

		private List<int> Available { get; set; }

		public Bag(int max, int min = 0)
		{
			Max = max;
			Min = min;
			Available = new List<int>(Max - Min);
			ResetBag();
		}

		public int GetRandom()
		{
			int ret = PullFromBag();
			if (Available.Count <= 0)
				ResetBag();
			return ret;
		}

		private readonly int PullFromBag()
		{
			int ret = Available.GetRandom();
			Available.Remove(ret);
			return ret;
		}

		public readonly void ResetBag()
		{
			Available.Clear();
			for (int i = 0; i < Max; ++i)
				Available.Add(Min + i);
		}
	}
}
