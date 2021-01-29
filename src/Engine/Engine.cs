using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GameEngine
{
	public abstract class Engine
	{
		Vector2 ScreenSize = null;
		string Title = null;
		Canvas Window = null;
		Thread GameLoopThread = null;
		
		public Color BackgroundColor = Color.Black;
		public Vector2 CameraPosition = Vector2.Zero();
		public float CameraAngle = 0f;

		public Engine(string title, Vector2 screenSize)
		{
			Title = title;
			ScreenSize = screenSize;

			Window = new Canvas
			{
				Size = new Size((int)ScreenSize.X, (int)ScreenSize.Y),
				Text = Title
			};

			Window.FormClosing += Closing;
			Window.Paint += Renderer;
			Window.KeyDown += Window_KeyDown;
			Window.KeyUp += Window_KeyUp;
			OnLoad();

			GameLoopThread = new Thread(GameLoop);
			GameLoopThread.Start();

			Application.Run(Window);
		}

		
		void Window_KeyUp(object sender, KeyEventArgs e)
		{
			OnKeyUp(e);
		}

		
		void Window_KeyDown(object sender, KeyEventArgs e)
		{
			OnKeyDown(e);
		}

		static List<Shape2D> AllShapes = new List<Shape2D>();
		static List<Sprite2D> AllSprites = new List<Sprite2D>();

		private void Closing(object sender, FormClosingEventArgs e)
		{
			GameLoopThread.Abort();
		}

		void GameLoop()
		{
			//try
			//{
				while (GameLoopThread.IsAlive)
				{
					OnDraw();
					Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
					Thread.Sleep(2);
					OnUpdate();
				}
			//}
			//catch 
			//{
			//	Log.Normal("Waiting for game Engine");
			//}
		}

		public static void RegisterShape(Shape2D shape)
		{
			AllShapes.Add(shape);
		}

		public static void UnRegisterShape(Shape2D shape)
		{
			AllShapes.Remove(shape);
		}

		public static void RegisterSprite(Sprite2D sprite)
		{
			AllSprites.Add(sprite);
		}

		public static void UnRegisterSprite(Sprite2D sprite)
		{
			AllSprites.Remove(sprite);
		}

		public abstract void OnLoad();
		public abstract void OnUpdate();
		public abstract void OnDraw();
		public abstract void OnKeyDown(KeyEventArgs e);
		public abstract void OnKeyUp(KeyEventArgs e);

		void Renderer(object sender, PaintEventArgs e)
		{
			var graphics = e.Graphics;
			graphics.Clear(BackgroundColor);
			graphics.TranslateTransform(CameraPosition.X, CameraPosition.Y);
			graphics.RotateTransform(CameraAngle);

			foreach (var shape in AllShapes)
			{
				graphics.DrawImage(shape.Sprite, shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
			}
			foreach (var shape in AllSprites)
			{
				graphics.DrawImage(shape.Sprite, shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
			}
		}
	}
}
