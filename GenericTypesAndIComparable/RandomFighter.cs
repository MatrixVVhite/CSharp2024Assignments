using GenericTypesAndIComparable.Structs;

namespace GenericTypesAndIComparable
{
	static class RandomFighter
	{
		public static Results Fight<T>(Deck<T> deck, Dice<T> die) where T : struct, IComparable<T>
		{
			if (die.Scalar != 1)
				throw new ArgumentException("Die cannot contain more or less than a single die.");
			Results results = new();
			while (deck.TryDraw(out T drawn))
			{
				T rolled = die.Roll().Single();
				switch (drawn.CompareTo(rolled))
				{
					case 1:
						results.DeckWinCount++;
						break;
					case -1:
						results.DiceWinCount++;
						break;
					case 0:
						results.TieCount++;
						break;
				}
			}
			return results;
		}

		public struct Results
		{
			public uint DeckWinCount { get; set; }
			public uint DiceWinCount { get; set; }
			public uint TieCount { get; set; }
			public uint TotalRoundsCount => DeckWinCount + DiceWinCount + TieCount;
			public bool DeckWinner => DeckWinCount > DiceWinCount;
			public bool DiceWinner => DiceWinCount > DeckWinCount;
			public bool Tie => DeckWinCount == DiceWinCount;

			public override string ToString()
			{
				string winner;
				string results;
				if (DeckWinner)
					winner = "Deck wins:";
				else if (DiceWinner)
					winner = "Dice wins:";
				else
					winner = "Tie:";
				results = @$"
Deck - {DeckWinCount}
Dice - {DiceWinCount}
Ties = {TieCount}";
				return winner + results;
			}

			public static Results operator+(Results left, Results right)
			{
				left.DiceWinCount += right.DiceWinCount;
				left.DeckWinCount += right.DeckWinCount;
				left.TieCount += right.TieCount;
				return left;
			}
		}
	}
}
