using LavGame.Scripts.Game.Settings.Gameplay.Entities;
using LavGame.Scripts.Game.Settings.Gameplay.Entities.Buildings;
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
		public EntitiesSettings entitiesSettings;
		public MapsSettings MapsSettings;				// настройки карт.
	}
}
