// ---- C# II (Dor Ben Dor) ----
//       Nehorai Buader
// ----------------------------
// Assignment 07 - Events and Delegates
public class Program
{
	public static void Main()
	{
		// Exercise A - Delegates
		DelegatesExercise();
		//Some funny stuff I did for no reason
		DelegatesExerciseBonus();
	}

	private static void DelegatesExercise()
	{
		Console.WriteLine("--- Exercise A - Delegates ---\nPress any key to run");
		Utility.BlockUntilKeyDown();
		ObservableLimitedList<string> list = new(ContainsTheLetterS);
		list.ListChanged += s => Console.WriteLine(s);
		string[] str = { "Lemonade", "Shield", "Calamity", "Pirate", "Season", "Door", "Lock", "Possessionlessnesses", "Event", "Delegate"};
		foreach (var s in str)
			list.Add(s);
	}

	private static void DelegatesExerciseBonus()
	{
		Console.WriteLine("--- Same as previous, but I used an IsPalindrome condition, just because ---\nPress any key to run");
		Utility.BlockUntilKeyDown();
		ObservableLimitedList<string> list = new(IsPalindrome);
		list.ListChanged += s => Console.WriteLine(s);
		var str = "Wait until they see the Hannah Montana episode with a racecar The kayak cop with a radar sees a nun in a Civic Those were some next level stats".Split(' ');
		foreach (var s in str)
			list.Add(s);
	}

	private static bool ContainsTheLetterS(string s)
	{
		return s.ToLower().Contains('s');
	}

	private static bool IsPalindrome(string s)
	{
		s = s.ToLower();
		for (var i = 0; i < s.Length/2; i++)
		{
			if (s[i] != s[s.Length - i - 1])
				return false;
		}
		return true;
	}
}