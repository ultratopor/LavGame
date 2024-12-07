using System.Collections.Generic;
using UnityEngine;

namespace Settings.Gameplay.Buildings
{
	/// <summary>
	/// Ковырябельный в настройках список настроек построек
	/// </summary>
	[CreateAssetMenu(fileName = "BuildingsSettings", menuName = "Game Settings/Buildings/New Buildings Settings")]
	public class BuildingsSettings : ScriptableObject
	{
		// сохранённый список настроек.
		public List<BuildingSettings> AllBuildings;
	}
}
