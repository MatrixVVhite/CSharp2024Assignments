namespace BloodyShapes.Shapes
{
	sealed class Square : Rectangle
	{
		public float Edge => Width;

		public Square(float edge = 1, float posX = 0, float posY = 0) : base(edge, edge, posX, posY) { }

		public override string ToString() => $"Square of dimensions {Width}x{Height} centered on ({PositionX}, {PositionY})";

		public override int GetHashCode()
		{
			int edgeHash = Edge.GetHashCode();
			short posXHash = (short)PositionX.GetHashCode();
			short posYHash = (short)PositionY.GetHashCode();
			return ((posXHash << 16) + posYHash) ^ edgeHash;
		}
	}
}
