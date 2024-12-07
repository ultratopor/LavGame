using System.Linq;

using UnityEngine;

namespace Commands
{
	/// <summary>
	/// обработчик определённой (CmdPlaceBuilding) команды
	/// </summary>
	public class CmdPlaceBuildingHandler : ICommandHandler<CmdPlaceBuilding>
	{
		private readonly GameStateProxy _gameState;

		public CmdPlaceBuildingHandler(GameStateProxy gameState)
		{		// конструктор с кэшированием замещающего состояния игры.
			_gameState = gameState;
		}

		public bool Handle(CmdPlaceBuilding command)
		{
			// ищем и кэшируем карту по идентификатору в замещающем сотоянии.
			var currentMap = _gameState.Maps.FirstOrDefault(m => m.Id ==_gameState.CurrentMapId.CurrentValue);
			if(currentMap == null)
			{		// если не нашли.
				Debug.LogError($"Couldn't find MapState for ID: {_gameState.CurrentMapId.CurrentValue}");
				return false;
			}

			var entityId = _gameState.CreateEntityId();        // создание идентификатора оригинального состояния.
			var newBuildingEntity = new BuildingEntity
			{		// создание оригинального состояния.
				Id = entityId,
				Position = command.Position,
				TypeId = command.BuildingTypeId
			};
				// создание замещающего состояния и запихивание туда оригинала.
			var newBuildingEntityProxy = new BuildingEntityProxy(newBuildingEntity);

			currentMap.Buildings.Add(newBuildingEntityProxy);

			// в будущем можно будет запихнуть проверку на возможности строительства, а пока что возвращается всегда истина.
			return true;
		}
	}
}
