namespace Berzerkers.Combat.Unit.UnitTypes
{
	using static DiceExtensions;

	public abstract class Marauder : Unit
	{
		private float _surviveHpThreshold;
		private int _surviveAtHp;

		private bool CanSurvive => CurrentHP > _surviveAtHp && this.GetHPPercentage() >= _surviveHpThreshold;

		public Marauder(string name, int hp, Dice damage, Race race, Dice hit, Dice avoid, int carryingCapacity = 8, float surviveHpThreshold = .1f, int surviveAtHp = 1) : base(name, hp, damage, hit, avoid, race, carryingCapacity)
		{
			_surviveHpThreshold = surviveHpThreshold;
			_surviveAtHp = surviveAtHp;
		}

		public Marauder(string name, int hp, Dice damage, Race race, int carryingCapacity = 8, float surviveHpThreshold = .1f, int surviveAtHp = 1) : base(name, hp, damage, oneD6, oneD6, race, carryingCapacity)
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
			int incomingDamage = other.Damage.Roll();
			if (CanSurvive)
				incomingDamage = Utility.ClampMax(incomingDamage, CurrentHP - _surviveAtHp);
			ApplyDamage(incomingDamage);
		}
	}
}
