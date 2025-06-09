using UnityEngine;

namespace LavGame.Scripts.Game.Gameplay.Commands
{
	/// <summary>
	/// команда поместить здание определённого типа и пoзиции
	/// </summary>
	public class CmdPlaceBuilding : ICommand
	{
		public readonly string BuildingTypeId;
		public readonly Vector3Int Position;

		public CmdPlaceBuilding(string buildingTypeId, Vector3Int position)
		{
			BuildingTypeId = buildingTypeId;
			Position = position;
		}
	}
}
