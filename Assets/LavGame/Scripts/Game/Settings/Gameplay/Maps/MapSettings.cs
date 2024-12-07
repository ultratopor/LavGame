using UnityEngine;

namespace Settings.Gameplay.Maps
{
	/// <summary>
	/// Ковырябельный в настройках список состояний зданий, обёрнутый в класс MapInitialStateSettings
	/// </summary>
	[CreateAssetMenu(fileName = "MapSettings", menuName = "Game Settings/Maps/New Map Settings")]
	public class MapSettings : ScriptableObject
	{
		public int MapId;
		public MapInitialStateSettings InitialStateSettings;
	}
}
