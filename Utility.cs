static class Utility
{
	public static void Swap<T>(ref T obj1, ref T obj2) => (obj2, obj1) = (obj1, obj2);

	public static float ClampMin(float val, float min) => Math.Max(val, min);

	public static float ClampMax(float val, float max) => Math.Min(val, max);

	public static float ClampRange(float val, float min, float max) => ClampMin(ClampMax(val, max), min);

	public static int ClampMin(int val, int min) => Math.Max(val, min);

	public static int ClampMax(int val, int max) => Math.Min(val, max);

	public static int ClampRange(int val, int min, int max) => ClampMin(ClampMax(val, max), min);

	public static void BlockUntilKeyDown() => Console.ReadKey();
}