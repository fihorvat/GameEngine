using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GameEngine
{
	public abstract class Engine
	{
		readonly Thread GameLoopThread;
		public Canvas Window;
		public Color BackgroundColor = Color.Black;
		public Vector2 CameraPosition = Vector2.Zero();
		public float CameraAngle = 0f;
		public Stopwatch GameTime = new Stopwatch();

		protected Engine(string title, Vector2 screenSize)
		{
			Window = new Canvas
			{
				Text = title,
				Size = new Size((int)screenSize.X, (int)screenSize.Y),
			};

			Window.FormClosing += Window_Closing;
			Window.Paint += Window_Renderer;
			Window.KeyDown += Window_KeyDown;
			Window.KeyUp += Window_KeyUp;
			Window.Resize += Window_Resize;

			GameLoopThread = new Thread(GameLoop);
			GameLoopThread.Start();
			GameTime.Start();

			Application.Run(Window);
		}

		public abstract void OnLoad();
		public abstract void OnUpdate();
		public abstract void OnDraw();
		public abstract void OnKeyDown(KeyEventArgs e);
		public abstract void OnKeyUp(KeyEventArgs e);

		protected static List<GameObject> GameObjects = new List<GameObject>();
		protected static IEnumerable<GameObject> GameObjectsInReach = new List<GameObject>();
		protected void AdjustCamera()
		{
			var x = (Window.Width / 2) - (MapProvider.MapWidth / 2);
			var y = (Window.Height / 2) - (MapProvider.MapHeight / 2);
			CameraPosition = new Vector2(x, y);
		}

		void Window_Resize(object sender, EventArgs e) => AdjustCamera();
		void Window_KeyUp(object sender, KeyEventArgs e) => OnKeyUp(e);
		void Window_KeyDown(object sender, KeyEventArgs e) => OnKeyDown(e);
		void Window_Closing(object sender, FormClosingEventArgs e) => GameLoopThread.Abort();

		void GameLoop()
		{
			OnLoad();
			while (GameLoopThread.IsAlive)
			{
				OnDraw();
				Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
				Thread.Sleep(1000 / 60); //60 fps
				OnUpdate();
			}
		}

		void Window_Renderer(object sender, PaintEventArgs e)
		{
			var graphics = e.Graphics;
			graphics.Clear(BackgroundColor);
			graphics.RotateTransform(CameraAngle);
			graphics.TranslateTransform(CameraPosition.X, CameraPosition.Y);

			//border
			var borderWidth = 3;
			graphics.DrawRectangle(new Pen(Color.DarkRed, borderWidth), -borderWidth, -borderWidth, MapProvider.MapWidth + borderWidth, MapProvider.MapHeight + borderWidth);

			//score
			var score = ScoreService.Get(GameObjects);
			var timeText = $"Time: {(GameTime.Elapsed.Minutes < 10 ? "0" : "")}{GameTime.Elapsed.Minutes}:{(GameTime.Elapsed.Seconds < 10 ? "0" : "")}{GameTime.Elapsed.Seconds}";
			var scoreText = $"Score: {score.Current} / {score.Total}";
			var text = $"{scoreText} | {timeText}";
			var drawFormat = new StringFormat { Alignment = StringAlignment.Center };
			graphics.DrawString(text, new Font("Arial", 20, FontStyle.Bold), new SolidBrush(Color.White), MapProvider.MapWidth / 2, -40, drawFormat);


			//----------------
			//If we want to show everything uncomment
			foreach (var gameObject in GameObjects.ToArray())
				graphics.DrawImage(gameObject.Sprite, gameObject.Position.X, gameObject.Position.Y, gameObject.Scale.X, gameObject.Scale.Y);
			//----------------

			//----------------
			//This will show only whats is in reach
			//var player = GameObjects.Find(x => x.Type == GameObjectType.Player);
			//if (player != null)
			//{
			//	var radius = 70;
			//	var x = (player.Position.X + (player.Scale.X / 2)) - radius / 2;
			//	var y = (player.Position.Y + (player.Scale.Y / 2)) - radius / 2;


			//	graphics.DrawEllipse(new Pen(Brushes.Yellow, radius), x, y, radius, radius);
			//	graphics.DrawImage(player.Sprite, player.Position.X, player.Position.Y, player.Scale.X, player.Scale.Y);
			//}

			//foreach (var gameObject in GameObjectsInReach.ToArray())
			//{
			//	graphics.DrawImage(gameObject.Sprite, gameObject.Position.X, gameObject.Position.Y, gameObject.Scale.X, gameObject.Scale.Y);
			//	//Uncomment for displaying borders around elements in reach
			//	//graphics.DrawRectangle(new Pen(Brushes.DarkRed), gameObject.Position.X, gameObject.Position.Y, gameObject.Scale.X, gameObject.Scale.Y);
			//}
			//----------------
		}
	}
}