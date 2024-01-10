namespace Berzerkers.Combat.Unit
{
	public abstract class Unit
	{
		private int _maxHp;
		private int _currentHp;
		private int _damage;
		private Race _race;

		public int MaxHP { get => _maxHp; protected set => _maxHp = Utility.ClampMin(value, 1); }
		public int CurrentHP { get => _currentHp; protected set => _currentHp = Utility.ClampRange(value, 0, _maxHp); }
		public int Damage { get => _damage; protected set => _damage = Utility.ClampMin(value, 0); }
		public bool IsDead => CurrentHP <= 0;
		public Race Race => _race;

		public Unit(int hp, int damage, Race race)
		{
			MaxHP = hp;
			CurrentHP = hp;
			Damage = damage;
			_race = race;
		}

		public virtual void Attack(Unit other) => other.Defend(this);

		public virtual void Defend(Unit other) => ApplyDamage(other.Damage);

		protected void ApplyDamage(int damage) => CurrentHP -= damage;
	}

	public enum Race
	{
		Bio,
		Machine,
		Void
	}
}
