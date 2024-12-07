using BaCon;
using MainMenu.Root;
using MainMenu.Services;

namespace MainMenu
{
	public static class MainMenuRegistrations
	{
		public static void Register(DIContainer container, MainMenuEnterParams mainMenuEnterParams)
		{		// регистрируем как синглтоны.
			container.RegisterFactory(c => new SomeMainMenuService(c.Resolve<SomeCommonService>())).AsSingle();
		}
	}
}
