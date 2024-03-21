namespace LINQ
{
	public static class ExtendingQueryOperations
	{
		public static void Print<T>(this IEnumerable<T> collection)
		{
			foreach (var e in collection)
				Console.WriteLine(e);
		}
	}
}
