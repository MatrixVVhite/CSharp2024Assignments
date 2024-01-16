namespace BloodyShapes.Shapes
{
	class Rectangle : Shape
	{
		public Rectangle(float width = 1, float height = 1, float posX = 0, float posY = 0) : base(width, height, posX, posY) { }

		public override float Area() => Width * Height;

		public override float Perimeter() => (Width * 2) + (Height * 2);

		public override string ToString() => $"Rectangle of dimensions {Width}x{Height} centered on ({PositionX}, {PositionY})";

		public override bool Equals(object? obj)
		{
			if (obj is Rectangle other)
				return Width == other.Width & Height == other.Height & PositionX == other.PositionX & PositionY == other.PositionY;
			else
				return false;
		}
	}
}
