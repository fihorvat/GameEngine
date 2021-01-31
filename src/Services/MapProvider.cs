using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
	public static class MapProvider
	{
		public static readonly int PlayerWidth = 25;
		public static readonly int PlayerHeight = 30;
		public static readonly int CoinWidth = 30;
		public static readonly int CoinHeight = 30;
		public static readonly int TileWidth = 50;
		public static readonly int TileHeight = 50;
		public static char[,] Map;
		public static int MapWidth;
		public static int MapHeight;
		public static int TotalCoins;

		public static void InitializeMap(int coinsCount)
		{
			Map = (char[,])_initialMap.Clone();
			MapWidth = Map.GetLength(1) * TileWidth;
			MapHeight = Map.GetLength(0) * TileHeight;
			TotalCoins = coinsCount;
			RandomizeCoins(coinsCount);
			RandomizePlayer();
		}

		static (int x, int y)[] GetPositions(char c)
		{
			var list = new List<(int x, int y)>();
			for (int i = 0; i < Map.GetLength(0); i++)
				for (int j = 0; j < Map.GetLength(1); j++)
					if (Map[i, j] == c) list.Add((i, j));

			return list.ToArray();
		}

		static void RandomizeCoins(int count)
		{
			var freePositions = GetPositions(' ').ToList();
			if (count > freePositions.Count)
				throw new InvalidOperationException("Too many coins for this map");
			var random = new Random();

			while (count > 0)
			{
				var index = random.Next(freePositions.Count);
				Map[freePositions[index].x, freePositions[index].y] = 'c';
				freePositions.RemoveAt(index);
				count--;
			}
		}

		static void RandomizePlayer()
		{
			var freePositions = GetPositions(' ').ToList();
			var random = new Random();
			var index = random.Next(freePositions.Count);
			Map[freePositions[index].x, freePositions[index].y] = 'p';
		}

		static readonly char[,] _initialMap = new char[,]{
			{'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g'},
			{'g', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'g', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'g'},
			{'g', ' ', 'g', 'g', 'g', 'g', 'g', ' ', 'g', 'g', 'g', 'g', ' ', ' ', 'g', 'g', 'g', ' ', ' ', 'g', ' ', 'g'},
			{'g', ' ', ' ', ' ', 'g', ' ', ' ', ' ', 'g', ' ', ' ', ' ', ' ', ' ', ' ', 'g', ' ', ' ', 'g', 'g', 'g', 'g'},
			{'g', ' ', 'g', ' ', ' ', ' ', 'g', 'g', 'g', ' ', 'g', 'g', 'g', 'g', ' ', 'g', ' ', ' ', ' ', ' ', ' ', 'g'},
			{'g', ' ', 'g', ' ', 'g', ' ', ' ', ' ', ' ', ' ', 'g', ' ', ' ', 'g', 'g', 'g', 'g', 'g', 'g', 'g', ' ', 'g'},
			{'g', 'g', 'g', ' ', 'g', ' ', 'g', ' ', 'g', ' ', ' ', ' ', ' ', 'g', ' ', ' ', 'g', 'g', ' ', ' ', ' ', 'g'},
			{'g', ' ', ' ', ' ', 'g', ' ', 'g', ' ', 'g', 'g', 'g', ' ', ' ', 'g', ' ', ' ', ' ', ' ', 'g', 'g', ' ', 'g'},
			{'g', ' ', 'g', 'g', 'g', ' ', 'g', ' ', ' ', ' ', 'g', 'g', ' ', 'g', 'g', 'g', ' ', ' ', ' ', ' ', ' ', 'g'},
			{'g', ' ', 'g', ' ', ' ', ' ', ' ', ' ', 'g', ' ', ' ', 'g', ' ', ' ', 'g', 'g', 'g', 'g', ' ', 'g', 'g', 'g'},
			{'g', ' ', 'g', ' ', 'g', 'g', 'g', ' ', 'g', 'g', ' ', 'g', 'g', ' ', ' ', ' ', 'g', 'g', ' ', ' ', ' ', 'g'},
			{'g', ' ', 'g', ' ', 'g', ' ', 'g', ' ', ' ', ' ', ' ', ' ', 'g', 'g', 'g', ' ', ' ', 'g', 'g', 'g', ' ', 'g'},
			{'g', ' ', ' ', ' ', 'g', ' ', 'g', 'g', ' ', ' ', 'g', ' ', ' ', 'g', 'g', 'g', ' ', 'g', ' ', ' ', ' ', 'g'},
			{'g', ' ', 'g', ' ', 'g', ' ', 'g', 'g', 'g', ' ', 'g', 'g', ' ', ' ', ' ', 'g', ' ', 'g', ' ', 'g', 'g', 'g'},
			{'g', ' ', 'g', 'g', 'g', ' ', 'g', ' ', ' ', ' ', 'g', 'g', 'g', 'g', ' ', 'g', ' ', 'g', ' ', ' ', ' ', 'g'},
			{'g', ' ', ' ', ' ', ' ', ' ', 'g', ' ', 'g', 'g', 'g', ' ', ' ', ' ', ' ', ' ', ' ', 'g', 'g', 'g', ' ', 'g'},
			{'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g'},
		};
	}
}
