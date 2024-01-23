namespace Berzerkers.Combat.Unit.UnitTypes
{
	public abstract class Bruiser : Unit
	{
		public Bruiser(int hp, Dice damage, Race race, Dice hit = new(), Dice avoid = new(), int carryingCapacity = 5) : base(hp, damage, race, carryingCapacity, hit, avoid) { }

		/// <summary>
		/// If HP is higher than target's HP when calling this, attacks twice.
		/// </summary>
		/// <param name="other"></param>
		public override void Attack(Unit other)
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
	}
}
