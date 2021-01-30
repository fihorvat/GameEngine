using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
	public static class ScoreService
	{
		public static ScoreResult Get(List<GameObject> gameObjects)
		{
			var total = MapProvider.GetCoinsCount();
			var current = gameObjects.Where(x => x.Type == GameObjectType.Coin).Count();

			return new ScoreResult { Current = total - current, Total = total };
		}
	}

	public class ScoreResult
	{
		public int Current { get; set; }
		public int Total { get; set; }
	}
}