using Berzerkers.Random;

namespace Berzerkers.Combat.Unit.UnitTypes.Races.Void
{
    public sealed class Sentinel : Bruiser
	{
		public Sentinel(string name = "Sentinel") : base(name, 8, new Dice(scalar: 2, baseDie: 2), Race.Void) { }

		/// <summary>
		/// Attacks with more, larger, dice if target is of race Cyber.
		/// </summary>
		/// <param name="other"></param>
		public override void Attack(Unit other)
		{
			if (other.Race == Race.Cyber)
				AttackOverrideDamage(other, ((Dice)Damage).AddBaseDie().AddScalar());
			else
				base.Attack(other);
		}
	}

	public sealed class Bonewalker : Marauder
	{
		private Dice _damageOnDeath;
		private Bag _onDefendBag;
		private readonly List<Action<Unit>> _onDefend;

		public Bonewalker(string name = "Bonewalker") : base(name, 13, new Dice(baseDie: 4), Race.Void)
		{
			_damageOnDeath = new(scalar: 2, baseDie: 3);
			_onDefend = new()
			{
				u => Attack(u),
				u => Heal(1),
				u => _damageOnDeath = _damageOnDeath.AddModifier(1)
			};
			_onDefendBag = new(_onDefend.Count);
		}

		/// <summary>
		/// When attacked, triggers a random effect.
		/// On death, makes an attack for 2d3 damage.
		/// </summary>
		/// <param name="other"></param>
		protected override void Defend(Unit other)
		{
			if (!IsDead)
			{
				base.Defend(other);
				DoOnDefend(other);
				if (IsDead)
				{
					Damage = _damageOnDeath;
					Attack(other);
				}
			}
		}

		private void DoOnDefend(Unit other)
		{
			_onDefend[_onDefendBag.GetRandom()](other);
		}
	}

	public sealed class Garuda : Bruiser
	{
		private float _secondAttackAtThreshold;

		public Garuda(string name = "Garuda") : base(name, 15, new Dice(scalar: 2, baseDie: 2, modifier: 2), Race.Void)
		{
			_secondAttackAtThreshold = .5f;
		}

		/// <summary>
		/// Makes a second attack upon lowering the enemy's HP to 50% or less.
		/// </summary>
		/// <param name="other"></param>
		public override void Attack(Unit other)
		{
			bool otherHpAboveThreshold = other.GetHPPercentage() > _secondAttackAtThreshold;
			base.Attack(other);
			if (otherHpAboveThreshold & other.GetHPPercentage() <= _secondAttackAtThreshold)
				base.Attack(other);
		}
	}
}
