using GameRoot;

namespace Gameplay.Root
{
	public class GameplayEnterParams : SceneEnterParams
	{
		public string SaveFileName { get; }	// файл сохранения
		public int LevelNumber { get; }		// номер уровня

		public GameplayEnterParams(string saveFileName, int levelNumber) : base(Scenes.GAMEPLAY)
		{
			SaveFileName = saveFileName;
			LevelNumber = levelNumber;
		}
	}
}
