namespace BloodyShapes.Shapes
{
	class Triangle : Shape
	{
		public float Base => Width;

		public Triangle(float @base = 1, float height = 1, float posX = 0, float posY = 0) : base(@base, height, posX, posY) { }

		public override float Area() => Base * Height * .5f;

		public override float Perimeter() => Base + Height + (float)Math.Sqrt((Base * Base) + (Height * Height));

		public override string ToString()
		{
			return $"Triangle of dimensions {Base}x{Height} centered on ({PositionX}, {PositionY})";
		}

		public override bool Equals(object? obj)
		{
			if (obj is Triangle other)
				return Base == other.Base & Height == other.Height & PositionX == other.PositionX & PositionY == other.PositionY;
			else
				return false;
		}
	}
}
