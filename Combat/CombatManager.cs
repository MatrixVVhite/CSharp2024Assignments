using System.Collections;

namespace Berzerkers.Combat
{
	public class CombatManager
	{
		private int _currentTeamIndex;

		private List<Team> Teams { get; set; }
		private int CurrentTeamIndex { get => _currentTeamIndex; set => _currentTeamIndex = value % TeamsLeft; }
		private WeatherEffect CurrentWeatherEffect { get; set; }
		private Team CurrentTeam => Teams[CurrentTeamIndex];
		private int TeamsLeft => Teams.Count;

		public CombatManager(List<Team> teams)
		{
			Teams = teams;
		}

		public CombatManager(params Team[] teams) : this(new List<Team>(teams)) { }

		public void Fight()
		{
			CombatStart();
			CombatLoop();
			CombatEnd();
		}

		private void CombatStart()
		{
			CurrentTeamIndex = GetRandomTeamIndex();
			Console.WriteLine($"Team {CurrentTeam} gets to go first.");
		}

		private void CombatLoop()
		{
			while (TeamsLeft > 1)
			{
				TryChangeWeather();
				CurrentTeamActs();
				ApplyWeatherEffect();
				RemoveDead();
				CurrentTeamIndex++;
			}
		}

		private void CombatEnd()
		{
			switch (TeamsLeft)
			{
				case 1:
					Console.WriteLine($"Team {CurrentTeam} wins.");
					break;
				case 0:
					Console.WriteLine("No team has survived.");
					break;
				default:
					Console.WriteLine("Combat has ended prematurely.");
					break;
			}
		}

		private int GetRandomTeamIndex() => Random.Shared.Next(TeamsLeft);

		private Team GetRandomTeam() => Teams.GetRandom();

		private void CurrentTeamActs()
		{
			var actingUnit = CurrentTeam.GetRandom();
			var defendingTeam = Teams.Where(t => t != CurrentTeam).GetRandom();
			var defendingUnit = defendingTeam.GetRandom();
			actingUnit.Attack(defendingUnit);
		}

		private void RemoveDead()
		{
			Teams.ForEach(t => t.RemoveDeadUnits());
			Teams.RemoveAll(t => t.AllUnitsDead);
		}

		private bool TryChangeWeather()
		{
			throw new NotImplementedException();
		}

		private void ApplyWeatherEffect()
		{
			throw new NotImplementedException();
		}
	}

	public readonly struct Team : ICollection<Unit.Unit>, IEquatable<Team>
	{
		public readonly string name;
		public readonly List<Unit.Unit> units;

		public int UnitCount => units.Count;
		public bool AllUnitsDead => UnitCount <= 0;

		public Team(string name, List<Unit.Unit> units)
		{
			this.name = name;
			this.units = units;
		}

		public Team(string name, params Unit.Unit[] units) : this(name, new List<Unit.Unit>(units)) { }

		public Unit.Unit[] GetDeadUnits() => units.Where(u => u.IsDead).ToArray();

		public Unit.Unit PickRandomUnit() => units.GetRandom();

		public int RemoveDeadUnits() => units.RemoveAll(u => u.IsDead);

		#region OVERRIDES
		public override string ToString() => name;

		public bool Equals(Team other) => name == other.name && units == other.units;

		public override bool Equals(object obj) => obj is Team team && Equals(team);

		public static bool operator ==(Team left, Team right) => left.Equals(right);

		public static bool operator !=(Team left, Team right) => !(left == right);

		public override int GetHashCode() => name.GetHashCode() ^ units.GetHashCode();

		#region ICOLLECTION
		public int Count => (units as ICollection<Unit.Unit>).Count;
		public bool IsReadOnly => (units as ICollection<Unit.Unit>).IsReadOnly;

		public void Add(Unit.Unit item) => (units as ICollection<Unit.Unit>).Add(item);

		public void Clear() => (units as ICollection<Unit.Unit>).Clear();

		public bool Contains(Unit.Unit item) => (units as ICollection<Unit.Unit>).Contains(item);

		public void CopyTo(Unit.Unit[] array, int arrayIndex) => (units as ICollection<Unit.Unit>).CopyTo(array, arrayIndex);

		public bool Remove(Unit.Unit item) => (units as ICollection<Unit.Unit>).Remove(item);

		public IEnumerator<Unit.Unit> GetEnumerator() => (units as IEnumerable<Unit.Unit>).GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => (units as IEnumerable).GetEnumerator();
		#endregion
		#endregion
	}
}
