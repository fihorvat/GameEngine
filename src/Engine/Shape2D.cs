using System.Drawing;

namespace GameEngine
{
	public class Shape2D
	{
		public Vector2 Position = null;
		public Vector2 Scale = null;
		public string Tag = null;
		public Bitmap Sprite = null;

		public Shape2D(Vector2 position, Vector2 scale, string directory, string tag)
		{
			Position = position;
			Scale = scale;
			Tag = tag;
			var image = Image.FromFile(directory);
			Sprite = new Bitmap(image);
			Engine.RegisterShape(this);
		}

		public void DestroySelf()
		{
			Engine.UnRegisterShape(this);
		}
	}
}
