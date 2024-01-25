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
				new WarCleric(),
				new Cataphract(),
				new WarCleric(),
				new Cataphract(),
				new Hegemon());
			Team team2 = new(
				"The Horizon",
				new Sentinel(),
				new Bonewalker(),
				new Sentinel(),
				new Bonewalker(),
				new Garuda());
			CombatManager combatManager = new(team1, team2);
			combatManager.Fight();
		}
	}
}