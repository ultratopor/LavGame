using BaCon;

namespace View
{
	public static class GameplayViewModelsRegistrations
	{		// этот контейнер содержит только view модели. Пр этом имеет доступ на сцену и в проект.
		public static void Register(DIContainer container)
		{   // созданы для примера. Зарегистрированы как синглтоны.
			container.RegisterFactory(c => new UIGameplayRootViewModel()).AsSingle();
			container.RegisterFactory(c => new WorldGameplayRootViewModel(c.Resolve<BuildingsService>())).AsSingle();
		}
	}
}
