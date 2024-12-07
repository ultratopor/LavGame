using GameRoot;

namespace Gameplay.Root
{
	public class GameplayEnterParams : SceneEnterParams
	{
		public int MapId { get; }		// номер уровня

		public GameplayEnterParams(int mapId) : base(Scenes.GAMEPLAY)
		{
			MapId = mapId;
		}
	}
}
