// ---- C# II (Dor Ben Dor) ----
//       Nehorai Buader
// ----------------------------
// Assignment 08 - Generic Types and IComparable
using GenericTypesAndIComparable.Structs;

namespace GenericTypesAndIComparable
{
    public static class Program
	{
		public static void Main()
		{
			RandomFighter();
		}

		private static void RandomFighter()
		{
			var arr = new int[40];
			for (int i = 0; i < arr.Length; i++)
				arr[i] = Random.Shared.Next(20);
			var deck = new Deck<int>(arr);
			var die = new Dice(1, 20);
			var results = GenericTypesAndIComparable.RandomFighter.Fight(deck, die);
			Console.WriteLine(results);
		}
	}
}