using System.Collections.Generic;
using System.Linq;
using Settings;
using State.Maps;
using UnityEngine;

namespace Game.Gameplay.Commands
{
	/// <summary>
	/// Обработчик команды CmdCreateMapState
	/// </summary>
	internal class CmdCreateMapStateHandler : ICommandHandler<CmdCreateMapState>
	{
		private readonly GameStateProxy _gameState;	// состояние игры
		private readonly GameSettings _gameSettings;// настройки игры

        public CmdCreateMapStateHandler(GameStateProxy gameState, GameSettings gameSettings)
        {
            _gameState = gameState;
			_gameSettings = gameSettings;
        }

        public bool Handle(CmdCreateMapState command)
		{
			// если состояние есть
			var isMapAlreadyExisted  = _gameState.Maps.Any(m => m.Id == command.MapId);
			// то выдаём ошибку
			if(isMapAlreadyExisted)
			{
				Debug.LogError($"Map with ID = {command.MapId} already exist");
				return false;
			}

			var newMapSettings = _gameSettings.MapsSettings.Maps.First(m => m.MapId == command.MapId);
			var newMapInitialStateSettings = newMapSettings.InitialStateSettings;

			var initialBuildings = new List<BuildingEntity>();
			foreach(var buildingSettings in newMapInitialStateSettings.Buildings)
			{		// создали и добавили в список состояния построек по умолчанию.
				var initialBuilding = new BuildingEntity
				{
					Id = _gameState.CreateEntityId(),
					TypeId = buildingSettings.TypeId,
					Position = buildingSettings.Position,
					Level = buildingSettings.Level
				};

				initialBuildings.Add(initialBuilding);
			}

			var newMapState = new MapState
			{		// создание состояния карты с указанным индентификатором и присвоенными выше состояниями строений.
				Id = command.MapId,
				Buildings = initialBuildings
			};

			var newMapStateProxy = new Map(newMapState);

			_gameState.Maps.Add(newMapStateProxy);

			return true;
		}
	}
}
