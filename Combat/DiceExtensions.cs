namespace Berzerkers.Combat
{
	static class DiceExtensions
	{
		public static readonly Dice oneD6 = new(scalar: 1, baseDie: 6);
		public static readonly Dice twoD12 = new(scalar: 2, baseDie: 6);

		public static Dice SetModifier(this Dice dice, short modifier) => new(dice.scalar, dice.baseDie, modifier);

		public static Dice AddModifier(this Dice dice, short modifier = 1) => dice.SetModifier((short)(dice.modifier + modifier));

		public static Dice SetScalar(this Dice dice, byte scalar) => new(scalar, dice.baseDie, dice.modifier);

		public static Dice AddScalar(this Dice dice, byte scalar = 1) => dice.SetScalar((byte)(dice.scalar + scalar));

		public static Dice SetBaseDie(this Dice dice, byte baseDie) => new(dice.scalar, (byte)(dice.baseDie + baseDie), dice.modifier);

		public static Dice AddBaseDie(this Dice dice, byte baseDie = 1) => dice.SetBaseDie((byte)(dice.baseDie + baseDie));
	}
}
