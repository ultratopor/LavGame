﻿using UnityEngine;

namespace LavGame.Scripts.Game.State.Entities
{
	public class EntityData
	{
		public int UniqueId { get; set; }			// Уникальный идентификатор сущности.
		public string ConfigId { get; set; }		// Идентификатор для поиска настроек сущности.
		public EntityType Type { get; set; }		// Тип сущности для быстрого понимания, что это за сущность.
		public Vector2Int Position { get; set; }	// Позиция в координатах х,у, которые конвертируются в х, z на плоскости.
	}
}
