public abstract class UnitFactory
{
	public virtual Unit MakeUnit(string name = "Unit", uint level = 0)
	{
		return UnitBuilder.MakeRandomUnit(name, level);
	}
}

public class FightersGuild : UnitFactory
{
	public override Unit MakeUnit(string name = "Unit", uint level = 0)
	{
		Unit unit = base.MakeUnit(name, level);
		unit.Weapon = new MeleeWeapon(level/5);
		return unit;
	}
}

public class Church : UnitFactory
{
	public override Unit MakeUnit(string name = "Unit", uint level = 0)
	{
		Unit unit = base.MakeUnit(name, level);
		unit.Weapon = new Staff(level / 4);
		return unit;
	}
}
