public static class Program
{
	public static void Main()
	{
		var space = new SpaceTime(GetSystems());
		Star theSun = space.PointOfReference as Star;
		theSun.SendPulse();
		Star proxima = space.First(obj => obj is Star star && star.Type == 'M') as Star;
		proxima.SendPulse();
	}

	private static IEnumerable<SolarSystem> GetSystems()
	{
		yield return SolarSystem();
		yield return ProximaCentauri();
	}

	public static SolarSystem SolarSystem()
	{
		return new SolarSystem(
			stars: new[] { new Star("Sol", 0, 'G') },
			planets: new[] {
				new Planet("Mercury", 57.91e+9),
				new Planet("Venus", 108.21e+9),
				new Planet("Earth", 149.6e+9),
				new Planet("Mars", 227.94e+9),
				new Planet("Jupiter", 778.48e+9),
				new Planet("Saturn", 1433.53e+9),
				new Planet("Uranus", 2870.97e+9),
				new Planet("Neptune", 4498.41e+9)
			},
			name: "Solar System"
			);
	}

	public static SolarSystem ProximaCentauri()
	{
		var distance = 4.0174992e+16;
		return new SolarSystem(
			stars: new[] { new Star("Proxima", distance, 'M') },
			planets: new[] {
				new Planet("Proxima d", distance + 43.15e+9),
				new Planet("Proxima b", distance + 72.65e+9),
				new Planet("Proxima c", distance + 129.31e+9),
			},
			name: "Proxima Centauri"
			);
	}
}
