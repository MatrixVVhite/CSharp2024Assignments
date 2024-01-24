namespace Berzerkers.Combat.Unit.UnitTypes.Races.Cyber
{
	public sealed class Blitzer : Bruiser
	{
		private short _hitIncrease;

		public Blitzer() : base(11, new(baseDie: 2, modifier: 1), Race.Cyber)
		{
			_hitIncrease = 2;
		}

		/// <summary>
		/// Attacks with +2 accuracy if target is of race Bio.
		/// </summary>
		/// <param name="other"></param>
		public override void Attack(Unit other)
		{
			if (other.Race == Race.Bio)
				AttackOverrideHit(other, Hit.AddModifier(_hitIncrease));
			else
				base.Attack(other);
		}
	}

	public sealed class MetalVanguard : Marauder
	{
		/// <summary>
		/// If not at 1HP, can always survive fatal blows at 1HP.
		/// </summary>
		public MetalVanguard() : base(16, new(baseDie: 2), Race.Cyber, surviveHpThreshold: 0f, surviveAtHp: 1) { }
	}

	public sealed class Draedon : Marauder
	{
		private int _revivalCells;
		private float _reviveAtHp;

		public int RevivalCells => _revivalCells;

		public Draedon() : base(25, new(baseDie: 3), Race.Cyber, surviveHpThreshold: 0f, surviveAtHp: 1)
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
