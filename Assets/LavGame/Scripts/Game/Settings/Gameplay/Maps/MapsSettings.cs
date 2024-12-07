using System.Collections.Generic;
using UnityEngine;

namespace Settings.Gameplay.Maps
{
	/// <summary>
	/// Ковырябельный в настройках список списков состояний зданий, обёрнутый в класс MapInitialStateSettings
	/// </summary>
	[CreateAssetMenu(fileName = "MapsSettings", menuName = "Game Settings/Maps/New Maps Settings")]
	public class MapsSettings : ScriptableObject
	{
		public List<MapSettings> Maps;
	}
}
