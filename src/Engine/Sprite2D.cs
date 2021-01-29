using System.Drawing;

namespace GameEngine
{
	public class Sprite2D
	{
		public Vector2 Position = null;
		public Vector2 Scale= null;
		public string Directory = null;
		public string Tag = null;
		public Bitmap Sprite = null;

		public Sprite2D(Vector2 position, Vector2 scale, string directory, string tag)
		{
			Position = position;
			Scale = scale;
			Directory = directory;
			Tag = tag;

			var image = Image.FromFile(directory);
			Sprite = new Bitmap(image);

			Log.Info($"[SHAPE2d]({tag}) - Has been registered!");
			Engine.RegisterSprite(this);
		}

		public void DestroySelf()
		{
			Engine.UnRegisterSprite(this);
		}
	}
}
