namespace Berzerkers.Random
{
	/// <summary>
	/// Used to generate a uniformly random number between <see cref="Min"/> and <see cref="Max"/> (inclusive).
	/// </summary>
	public readonly struct Uniform : IRandomProvider<int>, IEquatable<Uniform>
	{
		public int Min { get; init; }
		public int Max { get; init; }

		public readonly int GetRandom() => IRandomProvider<int>.RANDOM.Next(Min, Max+1);

		public Uniform(int min = 0, int max = 1)
		{
			Min = min;
			Max = max;
		}

		public override readonly int GetHashCode() => (Max, Min).GetHashCode();

		#region IEQUTABLE
		public readonly bool Equals(Uniform other) => Min == other.Min && Max == other.Max;

		public override readonly bool Equals(object? obj) => obj is Uniform uniform && Equals(uniform);

		public static bool operator ==(Uniform left, Uniform right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Uniform left, Uniform right)
		{
			return !(left == right);
		}
		#endregion
	}
}
