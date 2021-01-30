using System.Drawing;

namespace GameEngine
{
	public class GameObject
	{
		public GameObject(GameObjectType type, Vector2 position, Vector2 scale, Bitmap sprite)
		{
			Type = type;
			Scale = scale;
			Sprite = sprite;
			Position = position;

			Log.Info($"[GameObject]({type}) - Has been created!");
		}

		public Vector2 Position { get; set; }
		public Vector2 Scale { get; set; }
		public Bitmap Sprite { get; set; }
		public GameObjectType Type { get; set; }
	}
}