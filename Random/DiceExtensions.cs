namespace Berzerkers.Random
{
    static class DiceExtensions
    {
        public static readonly Dice oneD6 = new(scalar: 1, baseDie: 6);
        public static readonly Dice twoD12 = new(scalar: 2, baseDie: 6);

        public static Dice SetModifier(this Dice dice, short modifier) => new(dice.Scalar, dice.BaseDie, modifier);

        public static Dice AddModifier(this Dice dice, short modifier = 1) => dice.SetModifier((short)(dice.Modifier + modifier));

        public static Dice SetScalar(this Dice dice, byte scalar) => new(scalar, dice.BaseDie, dice.Modifier);

        public static Dice AddScalar(this Dice dice, byte scalar = 1) => dice.SetScalar((byte)(dice.Scalar + scalar));

        public static Dice SetBaseDie(this Dice dice, byte baseDie) => new(dice.Scalar, (byte)(dice.BaseDie + baseDie), dice.Modifier);

        public static Dice AddBaseDie(this Dice dice, byte baseDie = 1) => dice.SetBaseDie((byte)(dice.BaseDie + baseDie));
    }
}
