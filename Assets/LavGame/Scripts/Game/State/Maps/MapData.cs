using System.Collections.Generic;
using LavGame.Scripts.Game.State.Entities;

namespace LavGame.Scripts.Game.State.Maps
{
	public class MapData
	{
		public int Id { get; set; } // ID карты.
		public List<EntityData> Entities { get; set; }	
	}
}
