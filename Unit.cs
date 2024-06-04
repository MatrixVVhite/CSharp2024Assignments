
public class Unit : IPrintable
{
	public string Name { get; protected set; }
	public uint Level { get; protected set; }
	public UnitStats Stats { get; protected set; }
	public Weapon Weapon { get; set; }

	public Unit(string name, uint level, UnitStats stats)
	{
		Name = name;
		Level = level;
		Stats = stats;
	}

	public void LevelUp(uint count = 1)
	{
		Level++;
		for (int i = 0; i < count; i++)
			Stats.LevelUp();
	}

	public override string ToString() => $"{Name} Lv{Level}";

	public string Print()
	{
		return ToString();
	}

	public struct UnitStats : IComparable<UnitStats>
	{
		private Stat[] Stats { get; set; }
		public Stat HP { get => Stats[0]; set => Stats[0] = value; }
		public Stat STR { get => Stats[1]; set => Stats[1] = value; }
		public Stat MAG { get => Stats[2]; set => Stats[2] = value; }
		public Stat DEX { get => Stats[3]; set => Stats[3] = value; }
		public Stat SPD { get => Stats[4]; set => Stats[4] = value; }
		public Stat DEF { get => Stats[5]; set => Stats[5] = value; }
		public Stat RES { get => Stats[6]; set => Stats[6] = value; }
		public Stat LCK { get => Stats[7]; set => Stats[7] = value; }
		public Stat BLD { get => Stats[8]; set => Stats[8] = value; }
		public Stat MOV { get => Stats[9]; set => Stats[9] = value; }
		public long StatTotal => Stats.Sum(s => s.Value);

		public UnitStats()
		{
			Stats = new Stat[10];
			HP = new(22, 0.6f);
			STR = new(6, 0.35f);
			MAG = new(0, 0.2f);
			DEX = new(5, 0.45f);
			SPD = new(7, 0.5f);
			DEF = new(5, 0.4f);
			RES = new(3, 0.25f);
			LCK = new(5, 0.25f);
			BLD = new(4, 0.05f);
			MOV = new(4, 0.0f);
		}

		public UnitStats(Stat hp, Stat str, Stat mag, Stat dex, Stat spd, Stat def, Stat res, Stat lck, Stat mov, Stat bld)
		{
			Stats = new Stat[10];
			HP = hp;
			STR = str;
			MAG = mag;
			DEX = dex;
			SPD = spd;
			DEF = def;
			RES = res;
			LCK = lck;
			BLD = bld;
			MOV = mov;
		}

		public void LevelUp()
		{
			for (int i = 0; i < Stats.Length; i++)
				Stats[i].TryGrow();
		}

		public int CompareTo(UnitStats other) => StatTotal.CompareTo(other.StatTotal);
	}

	public struct Stat : IComparable<Stat>
	{
		public uint Value { get; private set; }
		public float GrowthRate { get; private init; }

		public Stat(uint baseStat = 1, float growthRate = 0.5f)
		{
			Value = baseStat;
			GrowthRate = ClampRange(growthRate, 0f, 1f);
		}

		public bool TryGrow()
		{
			if (RollChance(GrowthRate))
			{
				Value++;
				return true;
			}
			return false;
		}

		private static float ClampRange(float val, float min, float max) => Math.Max(Math.Min(val, max), min);

		private static bool RollChance(float chanceToSucceed) => chanceToSucceed > Random.Shared.NextDouble();

		public int CompareTo(Stat other) => Value.CompareTo(other.Value);
	}
}
