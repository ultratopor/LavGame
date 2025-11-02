using System.Collections.Generic;
using LavGame.Scripts.Game.Settings.Gameplay.Entities.Buildings;
using UnityEngine;

namespace LavGame.Scripts.Game.Settings.Gameplay.Entities
{
	/// <summary>
	/// Ковырябельный в настройках список настроек построек
	/// </summary>
	[CreateAssetMenu(fileName = "EntitiesSettings", menuName = "Game Settings/Entities/New Entities Settings")]
	public class EntitiesSettings : ScriptableObject
	{
		// сохранённый список настроек.
		[field: SerializeField] public List<BuildingSettings> Buildings { get; private set; }
	}
}
