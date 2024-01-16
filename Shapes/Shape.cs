namespace BloodyShapes.Shapes
{
	abstract class Shape
	{
		public float PositionX { get; protected set; }
		public float PositionY { get; protected set; }
		public float Width { get; protected set; }
		public float Height { get; protected set; }

		protected Shape(float width = 1, float height = 1, float posX = 0, float posY = 0)
		{
			PositionX = posX;
			PositionY = posY;
			Width = width;
			Height = height;
		}

		public abstract float Area();

		public abstract float Perimeter();

		public virtual void Draw() => Console.WriteLine(this);
	}
}
