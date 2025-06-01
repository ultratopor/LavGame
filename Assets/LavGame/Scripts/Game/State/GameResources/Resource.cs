using R3;

namespace LavGame.Scripts.Game.State.GameResources
{
    /// <summary>
    /// Заместитель ResourceData
    /// </summary>
    public class Resource
    {
        public readonly ResourceData Origin;
        public readonly ReactiveProperty<int> Amount; // Количество ресурсов, на которое будем подписываться.

        public ResourceType ResourceType => Origin.ResourceType;

        public Resource(ResourceData data)
        {
            Origin = data;
            Amount = new ReactiveProperty<int>(data.Amount);

            Amount.Subscribe(newValue => data.Amount = newValue);
        }
    }
}