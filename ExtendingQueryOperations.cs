namespace LINQ
{
	public static class ExtendingQueryOperations
	{
		public static void Print<T>(this IEnumerable<T> collection)
		{
			foreach (var e in collection)
				Console.WriteLine(e);
		}

		public static void PrintStrongestUnit()
		{
			uint levelUpsCount = 39;
			Unit alear = new("Alear", 1, new Unit.UnitStats());
			Unit blear = new("Blear", 1, new Unit.UnitStats());
			Unit clear = new("Clear", 1, new Unit.UnitStats());
			Unit[] units = {alear, blear, clear};
			units.LevelUpUnits(levelUpsCount);
			Unit strongest = units.GetStrongestUnit();
			Console.WriteLine($"{strongest} is the strongest unit with {strongest.Stats.StatTotal} stats total.");
		}

		private static Unit GetStrongestUnit(this IEnumerable<Unit> units) => units.MaxBy(u => u.Stats)!;

		private static void LevelUpUnits(this IEnumerable<Unit> units, uint count = 1)
		{
			for (uint i = 0; i < count; i++)
			{
				foreach (Unit unit in units)
					unit.LevelUp();
			}
		}
	}
}
