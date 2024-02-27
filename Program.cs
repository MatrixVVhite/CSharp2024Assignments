// ---- C# II (Dor Ben Dor) ----
//       Nehorai Buader
// ----------------------------
// Assignment 07 - Events and Delegates
public class Program
{
	public static void Main()
	{
		ObservableLimitedList<string> list = new(s => IsPalindrome(s));
		list.ListChanged += s => Console.WriteLine(s);
		var str = "Wait until they see the Hannah Montana episode with a racecar The kayak cop with a radar sees a nun in a Civic Those were some next level stats".Split(' ');
		foreach (var s in str)
			list.Add(s);
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