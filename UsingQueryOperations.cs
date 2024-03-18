namespace LINQ
{
	public static class UsingQueryOperations
	{
		/// <summary>
		/// Write a C# program that accepts a collection and outputs a Distinct collection arranged in ascending Order.
		/// </summary>
		public static void WorkInClass1()
		{
			var names = new List<string>() { "Neo", "Potato", "Darth", "Potato", "Nova", "Dragon" };
			names.Distinct().OrderBy(s => s).Print();
		}

		/// <summary>
		/// Write a C# program that accepts a sentence (string) and outputs the average word length.
		/// </summary>
		public static void WorkInClass2()
		{
			string sentence = "This is my message to my master";
			Console.WriteLine(sentence.Split(' ').Average(s => s.Length));
		}

		/// <summary>
		/// Write a C# program that accepts two chars (char ‘a’ and char ‘b’) and outputs all strings in the collection that start with char ‘a’ and end with ‘b’
		/// </summary>
		public static void WorkInClass3()
		{
			char a = 'a', b = 'b';
			var list = new List<string>() {"afdb", "fdggd", "ghdfb", "afg", "adfgfb", "ahjhb", "bfgha"};
			list.Where(l => l.StartsWith(a) && l.EndsWith(b)).Print();
		}

		/// <summary>
		/// Write a C# program that accepts a Student (First Name : string, ID : int) and Grade(ID : int, Grade : int) and Joins them together to create a new collection of (First Name, Grade).
		/// </summary>
		public static void WorkInClass4()
		{
			var students = new List<Student>() { new("Shlomi", 435), new("Maor", 436), new("Noa", 437), new("Moshe", 438)};
			var grades = new List<Grade>() { new(435, 1), new(436, 3), new(437, 3), new(438, 2) };
			students.Join(grades, s => s.id, g => g.id, (s, g) => new { s.firstName, g.grade }).Print();
		}

		private static void Print<T>(this IEnumerable<T> collection)
		{
			foreach (var e in collection)
				Console.WriteLine(e);
		}

		private class Student
		{
			public string firstName;
			public int id;

			public Student(string firstName, int id)
			{
				this.firstName = firstName;
				this.id = id;
			}
		}

		private class Grade
		{
			public int id;
			public int grade;

			public Grade(int id, int grade)
			{
				this.id = id;
				this.grade = grade;
			}
		}
	}
}
