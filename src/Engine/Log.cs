using System;

namespace GameEngine
{
	public class Log
	{
		public static void Normal(string msg)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine($"[MSG] - {msg}");
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void Info(string msg)
		{
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine($"[INFO] - {msg}");
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void Warning(string msg)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"[WARN] - {msg}");
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void Error(string msg)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"[ERROR] - {msg}");
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}
