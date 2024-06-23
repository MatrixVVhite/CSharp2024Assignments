public class Star : IStellarObject
{
	public string Name { get; set; }
	public double DistanceToRef { get; set; }
	public SpaceTime SpaceTime { get; set; }
	public char Type { get; set; }

	public Star(string name, double distanceToRef, char type)
	{
		Name = name;
		DistanceToRef = distanceToRef;
		Type = type;
	}

	public void SendPulse()
	{
		Console.WriteLine($"\n{this} lets out a pulse.\n");
		SpaceTime.PropagatePulse(this);
	}

	public override string ToString() => $"Star: {Name}";
}
