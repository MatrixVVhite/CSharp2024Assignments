using System.Collections;

public class SolarSystem : IStellarObject, IEnumerable<IStellarObject>
{
	public string Name { get; private set; }
	public double DistanceToRef => Stars.Average(x => x.DistanceToRef);
	public List<Star> Stars { get; private set; }
	public List<Planet> Planets { get; private set; }
	public SpaceTime SpaceTime { get; set; }

	public SolarSystem(IEnumerable<Star> stars, IEnumerable<Planet> planets, string? name = null)
	{
		Stars = stars.ToList();
		Planets = planets.ToList();
		if (string.IsNullOrEmpty(name))
			Name = stars.First().Name;
		else
			Name = name;
	}

	public IEnumerator<IStellarObject> GetEnumerator()
	{
		yield return this;
		foreach (IStellarObject obj in Stars)
			yield return obj;
		foreach (IStellarObject obj in Planets)
			yield return obj;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		foreach (IStellarObject obj in Stars)
			yield return obj;
		foreach (IStellarObject obj in Planets)
			yield return obj;
	}

	public override string ToString() => $"System: {Name}";
}
