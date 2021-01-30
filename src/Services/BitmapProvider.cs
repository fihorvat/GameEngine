using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameEngine
{
	public class BitmapProvider
	{
		Dictionary<GameObjectType, Bitmap> Images { get; } = new Dictionary<GameObjectType, Bitmap>();

		public Bitmap Get(GameObjectType type)
		{
			if (!Images.ContainsKey(type))
				Images.Add(type, new Bitmap(Image.FromFile(_map(type))));
			return Images[type];
		}

		readonly Func<GameObjectType, string> _map = (GameObjectType type) =>
		{
			switch (type)
			{
				case GameObjectType.Player:
					return "Assets/Player.png";
				case GameObjectType.Tile:
					return "Assets/Tile.png";
				case GameObjectType.Coin:
					return "Assets/Saw.png";
				default:
					throw new Exception("Invalid game object");
			}
		};
	}
}