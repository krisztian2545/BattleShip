namespace Model.Data
{
	public struct Vector
	{
		public static readonly Vector Up = new Vector(0, -1);
		public static readonly Vector Right = new Vector(1, 0);
		public static readonly Vector Down = new Vector(0, 1);
		public static readonly Vector Left = new Vector(-1, 0);

		public int X { get; set; }
		public int Y { get; set; }

		public Vector(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public static Vector operator +(Vector a, Vector b)
		{
			return new Vector(a.X + b.X, a.Y + b.Y);
		}

		public static bool operator ==(Vector a, Vector b)
		{
			return a.X == b.X
				&& a.Y == b.Y;
		}

		public static bool operator !=(Vector a, Vector b)
		{
			return !(a == b);
		}

		public override string ToString()
		{
			return $"[{X}, {Y}]";
		}
	}
}