namespace LINQ
{
	public static class ExtendingQueryOperations
	{
		/// <summary>
		/// Write an extension method for IEnumerables that prints all items in the collection to the console.
		/// </summary>
		/// <typeparam name="T">Type of IEnumerable</typeparam>
		/// <param name="collection">Collection whose elements to print</param>
		public static void Print<T>(this IEnumerable<T> collection)
		{
			foreach (var e in collection)
				Console.WriteLine(e);
		}

		/// <summary>
		/// Write an extension method for IEnumerables<T> that returns a T object according to an IComparable comparison on a class member inside T.
		/// For example, in an enemy with a Damage : int component (int is IComparable) could be used to get the enemy with the highest damage in a collection.
		/// </summary>
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
