namespace Berzerkers.Combat.Unit
{
	static class UnitExtensions
	{
		public static float GetHPPercentage(this Unit unit) => unit.CurrentHP / (float)unit.MaxHP;
	}
}
