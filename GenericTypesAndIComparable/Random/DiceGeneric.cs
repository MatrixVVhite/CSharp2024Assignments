public class Dice<T> where T : IComparable<T>
{
	public int Scalar { get; init; }
	public T[] BaseDie { get; init;}

	public Dice(int scalar, ICollection<T> baseDie)
	{
		Scalar = scalar;
		BaseDie = baseDie.ToArray();
	}

	public virtual T[] Roll()
	{
		T[] result = new T[Scalar];
		for (int d = 0; d < Scalar; d++)
			result[d] = RollSingle();
		return result;
	}

	private T RollSingle() => BaseDie.GetRandom();
}
