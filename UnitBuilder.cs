public static class UnitBuilder
{
	public static Unit MakeRandomUnit(string name = "Unit", uint levelUpsCount = 0)
	{
		Unit unit = new(name, 1, new Unit.UnitStats());
		LevelUpUnit(unit, levelUpsCount);
		return unit;
	}

	private static void LevelUpUnit(Unit unit, uint count = 1)
	{
		for (uint i = 0; i < count; i++)
			unit.LevelUp();
	}
}
