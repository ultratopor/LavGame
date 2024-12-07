using System;
using System.Collections.Generic;

namespace State.Maps
{
	[Serializable]
	public class MapState
	{
		public int Id;							// ID карты.
		public List<BuildingEntity> Buildings;	// список строений из GameState.
	}
}
