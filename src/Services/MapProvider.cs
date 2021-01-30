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
		public static int MapWidth = Get.GetLength(1) * TileWidth;
		public static int MapHeight = Get.GetLength(0) * TileHeight;
		static int? _coinsCount;
		public static int GetCoinsCount()
		{
			if(_coinsCount == null)
			{
				_coinsCount = 0;

				for (int i = 0; i < Get.GetLength(0); i++)
					for (int j = 0; j < Get.GetLength(1); j++)
						if (Get[i, j] == 'c') _coinsCount++;
			}

			return _coinsCount.Value;
		}

		public static char[,] Get => new char[,]{
			{'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g'},
			{'g', 'c', ' ', ' ', ' ', ' ', ' ', ' ', 'g', 'c', ' ', ' ', ' ', ' ', ' ', 'c', ' ', ' ', ' ', ' ', 'c', 'g'},
			{'g', ' ', 'g', 'g', 'g', 'g', 'g', ' ', 'g', 'g', 'g', 'g', ' ', ' ', 'g', 'g', 'g', ' ', ' ', 'g', ' ', 'g'},
			{'g', ' ', ' ', ' ', 'g', 'c', ' ', ' ', 'g', ' ', 'c', ' ', ' ', ' ', ' ', 'g', ' ', ' ', 'g', 'g', 'g', 'g'},
			{'g', ' ', 'g', ' ', ' ', ' ', 'g', 'g', 'g', ' ', 'g', 'g', 'g', 'g', ' ', 'g', 'c', ' ', ' ', ' ', ' ', 'g'},
			{'g', ' ', 'g', ' ', 'g', ' ', ' ', ' ', ' ', ' ', 'g', ' ', ' ', 'g', 'g', 'g', 'g', 'g', 'g', 'g', ' ', 'g'},
			{'g', 'g', 'g', ' ', 'g', ' ', 'g', ' ', 'g', 'c', ' ', ' ', ' ', 'g', ' ', ' ', 'g', 'g', 'c', ' ', ' ', 'g'},
			{'g', ' ', ' ', ' ', 'g', ' ', 'g', ' ', 'g', 'g', 'g', ' ', ' ', 'g', 'c', ' ', ' ', ' ', 'g', 'g', ' ', 'g'},
			{'g', 'c', 'g', 'g', 'g', ' ', 'g', ' ', ' ', ' ', 'g', 'g', ' ', 'g', 'g', 'g', ' ', ' ', ' ', ' ', ' ', 'g'},
			{'g', ' ', 'g', ' ', ' ', ' ', ' ', ' ', 'g', ' ', ' ', 'g', ' ', ' ', 'g', 'g', 'g', 'g', ' ', 'g', 'g', 'g'},
			{'g', ' ', 'g', ' ', 'g', 'g', 'g', ' ', 'g', 'g', ' ', 'g', 'g', 'c', ' ', ' ', 'g', 'g', ' ', ' ', 'c', 'g'},
			{'g', ' ', 'g', ' ', 'g', ' ', 'g', ' ', ' ', ' ', ' ', ' ', 'g', 'g', 'g', ' ', ' ', 'g', 'g', 'g', ' ', 'g'},
			{'g', ' ', ' ', ' ', 'g', 'p', 'g', 'g', ' ', ' ', 'g', ' ', ' ', 'g', 'g', 'g', ' ', 'g', ' ', ' ', ' ', 'g'},
			{'g', ' ', 'g', ' ', 'g', ' ', 'g', 'g', 'g', ' ', 'g', 'g', ' ', ' ', ' ', 'g', ' ', 'g', 'c', 'g', 'g', 'g'},
			{'g', ' ', 'g', 'g', 'g', ' ', 'g', ' ', ' ', ' ', 'g', 'g', 'g', 'g', ' ', 'g', ' ', 'g', ' ', ' ', ' ', 'g'},
			{'g', ' ', ' ', ' ', ' ', ' ', 'g', 'c', 'g', 'g', 'g', 'c', ' ', ' ', ' ', ' ', 'c', 'g', 'g', 'g', 'c', 'g'},
			{'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g'},
			};
	}
}
