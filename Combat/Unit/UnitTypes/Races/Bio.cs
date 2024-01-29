namespace Berzerkers.Combat.Unit.UnitTypes.Races.Bio
{
	public sealed class WarCleric : Bruiser
	{
		public WarCleric(string name = "WarCleric") : base(name, 10, new(baseDie: 4), Race.Bio) { }

		/// <summary>
		/// Attacks with an additional die if target is of race Void.
		/// </summary>
		/// <param name="other"></param>
		public override void Attack(Unit other)
		{
			if (other.Race == Race.Void)
				AttackOverrideDamage(other, Damage.AddScalar());
			else
				base.Attack(other);
		}
	}

	public sealed class Cataphract : Marauder
	{
		private readonly float _regenLostHealth;
		private readonly int _maxRegenAmount;

		public Cataphract(string name = "Cataphract") : base(name, 15, new(baseDie: 3, modifier: 0), Race.Bio, surviveHpThreshold: .2f)
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
		private Dice _reviveDice;
		private readonly short _onReviveDiceModifier;

		public Hegemon(string name = "Hegemon") : base(name, 20, new(baseDie: 2, modifier: 1), Race.Bio)
		{
			_reviveDice = new Dice(2, 6, -6);
			_onReviveDiceModifier = -2;
		}

		/// <summary>
		/// Rolls a 2d6-6 > 0 to revive to full HP, modifier worsen each successful revive.
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
			if (_reviveDice.Roll() > 0)
			{
				this.HealFully();
				_reviveDice = _reviveDice.AddModifier(_onReviveDiceModifier);
				return true;
			}
			return false;
		}
	}
}
