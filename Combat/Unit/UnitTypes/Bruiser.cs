namespace Berzerkers.Combat.Unit.UnitTypes
{
	using static DiceExtensions;

	public abstract class Bruiser : Unit
	{
		public Bruiser(string name, int hp, Dice damage, Race race, Dice hit, Dice avoid, int carryingCapacity = 5) : base(name, hp, damage, hit, avoid, race, carryingCapacity) { }
		
		public Bruiser(string name, int hp, Dice damage, Race race, int carryingCapacity = 5) : base(name, hp, damage, oneD6, oneD6, race, carryingCapacity) { }

		/// <summary>
		/// If HP is higher than target's HP when calling this, attacks twice.
		/// </summary>
		/// <param name="other"></param>
		public override void Attack(Unit other) => DoubleAttack(other);

		private void DoubleAttack(Unit other)
		{
			if (CurrentHP > other.CurrentHP)
			{
				base.Attack(other);
				base.Attack(other);
			}
			else
			{
				base.Attack(other);
			}
		}

		protected void AttackOverrideDamage(Unit other, Dice overrideDamage)
		{
			Dice baseDamage = Damage;
			Damage = overrideDamage;
			DoubleAttack(other);
			Damage = baseDamage;
		}

		protected void AttackOverrideHit(Unit other, Dice overrideHit)
		{
			Dice baseHit = Hit;
			Hit = overrideHit;
			DoubleAttack(other);
			Damage = baseHit;
		}
	}
}
