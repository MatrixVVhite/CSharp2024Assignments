namespace GenericTypesAndIComparable.Structs
{
    public class Dice : Dice<int>
    {
        public short Modifier { get; init; }

        public Dice(byte scalar = 1, byte baseDie = 6, short modifier = 0) : base(scalar, Enumerable.Range(1, baseDie).ToArray())
        {
            Modifier = modifier;
        }

        public new int Roll()
        {
            int[] result = base.Roll();
            return result.Sum() + Modifier;
        }

        #region OVERRIDES
        public override string ToString() => $"{Scalar}d{BaseDie.Last()}{(Modifier >= 0 ? '+' : string.Empty)}{Modifier}";

        public override bool Equals(object? obj) => obj is Dice dice && Equals(dice);

        public bool Equals(Dice other)
        {
            return Scalar == other.Scalar &&
                    BaseDie == other.BaseDie &&
                    Modifier == other.Modifier;
        }

        public override int GetHashCode() => (Scalar, BaseDie, Modifier).GetHashCode();

        public static bool operator ==(Dice left, Dice right) => left.Equals(right);

        public static bool operator !=(Dice left, Dice right) => !(left == right);
        #endregion
    }
}