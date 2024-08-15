namespace GameRoot
{
	public abstract class SceneEnterParams
	{
		// используются константы из класса Scenes
		public string SceneName { get; }

		public SceneEnterParams(string sceneName)
		{
			SceneName = sceneName;
		}
		// реализация наследования, чтоб название сцены было везде, а у наследников были свои параметры.
		public T As<T>() where T : SceneEnterParams /* чтобы кастовать SceneName к наследнику класса SceneEnterParams
		                                             * то есть кастуем класс в дочерний класс */
		{
			return (T)this;
		}
	}
}
