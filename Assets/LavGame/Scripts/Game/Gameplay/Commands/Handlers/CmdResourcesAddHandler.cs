﻿using System.Linq;
using LavGame.Scripts.Game.State.GameResources;

namespace LavGame.Scripts.Game.Gameplay.Commands.Handlers
{
    /// <summary>
    /// Обработчик команды добавления расходника.
    /// </summary>
    public class CmdResourcesAddHandler : ICommandHandler<CmdResourcesAdd>
    {
        private readonly GameStateProxy _gameState;

        public CmdResourcesAddHandler(GameStateProxy gameState)
        {
            _gameState = gameState;
        }

        public bool Handle(CmdResourcesAdd command)
        {
            var requiredResourceType = command.ResourceType;
            var requiredResource = _gameState.Resources.FirstOrDefault(r => r.ResourceType == requiredResourceType);
			if(requiredResource == null)
            {
                requiredResource = CreateNewResource(requiredResourceType);
            }

            requiredResource.Amount.Value += command.Amount;

            return true;
        }

        private Resource CreateNewResource(ResourceType resourceType)
        {
            var newResourceData = new ResourceData
            {
                ResourceType = resourceType,
                Amount = 0
            };

            var newResource = new Resource(newResourceData);
            _gameState.Resources.Add(newResource);

            return newResource;
        }
    }
}