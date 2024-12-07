using System;
using UnityEngine;

namespace Settings.Gameplay.Buildings
{
	/// <summary>
	/// состояние здания для загрузки после сохранения
	/// </summary>
	[Serializable]
	public class BuildingInitialStateSettings
	{
		public string TypeId;
		public int Level;
		public Vector3Int Position;
	}
}
