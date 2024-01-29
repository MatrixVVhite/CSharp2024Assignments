﻿namespace Berzerkers.Combat.Unit
{
	public abstract class Unit
	{
		private int _maxHp;
		private int _currentHp;
		private Dice _damage;
		private Dice _hit;
		private Dice _avoid;
		private int _carryingCapacity;
		private Race _race;

		public string Name { get; protected set; }
		public int MaxHP { get => _maxHp; protected set => _maxHp = Utility.ClampMin(value, 1); }
		public int CurrentHP { get => _currentHp; protected set => _currentHp = Utility.ClampRange(value, 0, _maxHp); }
		public Dice Damage { get => _damage; protected set => _damage = value; }
		public Dice Hit { get => _hit; protected set => _hit = value; }
		public Dice Avoid { get => _avoid; protected set => _avoid = value; }
		public int CarryingCapacity { get => _carryingCapacity; protected set => _carryingCapacity = value; }
		public bool IsDead => CurrentHP <= 0;
		public Race Race => _race;

		public Unit(string name, int hp, Dice damage, Dice hit, Dice avoid, Race race, int carryingCapacity)
		{
			Name = name;
			MaxHP = hp;
			CurrentHP = hp;
			Damage = damage;
			Hit = hit;
			Avoid = avoid;
			_race = race;
			CarryingCapacity = carryingCapacity;
		}

		/// <summary>
		/// This unit attempts to attack 'other', determine if attack lands.
		/// </summary>
		/// <param name="other">The defending unit</param>
		public virtual void Attack(Unit other)
		{
			if (Hit.Roll() >= other.Avoid.Roll())
				other.Defend(this);
		}

		/// <summary>
		/// This unit gets hit by 'other', calculate other's damage.
		/// </summary>
		/// <param name="other"></param>
		protected virtual void Defend(Unit other) => ApplyDamage(other.Damage.Roll());

		protected void ApplyDamage(int damage) => CurrentHP -= damage;

		public virtual void ApplyWeatherEffect(WeatherEffect weatherEffect)
		{
			switch (weatherEffect)
			{
				case WeatherEffect.None:
					break;
				case WeatherEffect.Sandstorm:
					if (Race == Race.Cyber && CurrentHP > 1)
						ApplyDamage(1);
					break;
				case WeatherEffect.Rain:
					if (Race == Race.Void && CurrentHP > 1)
						ApplyDamage(1);
					break;
				case WeatherEffect.AcidRain:
					if (Race == Race.Bio && CurrentHP > 1)
						ApplyDamage(1);
					break;
				case WeatherEffect.Miasma:
					ApplyDamage(1);
					break;
			}
		}

		public void Heal(int amount) => CurrentHP += amount;

		public override string ToString() => Name;
	}

	public enum Race
	{
		Bio,
		Cyber,
		Void
	}
}
