using System;
using LavGame.Scripts.Game.Settings.Gameplay.Entities;
using LavGame.Scripts.Game.State.Entities;
using LavGame.Scripts.Game.State.Entities.Mergeable.Buildings;
using UnityEngine;

namespace LavGame.Scripts.Game.Gameplay.Commands.Handlers
{
    public static class EntitiesDataFactory
    {
        public static EntityData CreateEntity(EntityInitialStateSettings initialSettings)
        {
            switch (initialSettings.EntityType)
            {
                
                case EntityType.Building:
                    var buildingEntityData = CreateEntity<BuildingEntityData>(initialSettings);
                    UpdateBuildingEntity(buildingEntityData);
                    return buildingEntityData;
                    break;
                
                default:
                    throw new Exception($"Not implement entity creation: {initialSettings.EntityType}");
            }
        }
        
        private static T CreateEntity<T>(EntityInitialStateSettings initialSettings) where T : EntityData, new()
        {
            return CreateEntity<T>(
                initialSettings.EntityType, 
                initialSettings.ConfigId, 
                initialSettings.Level, 
                initialSettings.InitialPosition);
        }

        private static T CreateEntity<T>(EntityType type, string configId, int level, Vector2Int position) 
            where T : EntityData, new()
        {
            var entity = new T
            {
                Type = type,
                ConfigId = configId,
                Level = level,
                Position = position
            };
            
            return entity;
        }

        private static void UpdateBuildingEntity(BuildingEntityData buildingEntityData)
        {
            buildingEntityData.LastClickedTimeMS = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            buildingEntityData.IsAutoCollectionEnabled = false;
        }
    }
}