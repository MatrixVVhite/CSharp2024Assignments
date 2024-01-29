namespace Berzerkers
{
	using Combat;
	using Combat.Unit.UnitTypes.Races.Bio;
	using Combat.Unit.UnitTypes.Races.Cyber;
	using Combat.Unit.UnitTypes.Races.Void;

	public class Program
	{
		public static void Main()
		{
			TestCombatManager();
		}

		private static void TestCombatManager()
		{
			Team cradle = new(
				"The Cradle",
				new WarCleric("War Cleric 1"),
				new Cataphract("Cataphract 1"),
				new WarCleric("War Cleric 2"),
				new Cataphract("Cataphract 2"),
				new Hegemon("Hegemon"));
			Team horizon = new(
				"The Horizon",
				new Sentinel("Sentinel 1"),
				new Bonewalker("Bonewalker 1"),
				new Sentinel("Sentinel 2"),
				new Bonewalker("Bonewalker 2"),
				new Garuda("Garuda"));
			Team rust = new(
				"Rust and Dust",
				new Blitzer("Blitzer 1"),
				new MetalVanguard("Metal Vanguard 1"),
				new Blitzer("Blitzer 2"),
				new MetalVanguard("Metal Vanguard 2"),
				new Draedon("Draedon"));
			List<Team> teams = new() { cradle, horizon, rust };
			CombatManager combatManager = new(teams);
			combatManager.Fight();
		}
	}
}