using Settings.Gameplay.Buildings;
using Settings.Gameplay.Maps;
using UnityEngine;

namespace Settings
{
	/// <summary>
	/// Настройки геймплея и карт
	/// </summary>
	[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings/New Game Settings")]
	public class GameSettings : ScriptableObject
	{
			// настройки геймплея.
		public BuildingsSettings BuildingsSettings;
		public MapsSettings MapsSettings;				// настройки карт.
	}
}
