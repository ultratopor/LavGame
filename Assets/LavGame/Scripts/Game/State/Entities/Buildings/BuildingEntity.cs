using System;
using UnityEngine;

[Serializable]
public class BuildingEntity : Entity
{
	public string TypeId;   // для синхронизации с настройками.
	public Vector3Int Position;
	public int Level;
}
