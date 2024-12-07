using System.Collections.Generic;
using UnityEngine;

namespace Settings.Gameplay.Buildings
{
	/// <summary>
	/// Ковырябельные в настройках настройки постройки
	/// </summary>
	[CreateAssetMenu(fileName = "BuildingSettings", menuName = "Game Settings/Buildings/New Building Settings")]
	public class BuildingSettings : ScriptableObject
	{
		// настройки постройки
		public string TypeId;
		public string TitleLID;
		public string DescriptionLID;
		public List<BuildingLevelSettings> LevelSettings;
	}
}
