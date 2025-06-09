using LavGame.Scripts.Game.State.Entities.Mergeable.Buildings;
using R3;

namespace LavGame.Scripts.MVVM.Example
{
    // ViewModel
    public class BuildingNameEntity : BuildingEntity
    {
        public readonly ReactiveProperty<string> BuildingName;
        public BuildingNameEntity(BuildingNameEntityData data) : base(data)
        {
            BuildingName = new ReactiveProperty<string>(data.BuildingName);
            BuildingName.Subscribe(newValue => data.BuildingName = newValue);
        }
    }

}