namespace Berzerkers.Combat.Unit
{
	static class UnitExtensions
	{
		public static float GetHPPercentage(this Unit unit) => unit.CurrentHP / (float)unit.MaxHP;

		public static void HealFully(this Unit unit) => unit.Heal(unit.MaxHP);

		public static void HealPercentage(this Unit unit, float percentage = 1f) => unit.Heal((int)(unit.MaxHP * percentage));
	}
}
