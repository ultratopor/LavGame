using System.Collections.Generic;
using LavGame.Scripts.Game.Gameplay.Services;
using LavGame.Scripts.Game.Settings.Gameplay.Entities.Buildings;
using LavGame.Scripts.Game.State.Entities.Mergeable.Buildings;
using R3;
using UnityEngine;

namespace LavGame.Scripts.Game.Gameplay.View.Buildings
{
	/// <summary>
	/// создание View Model здания.
	/// </summary>
	public class BuildingViewModel
	{
		private readonly BuildingEntity _buildingEntity;
		private readonly BuildingSettings _buildingSettings;
		private readonly BuildingsService _buildingsService;
		// кэшируем уровень здания.
		private readonly Dictionary<int, BuildingLevelSettings> _levelSettingsMap=new();

		public readonly int BuildingEntityId;

		public ReadOnlyReactiveProperty<Vector2Int> Position { get; }       // реактивное свойство.
		public ReadOnlyReactiveProperty<int> Level {  get; }
		public readonly string ConfigId;


		public BuildingViewModel(BuildingEntity buildingEntity, BuildingSettings buildingSettings, BuildingsService buildingsService)
		{
			BuildingEntityId = buildingEntity.UniqueId;
			ConfigId = buildingEntity.ConfigId; // можно взять ID buildingSettings - они одинаковые.
			Level = buildingEntity.Level;

			_buildingEntity = buildingEntity;
			_buildingSettings = buildingSettings;
			_buildingsService = buildingsService;

			foreach(var buildingLevelSettings in buildingSettings.Levels)
			{		// пробегаемся по каждому уровню и кэшируем каждый.
				_levelSettingsMap[buildingLevelSettings.Level] = buildingLevelSettings;
			}

			Position = buildingEntity.Position;
		}

		public BuildingLevelSettings GetLevelSettings(int level)
		{		// глянуть уровень. Что будет в каджом уровне строения.
			return _levelSettingsMap[level];
		}
	}
}
