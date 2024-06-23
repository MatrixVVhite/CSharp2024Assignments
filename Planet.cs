public class Planet : IStellarObject
{
	public string Name { get; set;}
	public double DistanceToRef { get; set;}
	public SpaceTime SpaceTime { get; set; }

	public Planet(string name, double distanceToRef)
	{
		Name = name;
		DistanceToRef = distanceToRef;
	}

	public override string ToString() => $"Planet: {Name}";
}
