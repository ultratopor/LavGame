using System;
using System.Collections.Generic;
using LavGame.Scripts.Game.Settings.Gameplay.Entities;
using LavGame.Scripts.Game.Settings.Gameplay.Entities.Buildings;

namespace LavGame.Scripts.Game.Settings.Gameplay.Maps
{
	/// <summary>
	/// Список состояний зданий для загрузки после сохранения
	/// </summary>
	[Serializable]
	public class MapInitialStateSettings
	{
		public List<EntityInitialStateSettings> Entities;    // список состояний зданий.
	}
}
