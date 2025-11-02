using System;
using LavGame.Scripts.Game.State.Entities;
using UnityEngine;

namespace LavGame.Scripts.Game.Settings.Gameplay.Entities
{
	/// <summary>
	/// состояние здания для загрузки после сохранения
	/// </summary>
	[Serializable]
	public class EntityInitialStateSettings
	{
		[field: SerializeField] public EntityType EntityType { get; private set; }
		[field: SerializeField] public string ConfigId { get; private set; }
		[field: SerializeField] public int Level { get; private set; }
		[field: SerializeField] public Vector2Int InitialPosition { get; private set; }
	}
}
