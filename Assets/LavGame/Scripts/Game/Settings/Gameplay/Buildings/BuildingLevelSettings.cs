using UnityEngine;

namespace Settings.Gameplay.Buildings
{
	/// <summary>
	/// Ковырябельные в настройках настройки уровня постройки
	/// </summary>
	[CreateAssetMenu(fileName = "BuildingLevelSettings", menuName = "Game Settings/Buildings/New Building Level Settings")]
	public class BuildingLevelSettings : ScriptableObject
	{
		// настройки уровня постройки.
		public int Level;   // уровень.
		public int BaseIncome;  // базовый доход.
	}
}
