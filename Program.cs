// ---- C# II (Dor Ben Dor) ----
//       Nehorai Buader
// ----------------------------
// Assignment 9 - The Tree Data Structure (E.C.)
using TheTreeDataStructure;
using Ingredient = TheTreeDataStructure.Tree<string>.Node;

public static class Program
{
	public static void Main()
	{
		var nightsEdge = GetNightsEdgeCraftingTree();
		Console.WriteLine("\nPrinting Night's Edge recipe depth-first:");
		nightsEdge.Print();
		Console.WriteLine("\nPrinting Night's Edge recipe breadth-first:");
		nightsEdge.enumerateBreadthFirst = true;
		nightsEdge.Print();
	}

	public static Tree<string> GetNightsEdgeCraftingTree()
	{
		var nightsEdge = new Ingredient("Night's Edge");
		nightsEdge.AddChild(new Ingredient("Blade of Grass", "Vine x3", "Jungle Spores x15", "Stinger x12"));
		nightsEdge.AddChild(new Ingredient("Muramasa", "Locked Golden Chest"));
		var hellstoneBar = new Ingredient("Hellstone Bar x20", "Obsidian x20", "Hellstone x60");
		nightsEdge.AddChild(new Ingredient("Volcano", hellstoneBar));
		var demoniteBar = new Ingredient("Demonite Bar x10", "Demonite Ore x30");
		nightsEdge.AddChild(new Ingredient("Light's Bane", demoniteBar));
		return new(nightsEdge);
	}

	public static void Print<T>(this IEnumerable<T> collection)
	{
		foreach (var e in collection)
			Console.WriteLine(e);
	}
}
