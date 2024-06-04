public static class PartyBuilder
{
	public static Party MakeParty(uint membersCount, uint minLevel, uint maxLevel)
	{
		if (membersCount == 0)
			throw new ArgumentException("Members count can't be zero.");
		List<UnitFactory> factories = new()
		{
			new FightersGuild(),
			new Church()
		};
		List<Unit> members = new((int)membersCount);
		for (int i = 0; i < membersCount; i++)
			members.Add(factories.GetRandom().MakeUnit(level: (uint)Random.Shared.Next((int)minLevel, (int)maxLevel)));
		return new Party(members, p => p.MaxBy(u => u.Stats.StatTotal));
	}

	static T GetRandom<T>(this List<T> source) => source.ElementAt(Random.Shared.Next(source.Count()));
}
