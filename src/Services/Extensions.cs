namespace GameEngine
{
	public static class Extensions
	{
		public static bool IsColliding(this GameObject a, GameObject b)
		{
			if (a.Position.X < b.Position.X + b.Scale.X &&
					a.Position.X + a.Scale.X > b.Position.X &&
					a.Position.Y < b.Position.Y + b.Scale.Y &&
					a.Position.Y + a.Scale.Y > b.Position.Y)
				return true;
			return false;
		}
	}
}