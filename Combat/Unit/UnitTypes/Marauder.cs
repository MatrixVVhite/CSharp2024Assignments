namespace Berzerkers.Combat.Unit.UnitTypes
{
	public abstract class Marauder : Unit
	{
		private const float SURVIVE_HP_THRESHOLD = .1f;
		private const int SURVIVE_AT_HP = 1;

		private bool CanSurvive => CurrentHP > SURVIVE_AT_HP && this.GetHPPercentage() >= SURVIVE_HP_THRESHOLD;

		public Marauder(int hp, int damage, Race race) : base(hp, damage, race) { }

		protected override void Defend(Unit other)
		{
			int incomingDamage = other.Damage;
			if (CanSurvive)
				incomingDamage = Utility.ClampMax(incomingDamage, CurrentHP - SURVIVE_AT_HP);
			ApplyDamage(incomingDamage);
		}
	}
}
