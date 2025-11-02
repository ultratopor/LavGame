using System;
using System.Collections.Generic;
using LavGame.Scripts.Game.Gameplay.Commands;
using LavGame.Scripts.Game.Gameplay.View.Buildings;
using LavGame.Scripts.Game.Settings.Gameplay.Entities;
using LavGame.Scripts.Game.Settings.Gameplay.Entities.Buildings;
using LavGame.Scripts.Game.State.Entities;
using LavGame.Scripts.Game.State.Entities.Mergeable.Buildings;
using ObservableCollections;
using R3;
using UnityEngine;

// сервисы являются прослойкой между View моделью и обработчиком команд. Этот посылает сигналы в обработчик команд на
// размещение здания, перемещение и удаление.
namespace LavGame.Scripts.Game.Gameplay.Services
{
	public class BuildingsService
	{
		private readonly ICommandProcessor _cmd;

		// Словарь зданий Building Entity ID.
		private readonly Dictionary<int, BuildingViewModel> _buildingsMap = new();

		// Список наблюдаемый зданий и добавляем его в компонент который, нужен для обновления нашей коллекции 
		// с помощью метод Add/Remove. Возвращает только метод, когда класс будь инициализирован на view.
		private readonly ObservableList<BuildingViewModel> _allBuildings = new();

		// Словарь настраиваемый списка настроек зданий по TypeId
		private readonly Dictionary<string, BuildingSettings> _buildingSettingsMap = new();
		public IObservableCollection<BuildingViewModel> AllBuildings => _allBuildings;

		/* buildings - коллекцией исходного состояния, cmd - сервис через который создаем команды для изменения состояния.
	* Объект не вызова напрямую. Это через вызов метод команд. 
	* Начальная инициализия нового состояния процесса. На основании этих состояний создаем View Model, устанавливаем 
	* подписываемся изменения процесса.*/
		public BuildingsService(IObservableCollection<Entity> entities, EntitiesSettings entitiesSettings, ICommandProcessor cmd)
		{
			this._cmd = cmd;

			foreach(var buildingSettings in entitiesSettings.Buildings)
			{       // Кэшируем настройки здания.
				_buildingSettingsMap[buildingSettings.ConfigId] = buildingSettings;
			}

			// Создание View Model на основе исходного состояния.
			foreach(var entity in entities)
			{
				if (entity is BuildingEntity buildingEntity)
				{
					CreateBuildingViewModel(buildingEntity);
				}
			}

			// Создание View Model при добавлении процесса.
			entities.ObserveAdd().Subscribe(e =>
			{
				var entity = e.Value;
				if (entity is BuildingEntity buildingEntity)
				{
					CreateBuildingViewModel(buildingEntity);
				}
			});

			// Удаление View Model при удалении процесса.
			entities.ObserveRemove().Subscribe(e =>
			{
				var entity = e.Value;
				if (entity is BuildingEntity buildingEntity)
				{
					RemoveBuildingViewModel(buildingEntity);
				}
			});
		}

		public bool PlaceBuilding(string buildingTypeId, Vector3Int position)
		{       // Размещение здания. bool - нужен View Models могут плохо обработать возвращения.
			// buildingTypeId - тип здания.
			var command = new CmdPlaceBuilding(buildingTypeId, position);
			var result = _cmd.Process(command);

			return result;
		}

		public bool MoveBuilding(int buildingEntityId, Vector3Int newPosition)
		{       // Перемещение здания. buildingTypeId - идентификатор здания.
			throw new NotImplementedException();
		}

		public bool DeleteBuilding(int buildingEntityId)
		{       // Удаляет здание.
			throw new NotImplementedException();
		}

		private void CreateBuildingViewModel(BuildingEntity buildingEntity)
		{       // Берётся прокс состояние сущностью (Proxy) и создаём модель и добавляем его в наблюдаемый список.
			// Получаем настройки по типам.
			var buildingSettings = _buildingSettingsMap[buildingEntity.ConfigId];
			var buildingViewModel = new BuildingViewModel(buildingEntity, buildingSettings, this);

			_allBuildings.Add(buildingViewModel);
			_buildingsMap[buildingEntity.UniqueId] = buildingViewModel;   // Кэширование текущей View Model по ключу - ID.
		}

		private void RemoveBuildingViewModel(BuildingEntity buildingEntity)
		{
			if(_buildingsMap.TryGetValue(buildingEntity.UniqueId, out var buildingViewModel))
			{
				_allBuildings.Remove(buildingViewModel);
				_buildingsMap.Remove(buildingEntity.UniqueId);
			}
		}
	}
}
