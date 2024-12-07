using System;
using System.Collections.Generic;
using Settings.Gameplay.Buildings;

namespace Settings.Gameplay.Maps
{
	/// <summary>
	/// Список состояний зданий для загрузки после сохранения
	/// </summary>
	[Serializable]
	public class MapInitialStateSettings
	{
		public List<BuildingInitialStateSettings> Buildings;    // список состояний зданий.
	}
}
