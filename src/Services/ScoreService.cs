using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
	public static class ScoreService
	{
		static ScoreResult _score = new ScoreResult();
		public static ScoreResult Get() => _score;

		public static void CalculateScore(List<GameObject> gameObjects)
		{
			var total = MapProvider.TotalCoins;
			var current = total - gameObjects.Count(x => x.Type == GameObjectType.Coin);

			_score = new ScoreResult { Current = current, Total = total, IsEnd = total == current };
		}
	}

	public class ScoreResult
	{
		public int Current { get; set; }
		public int Total { get; set; }
		public bool IsEnd { get; set; }
	}
}