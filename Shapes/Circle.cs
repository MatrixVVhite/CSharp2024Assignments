namespace BloodyShapes.Shapes
{
	class Circle : Shape
	{
		private const float PI = (float)Math.PI;

		public float Radius { get; protected set; }

		public float Diameter => Radius * 2;

		public Circle(float radius = 1, float posX = 0, float posY = 0) : base(radius * 2, radius * 2, posX, posY)
		{
			Radius = radius;
		}

		public override float Area() => PI * Radius * Radius;

		public override float Perimeter() => Diameter * PI;

		public override string ToString() => $"Circle of radius {Radius} centered on ({PositionX}, {PositionY})";
	}
}
