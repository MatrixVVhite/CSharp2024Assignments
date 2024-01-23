using System.Diagnostics.CodeAnalysis;

namespace Berzerkers.Combat
{
	public readonly struct Dice : IEquatable<Dice>
	{
		public readonly byte scalar;
		public readonly byte baseDie;
		public readonly short modifier;

		public Dice(byte scalar = 1, byte baseDie = 6, short modifier = 0)
		{
			this.scalar = scalar;
			this.baseDie = baseDie;
			this.modifier = modifier;
		}

		public readonly int Roll()
		{
			int retVal = modifier;
			for (int d = 0; d < scalar; d++)
				retVal += RollSingle();
			return retVal;
		}

		private readonly int RollSingle() => 1 + Random.Shared.Next(baseDie);

		public override readonly string ToString() => $"{scalar}d{baseDie}{(modifier>0?"": '+')}{modifier}";

		public override bool Equals(object? obj)
		{
			return obj is Dice dice && Equals(dice);
		}

		public bool Equals(Dice other)
		{
			return scalar == other.scalar &&
				   baseDie == other.baseDie &&
				   modifier == other.modifier;
		}

		public override readonly int GetHashCode() => (int)scalar << 24 + (int)baseDie << 16 + modifier;

		public static bool operator ==(Dice left, Dice right) => left.Equals(right);

		public static bool operator !=(Dice left, Dice right) => !(left == right);
	}
}
