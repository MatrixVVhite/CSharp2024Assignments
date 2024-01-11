namespace Berzerkers.Combat.Unit.UnitTypes.Races
{
	public sealed class Sentinel : Bruiser
	{
		private float _damageMultiplier;

		public Sentinel() : base(8, 4, Race.Void)
		{
			_damageMultiplier = 2f;
		}

		/// <summary>
		/// Attacks twice if has more HP than target, attacks for double damage if target is of race Cyber.
		/// </summary>
		/// <param name="other"></param>
		public override void Attack(Unit other)
		{
			if (other.Race == Race.Cyber)
			{
				int defaultDamage = Damage;
				Damage = (int)(Damage * _damageMultiplier);
				base.Attack(other);
				Damage = defaultDamage;
			}
			else
			{
				base.Attack(other);
			}
		}
	}

	public sealed class Bonewalker : Marauder
	{
		private int _damageOnDeath;

		public int DamageOnDeath => _damageOnDeath;

		public Bonewalker() : base(13, 3, Race.Void)
		{
			_damageOnDeath = 5;
		}

		/// <summary>
		/// Deals 5 damage to attacker on death.
		/// </summary>
		/// <param name="other"></param>
		protected override void Defend(Unit other)
		{
			if (!IsDead)
			{
				base.Defend(other);
				if (IsDead)
				{
					Damage = _damageOnDeath;
					Attack(other);
				}
			}
		}
	}

	public sealed class Garuda : Bruiser
	{
		private float _secondAttackAtThreshold;

		public Garuda() : base(25, 5, Race.Void)
		{
			_secondAttackAtThreshold = .5f;
		}

		/// <summary>
		/// Makes a second attack upon lowering the enemy's HP below 50%.
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
