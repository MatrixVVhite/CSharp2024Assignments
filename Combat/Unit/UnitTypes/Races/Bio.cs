namespace Berzerkers.Combat.Unit.UnitTypes.Races
{
	public sealed class WarCleric : Bruiser
	{
		public WarCleric() : base(10, 3, Race.Bio) { }

		/// <summary>
		/// Attacks twice if target is either of race Void or has more HP than target, can attack up to 4 times if both.
		/// </summary>
		/// <param name="other"></param>
		public override void Attack(Unit other)
		{
			base.Attack(other);
			if (other.Race == Race.Void)
				base.Attack(other);
		}
	}

	public sealed class Cataphract : Marauder
	{
		private float _regenLostHealth;
		private int _maxRegenAmount;

		public Cataphract() : base(15, 2, Race.Bio, .2f)
		{
			_regenLostHealth = .5f;
			_maxRegenAmount = 2;
		}

		/// <summary>
		/// Heals 50% of non-fatal damage received, up to 2 HP.
		/// </summary>
		/// <param name="other"></param>
		protected override void Defend(Unit other)
		{
			int hpBefore = CurrentHP;
			base.Defend(other);
			if (!IsDead)
			{
				int lostHp = Utility.ClampMin(hpBefore - CurrentHP, 0);
				int amountToRegen = Utility.ClampMax((int)(lostHp * _regenLostHealth), _maxRegenAmount);
				Heal(amountToRegen);
			}
		}
	}

	public sealed class Hegemon : Marauder
	{
		private float _reviveChance;

		public Hegemon() : base(30, 4, Race.Bio)
		{
			_reviveChance = .25f;
		}

		/// <summary>
		/// Has a 50% chance to revive once to full HP.
		/// </summary>
		/// <param name="other"></param>
		protected override void Defend(Unit other)
		{
			base.Defend(other);
			if (IsDead)
				TryRevive();
		}

		private bool TryRevive()
		{
			if (Utility.RollChance(_reviveChance))
			{
				this.HealFully();
				_reviveChance = 0f;
				return true;
			}
			return false;
		}
	}
}
