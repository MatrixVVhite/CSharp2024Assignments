using System.Collections;
using System.Diagnostics;
using System.Text;

public class SpaceTime : IEnumerable<IStellarObject>
{
	public const double C = 299_792_458;

	public IStellarObject PointOfReference { get; private set; }

	public List<SolarSystem> Systems { get; private set; }

	public SpaceTime(IEnumerable<SolarSystem> systems)
	{
		Systems = systems.ToList();
		var pointOfRef = this.Where(obj => obj is Star).MinBy(s => s.DistanceToRef);
		if (pointOfRef != null)
			PointOfReference = pointOfRef;
		else
			PointOfReference = this.First();
		foreach (var obj in this)
			obj.SpaceTime = this;
	}

	public void PropagatePulse(Star source)
	{
		foreach (var obj in this.OrderBy(obj => IStellarObject.Distance(obj, source)).Where(obj => obj != source))
			obj.ReceivePulse(source);
	}

	public TimeSpan TimeTo(double distance)
	{
		distance -= PointOfReference.DistanceToRef;
		var spanSeconds = distance / C;
		return TimeSpan.FromSeconds(spanSeconds);
	}

	public static string TimeToString(TimeSpan time)
	{
		const uint DAYS_IN_YEAR = 365;
		if (time < TimeSpan.Zero)
			return "0s";
		var sb = new StringBuilder();
		var years = time.Days / DAYS_IN_YEAR;
		if (years > 1)
			sb.Append($"{time.Days / DAYS_IN_YEAR} years, ");
		if (time.Days > 1)
			sb.Append($"{time.Days % DAYS_IN_YEAR} days, ");
		if (time.Hours > 1)
			sb.Append($"{time.Hours} hours, ");
		if (time.Minutes > 1)
			sb.Append($"{time.Minutes} minutes, and ");
		sb.Append($"{time.Seconds} seconds.");
		return sb.ToString();
	}

	public IEnumerator<IStellarObject> GetEnumerator() => Systems.SelectMany(x => x).GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => Systems.SelectMany(x => x).GetEnumerator();
}
