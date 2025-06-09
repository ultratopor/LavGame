using System;
using LavGame.Scripts.Game.State.Entities.Mergeable.Buildings;
using LavGame.Scripts.Game.State.Entities.Mergeable.ResourcesEntities;

namespace LavGame.Scripts.Game.State.Entities
{
    public static class EntitiesFactory
    {
        public static Entity CreateEntity(EntityData entityData)
        {
            switch (entityData.Type)
            {
                case EntityType.Building:
                    return new BuildingEntity(entityData as BuildingEntityData);
                
                case EntityType.Resource:
                    return new ResourceEntity(entityData as ResourceEntityData);
                
                default:
                    throw new Exception("Unsupported entity type: " + entityData.Type);
            }
        }
    }
}