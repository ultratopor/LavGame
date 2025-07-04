﻿using System.Linq;
using LavGame.Scripts.Game.State.Root;
using UnityEngine;

namespace LavGame.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdResourcesSpendHandler:ICommandHandler<CmdResourcesSpend>
    {
        private readonly GameStateProxy _gameState;

        public CmdResourcesSpendHandler(GameStateProxy gameState)
        {
            _gameState = gameState;
        }

        public bool Handle(CmdResourcesSpend command)
        {
            var requiredResourceType = command.ResourceType;
            var requiredResource = _gameState.Resources.FirstOrDefault((r) => r.ResourceType == requiredResourceType);
            if (requiredResource == null)
            {
                Debug.LogError("Trying to spend not existed resource");
                return false;
            }

            if (requiredResource.Amount.Value < command.Amount)
            {
                Debug.LogError($"Trying to spend more resources than existed ({requiredResourceType}). Exists: {requiredResource.Amount.Value}");
                return false;
            }
            
            requiredResource.Amount.Value -= command.Amount;
            return true;
        }
    }
}