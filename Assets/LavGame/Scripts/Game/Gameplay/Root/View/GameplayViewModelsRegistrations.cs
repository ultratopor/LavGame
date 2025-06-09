using Assets.LavGame.Scripts.Game.Gameplay.Services;
using Assets.LavGame.Scripts.Game.Gameplay.View.UI;
using BaCon;

namespace LavGame.Scripts.Game.Gameplay.Root.View
{
	public static class GameplayViewModelsRegistrations
	{       // этот контейнер содержит только view модели. Пр этом имеет доступ на сцену и в проект.
		public static void Register(DIContainer container)
		{   // созданы для примера. Зарегистрированы как синглтоны.
			container.RegisterFactory(c => new GameplayUIManager(container)).AsSingle();
			container.RegisterFactory(c => new UIGameplayRootViewModel()).AsSingle();
			container.RegisterFactory(c => new WorldGameplayRootViewModel
			(
				//c.Resolve<BuildingsService>(),
				c.Resolve<ResourcesService>())
			).AsSingle();
		}
	}
}
