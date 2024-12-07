using System.Linq;
using BaCon;
using Commands;

using Game.Gameplay.Commands;

using Gameplay.Root;
using Settings;

namespace Gameplay
{
	/// <summary>
	/// Регистрация сервисов
	/// </summary>
	public static class GameplayRegistrations
	{
		public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
		{   // пишем делегаты. Как синглтоны. Добавляем состояние.
			var gameStateProvider = container.Resolve<IGameStateProvider>();
			var gameState = gameStateProvider.GameState;        // кэширование ссылки на состояние.
			var settingsProvider = container.Resolve<ISettingsProvider>();      // кэширование настроек из интерфейса.
			var gameSettings = settingsProvider.GameSettings;       // кэширование настроек.

			// регистрация нового сервиса.
			var cmd = new CommandProcessor(gameStateProvider);
			cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameState));
			cmd.RegisterHandler(new CmdCreateMapStateHandler(gameState, gameSettings));
			container.RegisterInstance<ICommandProcessor>(cmd);     // заворачивание в контейнер.

			/* на данный момент мы знаем, что пытаемся загрузить карту. Но не знаем, есть ли её состояние вообще.
			 * Создание карты - это модель, так что работать с ней нужно через команды, поэтому нужен обработчик команд
			 * на случай, если состояния карты ещё нет. Может мы этот момент переделаем потом, чтобы состояние карты 
			 * создавалось до загрузки сцены и тут не было подобных проверок, но пока так. Делаем пошагово.*/

			var loadingMapId = gameplayEnterParams.MapId;	// идентификатор загружаемой карты из входных параметров.
			var loadingMap = gameState.Maps.FirstOrDefault(m => m.Id == loadingMapId);	// карта из списка состояния.
			if(loadingMap == null)
			{
				// создание состояния, если его ещё нет, через команду.
				var command = new CmdCreateMapState(loadingMapId);
				var success = cmd.Process(command);
                if (!success)
                {
                    throw new System.Exception($"Couldn't create map state with ID: {loadingMapId}");
                }

				loadingMap = gameState.Maps.First(m => m.Id == loadingMapId);
            }

			// создание фабрики, при запросе которой создаётся новый сервис - одиночка.
			container.RegisterFactory(_ => new BuildingsService(loadingMap.Buildings, gameSettings.BuildingsSettings, cmd)).AsSingle();
		}
	}
}
