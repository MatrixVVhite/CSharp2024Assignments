using Berzerkers.Random;

namespace Berzerkers.Combat.Unit.UnitTypes
{
    using Berzerkers.Random;
    using static DiceExtensions;

    public abstract class Marauder : Unit
	{
		private float _surviveHpThreshold;
		private int _surviveAtHp;

		private bool CanSurvive => CurrentHP > _surviveAtHp && this.GetHPPercentage() >= _surviveHpThreshold;

		public Marauder(string name, int hp, IRandomProvider<int> damage, Race race, IRandomProvider<int> hit, IRandomProvider<int> avoid, int carryingCapacity = 8, float surviveHpThreshold = .1f, int surviveAtHp = 1) : base(name, hp, damage, hit, avoid, race, carryingCapacity)
		{
			_surviveHpThreshold = surviveHpThreshold;
			_surviveAtHp = surviveAtHp;
		}

		public Marauder(string name, int hp, IRandomProvider<int> damage, Race race, int carryingCapacity = 8, float surviveHpThreshold = .1f, int surviveAtHp = 1) : base(name, hp, damage, oneD6, oneD6, race, carryingCapacity)
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
			int incomingDamage = other.Damage.GetRandom();
			if (CanSurvive)
				incomingDamage = Utility.ClampMax(incomingDamage, CurrentHP - _surviveAtHp);
			Console.WriteLine($"{other} deals {incomingDamage} damage to {this}.");
			ApplyDamage(incomingDamage);
		}
	}
}
