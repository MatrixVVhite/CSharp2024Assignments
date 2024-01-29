using System.Collections;

namespace Berzerkers.Combat
{
	public class CombatManager
	{
		private int _currentTeamIndex;

		private List<Team> Teams { get; set; }
		private int CurrentTeamIndex { get => _currentTeamIndex; set => _currentTeamIndex = value % TeamsLeft; }
		private WeatherEffect CurrentWeatherEffect { get; set; }
		private int CurrentWeatherStrength { get; set; }
		private Team CurrentTeam => Teams[CurrentTeamIndex];
		private int TeamsLeft => Teams.Count;
		private bool ContinueCombat => TeamsLeft > 1;
		private List<Unit.Unit> DeadUnits {get; set;}

		public CombatManager(List<Team> teams)
		{
			Teams = teams;
			int unitsCount = Teams.Sum(t => t.UnitCount);
			DeadUnits = new(unitsCount);
		}

		public CombatManager(params Team[] teams) : this(new List<Team>(teams)) { }

		public void Fight()
		{
			CombatStart();
			if (ContinueCombat)
			{
				CombatLoop();
				CombatEnd();
			}
		}

		private void CombatStart()
		{
			PrintCombatStarted();
			CurrentTeamIndex = GetRandomTeamIndex();
			CurrentWeatherEffect = WeatherEffect.Clear;
			CurrentWeatherStrength = 0;
		}

		private void CombatLoop()
		{
			while (ContinueCombat)
			{
				CurrentTeamIndex++;
				TryChangeWeather();
				CurrentTeamActs();
				ApplyWeatherEffect();
				RemoveDead();
			}
		}

		private void CombatEnd()
		{
			PrintCombatEnded();
		}

		private int GetRandomTeamIndex() => Random.Shared.Next(TeamsLeft);

		private Team? GetWinningTeam() => TeamsLeft == 1 ? Teams[0] : null;

		private void CurrentTeamActs()
		{
			Console.WriteLine($"\t{CurrentTeam}'s turn:");
			var actingUnit = CurrentTeam.GetRandom();
			var defendingTeam = Teams.Where(t => t != CurrentTeam).GetRandom();
			var defendingUnit = defendingTeam.GetRandom();
			actingUnit.Attack(defendingUnit);
			PrintIfKilled(defendingUnit, actingUnit);
			PrintIfKilled(actingUnit, defendingUnit);
		}

		private void PrintCombatStarted()
		{
			switch (TeamsLeft)
			{
				case 0:
					Console.WriteLine("Combat has started but no one bothered.");
					break;
				case 1:
					Console.WriteLine($"{CurrentTeam} has entered an empty battlefield.");
					break;
				case 2:
					Console.WriteLine($"{Teams[0]} dukes it out vs {Teams[1]}.");
					break;
				default:
					Console.WriteLine($"{TeamsLeft} teams battle it out.");
					break;
			}
		}

		private void PrintCombatEnded()
		{
			switch (TeamsLeft)
			{
				case 1:
					Team winningTeam = GetWinningTeam()!.Value;
					int recoveredLoot = DeadUnits.Sum(u => u.Loot);
					Console.WriteLine($"{winningTeam} wins with {winningTeam.UnitCount} unit/s left\n{winningTeam} has gained {recoveredLoot} loot.");
					break;
				case 0:
					Console.WriteLine("No team has survived.");
					break;
				default:
					Console.WriteLine("Combat has ended prematurely.");
					break;
			}
		}

		private static bool PrintIfKilled(Unit.Unit killed, Unit.Unit by)
		{
			if (killed.IsDead)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"{killed} was killed by {by}.");
				Console.ForegroundColor = ConsoleColor.Gray;
				return true;
			}
			return false;
		}

		private void RemoveDead()
		{
			Teams.ForEach(t => DeadUnits.AddRange(t.GetDeadUnits()));
			Teams.ForEach(t => t.RemoveDeadUnits());
			Teams.RemoveAll(t => t.AllUnitsDead);
		}

		private bool TryChangeWeather()
		{
			Dice changeWeatherDice = new(2, 6);
			if (changeWeatherDice.Roll() > CurrentWeatherStrength)
			{
				CurrentWeatherEffect = GetRandomWeatherEffect();
				Console.WriteLine($"The weather has changed to {CurrentWeatherEffect}.");
				return true;
			}
			CurrentWeatherStrength--;
			return false;
		}

		private void ApplyWeatherEffect()
		{
			Teams.ForEach(t => t.units.ForEach(u => u.ApplyWeatherEffect(CurrentWeatherEffect)));
		}

		private static WeatherEffect GetRandomWeatherEffect()
		{
			return (WeatherEffect)typeof(WeatherEffect).GetRandom();
		}
	}

	public readonly struct Team : ICollection<Unit.Unit>, IEnumerable<Unit.Unit>, IEquatable<Team>
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

		public int RemoveDeadUnits() => units.RemoveAll(u => u.IsDead);

		#region OVERRIDES
		public override string ToString() => name;

		public bool Equals(Team other) => name == other.name && units == other.units;

		public override bool Equals(object obj) => obj is Team team && Equals(team);

		public static bool operator ==(Team left, Team right) => left.Equals(right);

		public static bool operator !=(Team left, Team right) => !(left == right);

		public override int GetHashCode() => name.GetHashCode() ^ units.GetHashCode();

		#region INTERFACES
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
