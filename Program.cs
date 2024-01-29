namespace Berzerkers
{
	using Combat;
	using Combat.Unit.UnitTypes.Races.Bio;
	using Combat.Unit.UnitTypes.Races.Void;

	public class Program
	{
		public static void Main()
		{
			TestCombatManager();
		}

		private static void TestCombatManager()
		{
			Team team1 = new(
				"The Cradle",
				new WarCleric("War Cleric 1"),
				new Cataphract("Cataphract 1"),
				new WarCleric("War Cleric 2"),
				new Cataphract("Cataphract 2"),
				new Hegemon("Hegemon"));
			Team team2 = new(
				"The Horizon",
				new Sentinel("Sentinel 1"),
				new Bonewalker("Bonewalker 1"),
				new Sentinel("Sentinel 2"),
				new Bonewalker("Bonewalker 2"),
				new Garuda("Garuda"));
			CombatManager combatManager = new(team1, team2);
			combatManager.Fight();
		}
	}
}