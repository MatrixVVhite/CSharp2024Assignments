public static class Program
{
	const uint MEMBERS_COUNT = 4, MIN_LEVEL = 10, MAX_LEVEL = 15;
	public static void Main()
	{
		var party = PartyBuilder.MakeParty(MEMBERS_COUNT, MIN_LEVEL, MAX_LEVEL);
		Console.WriteLine(party.Print());
	}
}
