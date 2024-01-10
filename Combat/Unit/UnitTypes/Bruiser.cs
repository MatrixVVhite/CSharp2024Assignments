namespace Berzerkers.Combat.Unit.UnitTypes
{
	public abstract class Bruiser : Unit
	{
		public Bruiser(int hp, int damage, Race race) : base(hp, damage, race) { }

		public override void Attack(Unit other)
		{
			if (CurrentHP > other.CurrentHP)
			{
				base.Attack(other);
				base.Attack(other);
			}
		}
	}
}
