using System.Windows.Forms;

namespace GameEngine
{
	public class Game : Engine
	{
		public Game() : base("Game Engine" ,new Vector2(615, 512)) { }
		Shape2D Player;

		bool Up = false;
		bool Down = false;
		bool Left = false;
		bool Right = false;

		public override void OnLoad()
		{
			Player = new Shape2D(new Vector2(30, 30), new Vector2(50, 50), "Assets/Player.png", "Player");

			for (int i = 0; i < Map.GetLength(1); i++)
			{
				for (int j = 0; j < Map.GetLength(0); j++)
				{
					if (Map[j, i] == "g")
					{
						new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(50, 50), "Assets/Tile.png", "Tile");
					}
				}
			}
		}

		public override void OnDraw()
		{
		}

		public override void OnUpdate()
		{
			if (Up)
			{
				Player.Position.Y -= 1f;
				Log.Warning($"{Player.Position.X}:{Player.Position.Y}");
			}
			if (Down)
			{
				Player.Position.Y += 1f;
				Log.Warning($"{Player.Position.X}:{Player.Position.Y}");
			}
			if (Left) 
			{ 
				Player.Position.X -= 1f;
				Log.Warning($"{Player.Position.X}:{Player.Position.Y}");
			}
			if (Right)
			{
				Player.Position.X += 1f;
				Log.Warning($"{Player.Position.X}:{Player.Position.Y}");
			}
			
		}

		public override void OnKeyDown(KeyEventArgs e)
		{
			Log.Info(e.KeyCode.ToString());
			Log.Warning($"{Player.Position.X}:{Player.Position.Y}");
			if(e.KeyCode == Keys.W) { Up = true; }
			if(e.KeyCode == Keys.A) { Left = true; }
			if(e.KeyCode == Keys.S) { Down = true; }
			if(e.KeyCode == Keys.D) { Right = true; }
		}

		public override void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.W) { Up = false; }
			if (e.KeyCode == Keys.A) { Left = false; }
			if (e.KeyCode == Keys.S) { Down = false; }
			if (e.KeyCode == Keys.D) { Right = false; }
		}

		string[,] Map = {
			{".", ".", ".", ".", ".", ".", ".", "."},
			{".", ".", ".", ".", ".", ".", ".", "."},
			{".", ".", ".", ".", "g", ".", ".", "."},
			{".", ".", ".", ".", "g", ".", ".", "."},
			{".", ".", ".", ".", "g", ".", ".", "."},
			{".", ".", ".", ".", "g", ".", ".", "."},
			{".", ".", ".", ".", "g", ".", ".", "."},
	};
	}
}
