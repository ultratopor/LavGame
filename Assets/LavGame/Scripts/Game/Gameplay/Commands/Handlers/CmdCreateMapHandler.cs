using System.Collections.Generic;
using System.Linq;
using LavGame.Scripts.Game.State.Entities;
using LavGame.Scripts.Game.State.Maps;
using LavGame.Scripts.Game.State.Root;
using Settings;
using UnityEngine;

namespace LavGame.Scripts.Game.Gameplay.Commands.Handlers
{
	/// <summary>
	/// Обработчик команды CmdCreateMap
	/// </summary>
	internal class CmdCreateMapHandler : ICommandHandler<CmdCreateMap>
	{
		private readonly GameStateProxy _gameState;	// состояние игры
		private readonly GameSettings _gameSettings;// настройки игры

        public CmdCreateMapHandler(GameStateProxy gameState, GameSettings gameSettings)
        {
            _gameState = gameState;
			_gameSettings = gameSettings;
        }

        public bool Handle(CmdCreateMap command)
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

			var initialEntities = new List<EntityData>();
			foreach(var entitySettings in newMapInitialStateSettings.Entities)
			{		// создали и добавили в список состояния построек по умолчанию.
				var initialEntity = EntitiesDataFactory.CreateEntity(entitySettings);
				initialEntity.UniqueId = _gameState.CreateEntityId();
				initialEntities.Add(initialEntity);
			}

			var newMapState = new MapData
			{		// создание состояния карты с указанным индентификатором и присвоенными выше состояниями строений.
				Id = command.MapId,
				Entities = initialEntities,
			};

			var newMapStateProxy = new Map(newMapState);

			_gameState.Maps.Add(newMapStateProxy);

			return true;
		}
	}
}
