using BaCon;
using View;

public static class MainMenuViewModelsRegistrations
{
	public static void Register(DIContainer container)
	{		// регистрируем как синглтон.
		container.RegisterFactory(c => new UIMainMenuRootViewModel()).AsSingle();
	}
}