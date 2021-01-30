namespace GameEngine
{
	public class Vector2
	{
		public float X { get; set; }
		public float Y { get; set; }

		public Vector2(float x, float y)
		{
			X = x;
			Y = y;
		}

		public static Vector2 Zero()
		{
			return new Vector2(0, 0);
		}
	}
}
