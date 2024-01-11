namespace Berzerkers.Combat.Unit.UnitTypes
{
	public abstract class Marauder : Unit
	{
		private float _surviveHpThreshold;
		private int _surviveAtHp;

		private bool CanSurvive => CurrentHP > _surviveAtHp && this.GetHPPercentage() >= _surviveHpThreshold;

		public Marauder(int hp, int damage, Race race, float surviveHpThreshold = .1f, int surviveAtHp = 1) : base(hp, damage, race)
		{
			_surviveHpThreshold = surviveHpThreshold;
			_surviveAtHp = surviveAtHp;
		}

		/// <summary>
		/// Can survive fatal blows at "_surviveAtHp" HP when HP percentage is above "_surviveHpThreshold".
		/// </summary>
		/// <param name="other"></param>
		protected override void Defend(Unit other)
		{
			int incomingDamage = other.Damage;
			if (CanSurvive)
				incomingDamage = Utility.ClampMax(incomingDamage, CurrentHP - _surviveAtHp);
			ApplyDamage(incomingDamage);
		}
	}
}
