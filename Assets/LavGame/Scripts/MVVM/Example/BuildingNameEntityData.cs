using LavGame.Scripts.Game.State.Entities.Mergeable.Buildings;

namespace LavGame.Scripts.MVVM.Example
{
    // Model - создаётся, как и любая логика, при инициализации сцены. Например, в контейнере.
    public class BuildingNameEntityData : BuildingEntityData
    {
        public string BuildingName { get; set; }

    }
}