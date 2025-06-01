using LavGame.Scripts.Game.State.GameResources;

namespace LavGame.Scripts.Game.Gameplay.Commands
{
    /// <summary>
    /// Команда на добавление расходника
    /// </summary>
    public class CmdResourcesAdd : ICommand
    {
        public readonly ResourceType ResourceType;
        public readonly int Amount;

        public CmdResourcesAdd(ResourceType resourceType, int amount)
        {
            ResourceType = resourceType;
            Amount = amount;
        }
    }
}