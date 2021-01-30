using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GameEngine
{
	public class Game : Engine
	{
		public Game() : base("Game Engine", new Vector2(1200, 1000)) { }

		BitmapProvider BitmapProvider { get; } = new BitmapProvider();
		GameObject _player;

		public override void OnLoad()
		{
			var map = MapProvider.Get;
			var objects = new List<GameObject>();
			for (int i = 0; i < map.GetLength(1); i++)
			{
				for (int j = 0; j < map.GetLength(0); j++)
				{
					if (map[j, i] == 'p')
					{
						var x = GetCenter(i, MapProvider.PlayerWidth);
						var y = GetCenter(j, MapProvider.PlayerHeight);
						_player = new GameObject(GameObjectType.Player, new Vector2(x, y), new Vector2(MapProvider.PlayerWidth, MapProvider.PlayerHeight), BitmapProvider.Get(GameObjectType.Player));
						objects.Add(_player);
					}

					if (map[j, i] == 'g')
					{
						var tile = new GameObject(GameObjectType.Tile, new Vector2(i * MapProvider.TileWidth, j * MapProvider.TileHeight), new Vector2(MapProvider.TileWidth, MapProvider.TileHeight), BitmapProvider.Get(GameObjectType.Tile));
						objects.Add(tile);
					}

					if (map[j, i] == 'c')
					{
						var x = GetCenter(i, MapProvider.CoinWidth);
						var y = GetCenter(j, MapProvider.CoinHeight);
						var tile = new GameObject(GameObjectType.Coin, new Vector2(x, y), new Vector2(MapProvider.CoinWidth, MapProvider.CoinHeight), BitmapProvider.Get(GameObjectType.Coin));
						objects.Add(tile);
					}
				}
			}

			GameObjects.AddRange(objects);
			AdjustCamera();
		}

		int GetCenter(int mapIndex, int gameObjectDimension) => (mapIndex * MapProvider.TileWidth) + (MapProvider.TileWidth / 2 - (gameObjectDimension / 2));

		public override void OnDraw()
		{
		}

		bool _up;
		bool _down;
		bool _left;
		bool _right;
		bool _shift;

		public override void OnUpdate()
		{
			const double threshold = 50;
			GameObjectsInReach = GameObjects.Where(x => x.Type != GameObjectType.Player && _player.GetDistance(x) < threshold);
			UpdatePosition();

			var colideElement = GameObjectsInReach.FirstOrDefault(tile => _player.IsColliding(tile));
			if (colideElement?.Type == GameObjectType.Coin)
				GameObjects.Remove(colideElement);
		}

		void UpdatePosition()
		{
			var playerSpeed = _shift ? 5f : 3f;
			if (_up) TryUpdatePosition(pos => pos.Y -= playerSpeed);
			if (_down) TryUpdatePosition(pos => pos.Y += playerSpeed);
			if (_left) TryUpdatePosition(pos => pos.X -= playerSpeed);
			if (_right) TryUpdatePosition(pos => pos.X += playerSpeed);
		}

		void TryUpdatePosition(Action<Vector2> updateAction)
		{
			var lastPosition = new { _player.Position.X, _player.Position.Y };
			updateAction(_player.Position);
			var colideElement = GameObjectsInReach.FirstOrDefault(tile => _player.IsColliding(tile));
			if (colideElement?.Type == GameObjectType.Tile)
			{
				_player.Position.X = lastPosition.X;
				_player.Position.Y = lastPosition.Y;
			}
		}

		public override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { _up = true; }
			if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { _left = true; }
			if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { _down = true; }
			if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { _right = true; }
			if (e.KeyCode == Keys.ShiftKey) { _shift = true; }
		}

		public override void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { _up = false; }
			if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { _left = false; }
			if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { _down = false; }
			if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { _right = false; }
			if (e.KeyCode == Keys.ShiftKey) { _shift = false; }
		}
	}
}