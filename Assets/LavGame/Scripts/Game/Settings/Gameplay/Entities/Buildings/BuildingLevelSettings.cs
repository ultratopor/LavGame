using UnityEngine;

namespace LavGame.Scripts.Game.Settings.Gameplay.Entities.Buildings
{
	/// <summary>
	/// Ковырябельные в настройках настройки уровня постройки
	/// </summary>
	[CreateAssetMenu(fileName = "BuildingLevelSettings", menuName = "Game Settings/Entities/Buildings/New Building Level Settings")]
	public class BuildingLevelSettings : EntityLevelSettings
	{
		[field: SerializeField] public double BaseIncome { get; private set; }  // базовый доход.
	}
}
