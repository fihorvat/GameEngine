using System;

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

		//Returns the distance between two center points
		public static double GetDistance(this GameObject a, GameObject b)
		{
			var first = new { X = (a.Position.X + a.Scale.X) / 2, Y = (a.Position.Y + a.Scale.Y) / 2 };
			var second = new { X = (b.Position.X + b.Scale.X) / 2, Y = (b.Position.Y + b.Scale.Y) / 2 };
			return GetDistance(first.X, first.Y, second.X, second.Y);
		}

		static double GetDistance(double x1, double y1, double x2, double y2) => Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
	}
}