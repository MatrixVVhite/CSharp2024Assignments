namespace Berzerkers.Random
{
	public readonly struct Dice : IRandomProvider<int>, IEquatable<Dice>
	{
		private readonly Uniform _die;

		public byte Scalar { get; init; }
		public byte BaseDie => (byte)_die.Max;
		public short Modifier { get; init; }
		public int Min => Modifier + Scalar;
		public int Max => Modifier + Scalar * BaseDie;

		public Dice(byte scalar = 1, byte baseDie = 6, short modifier = 0)
		{
			Scalar = scalar;
			_die = new(1, baseDie);
			Modifier = modifier;
		}

		public int GetRandom() => Roll();

		public readonly int Roll()
		{
			int retVal = Modifier;
			for (int d = 0; d < Scalar; d++)
				retVal += RollSingle();
			return retVal;
		}

		private readonly int RollSingle() => 1 + IRandomProvider<int>.RANDOM.Next(BaseDie);

		#region OVERRIDES
		public override readonly string ToString() => $"{Scalar}d{BaseDie}{(Modifier >= 0 ? '+' : string.Empty)}{Modifier}";

		public override bool Equals(object? obj) => obj is Dice dice && Equals(dice);

		public bool Equals(Dice other)
		{
			return Scalar == other.Scalar &&
				   BaseDie == other.BaseDie &&
				   Modifier == other.Modifier;
		}

		public override readonly int GetHashCode() => (Scalar, BaseDie, Modifier).GetHashCode();

		public static bool operator ==(Dice left, Dice right) => left.Equals(right);

		public static bool operator !=(Dice left, Dice right) => !(left == right);
		#endregion
	}
}
