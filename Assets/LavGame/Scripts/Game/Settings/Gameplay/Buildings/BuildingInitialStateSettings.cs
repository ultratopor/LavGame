using System;
using UnityEngine;

namespace LavGame.Scripts.Game.Settings.Gameplay.Buildings
{
	/// <summary>
	/// состояние здания для загрузки после сохранения
	/// </summary>
	[Serializable]
	public class BuildingInitialStateSettings
	{
		public string TypeId;
		public int Level;
		public Vector2Int Position;
	}
}
