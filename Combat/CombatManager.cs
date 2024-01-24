﻿using System.Collections;

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
				// TODO Combat
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

		private void RemoveDead()
		{
			Teams.ForEach(t => t.RemoveDeadUnits());
			Teams.RemoveAll(t => t.AllUnitsDead);
		}

		private void TryChangeWeather()
		{
			throw new NotImplementedException();
		}
	}

	public readonly struct Team : ICollection<Unit.Unit>
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

		#region ICOLLECTION
		public int Count => ((ICollection<Unit.Unit>)units).Count;
		public bool IsReadOnly => ((ICollection<Unit.Unit>)units).IsReadOnly;

		public void Add(Unit.Unit item)
		{
			((ICollection<Unit.Unit>)units).Add(item);
		}

		public void Clear()
		{
			((ICollection<Unit.Unit>)units).Clear();
		}

		public bool Contains(Unit.Unit item)
		{
			return ((ICollection<Unit.Unit>)units).Contains(item);
		}

		public void CopyTo(Unit.Unit[] array, int arrayIndex)
		{
			((ICollection<Unit.Unit>)units).CopyTo(array, arrayIndex);
		}

		public bool Remove(Unit.Unit item)
		{
			return ((ICollection<Unit.Unit>)units).Remove(item);
		}

		public IEnumerator<Unit.Unit> GetEnumerator()
		{
			return ((IEnumerable<Unit.Unit>)units).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)units).GetEnumerator();
		}
		#endregion
		#endregion
	}
}
