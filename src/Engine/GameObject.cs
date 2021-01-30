using System.Drawing;

namespace GameEngine
{
	public class GameObject
	{
		public Vector2 Position;
		public Vector2 Scale;
		public Bitmap Sprite;
		public GameObjectType Type;

		public GameObject(GameObjectType type, Vector2 position, Vector2 scale, Bitmap sprite)
		{
			Type = type;
			Scale = scale;
			Sprite = sprite;
			Position = position;

			Log.Info($"[GameObject]({type}) - Has been created!");
		}
	}
}
