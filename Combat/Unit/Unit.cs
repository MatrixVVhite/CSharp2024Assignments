namespace Berzerkers.Combat.Unit
{
	public abstract class Unit
	{
		private int _maxHp;
		private int _currentHp;
		private Dice _damage;
		private Dice _hit;
		private Dice _avoid;
		private int _carryingCapacity;
		private WeatherEffect _weatherEffect;
		private Race _race;

		public int MaxHP { get => _maxHp; protected set => _maxHp = Utility.ClampMin(value, 1); }
		public int CurrentHP { get => _currentHp; protected set => _currentHp = Utility.ClampRange(value, 0, _maxHp); }
		public Dice Damage { get => _damage; protected set => _damage = value; }
		public Dice Hit { get => _hit; protected set => _hit = value; }
		public Dice Avoid { get => _avoid; protected set => _avoid = value; }
		public int CarryingCapacity { get => _carryingCapacity; protected set => _carryingCapacity = value; }
		public bool IsDead => CurrentHP <= 0;
		public WeatherEffect WeatherEffect { get => _weatherEffect; protected set => _weatherEffect = value; }
		public Race Race => _race;

		public Unit(int hp, Dice damage, Race race, int carryingCapacity, Dice hit = new(), Dice avoid = new(), WeatherEffect weatherEffect = WeatherEffect.None)
		{
			MaxHP = hp;
			CurrentHP = hp;
			Damage = damage;
			Hit = hit;
			Avoid = avoid;
			_race = race;
			CarryingCapacity = carryingCapacity;
			WeatherEffect = weatherEffect;
		}

		public virtual void Attack(Unit other)
		{
			if (Hit.Roll() > other.Avoid.Roll())
				other.Defend(this);
		}

		protected virtual void Defend(Unit other) => ApplyDamage(other.Damage.Roll());

		protected void ApplyDamage(int damage) => CurrentHP -= damage;

		public void Heal(int amount) => CurrentHP += amount;
	}

	public enum Race
	{
		Bio,
		Cyber,
		Void
	}

	public enum WeatherEffect
	{
		None,
		Sandstorm,
		Rain,
		AcidRain,
		Miasma
	}
}
