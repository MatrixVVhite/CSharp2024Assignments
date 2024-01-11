namespace Berzerkers.Combat.Unit.UnitTypes.Races
{
	public sealed class Blitzer : Bruiser
	{
		private float _damageMultiplier;

		public Blitzer() : base(11, 3, Race.Cyber)
		{
			_damageMultiplier = 1.5f;
		}

		/// <summary>
		/// Attacks twice if has more HP than target, attacks for 150% damage if target is of race Bio.
		/// </summary>
		/// <param name="other"></param>
		public override void Attack(Unit other)
		{
			if (other.Race == Race.Bio)
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

	public sealed class MetalVanguard : Marauder
	{
		/// <summary>
		/// If not at 1HP, can always survive fatal blows at 1HP.
		/// </summary>
		public MetalVanguard() : base(16, 1, Race.Cyber, 0f, 1) { }
	}

	public sealed class Draedon : Marauder
	{
		private int _revivalCells;
		private float _reviveAtHp;

		public int RevivalCells => _revivalCells;

		public Draedon() : base(20, 2, Race.Cyber, 0f, 1)
		{
			_revivalCells = 1;
			_reviveAtHp = .5f;
		}

		/// <summary>
		/// Revives once to 50% HP after being defeated.
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
			if (_revivalCells > 0)
			{
				this.HealPercentage(_reviveAtHp);
				_revivalCells--;
				return true;
			}
			return false;
		}
	}
}
