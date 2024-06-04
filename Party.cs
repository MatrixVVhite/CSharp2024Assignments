using System.Collections;
using System.Text;

public class Party : IEnumerable<Unit>, IPrintable
{
	private List<Unit> _members;
	public Unit Leader { get; private set; }

	public Party(List<Unit> members, Func<List<Unit>, Unit> pickLeader)
	{
		_members = members;
		Leader = pickLeader(_members);
	}

	public IEnumerator<Unit> GetEnumerator()
	{
		return ((IEnumerable<Unit>)_members).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)_members).GetEnumerator();
	}

	public string Print()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append(Leader.Name).Append("'s party:");
		foreach (Unit u in this)
			sb.Append("\n\t").Append(u.Print());
		return sb.ToString();
	}
}
