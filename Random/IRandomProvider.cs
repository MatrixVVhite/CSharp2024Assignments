namespace Berzerkers.Random
{
	public interface IRandomProvider<T>
	{
		protected static readonly System.Random RANDOM = System.Random.Shared;

		public T Min { get; }
		public T Max { get; }

		public T GetRandom();
	}
}
