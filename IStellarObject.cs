public interface IStellarObject
{
	string Name { get; }
	double DistanceToRef { get; }
	SpaceTime SpaceTime { get; set; }

	void ReceivePulse(Star star)
	{
		Console.WriteLine($"{Name} received {star.Type}-signature pulse from {star.Name} in {SpaceTime.TimeToString(SpaceTime.TimeTo(Distance(star, this)))}");
	}

	static double Distance(IStellarObject a, IStellarObject b) => Distance(a.DistanceToRef, b.DistanceToRef);
	
	static double Distance(double a, double b) => Math.Abs(b - a);
}
