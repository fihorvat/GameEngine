using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GameEngine
{
	public abstract class Engine
	{
		string Title;
		Vector2 ScreenSize;
		Thread GameLoopThread;
		public Canvas Window;
		public Color BackgroundColor = Color.Black;
		public Vector2 CameraPosition = Vector2.Zero();
		public float CameraAngle = 0f;
		public Stopwatch GameTime = new Stopwatch();

		public Engine(string title, Vector2 screenSize)
		{
			Title = title;
			ScreenSize = screenSize;

			Window = new Canvas
			{
				Text = Title,
				Size = new Size((int)ScreenSize.X, (int)ScreenSize.Y),
			};

			Window.FormClosing += Closing;
			Window.Paint += Renderer;
			Window.KeyDown += Window_KeyDown;
			Window.KeyUp += Window_KeyUp;
			Window.Resize += Window_Resize;

			GameLoopThread = new Thread(GameLoop);
			GameLoopThread.Start();
			GameTime.Start();

			Application.Run(Window);
		}

		protected void AdjustCamera()
		{
			var x = (Window.Width / 2) - (MapProvider.MapWidth / 2);
			var y = (Window.Height / 2) - (MapProvider.MapHeight / 2);
			CameraPosition = new Vector2(x, y);
		}

		void Window_Resize(object sender, EventArgs e)
		{
			AdjustCamera();
		}

		void Window_KeyUp(object sender, KeyEventArgs e)
		{
			OnKeyUp(e);
		}

		void Window_KeyDown(object sender, KeyEventArgs e)
		{
			OnKeyDown(e);
		}

		protected static List<GameObject> GameObjects = new List<GameObject>();

		private void Closing(object sender, FormClosingEventArgs e)
		{
			GameLoopThread.Abort();
		}

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

		void Renderer(object sender, PaintEventArgs e)
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
			var timeText = $"Time: {(GameTime.Elapsed.Minutes < 10 ? "0":"")}{GameTime.Elapsed.Minutes}:{(GameTime.Elapsed.Seconds < 10 ? "0" : "")}{GameTime.Elapsed.Seconds}";
			var scoreText = $"Score: {score.Current} / {score.Total}";
			var text = $"{scoreText} | {timeText}";
			var drawFormat = new StringFormat { Alignment = StringAlignment.Center };
			graphics.DrawString(text, new Font("Arial", 20, FontStyle.Bold), new SolidBrush(Color.White), MapProvider.MapWidth / 2, -40, drawFormat);

			foreach (var gameObject in GameObjects.ToArray())
				graphics.DrawImage(gameObject.Sprite, gameObject.Position.X, gameObject.Position.Y, gameObject.Scale.X, gameObject.Scale.Y);
		}

		public abstract void OnLoad();
		public abstract void OnUpdate();
		public abstract void OnDraw();
		public abstract void OnKeyDown(KeyEventArgs e);
		public abstract void OnKeyUp(KeyEventArgs e);
	}
}
