namespace Game.Gameplay.Commands
{
    /// <summary>
    /// Команда создания состояния карты
    /// </summary>
	public class CmdCreateMapState : ICommand
	{
		public readonly int MapId;

        public CmdCreateMapState(int mapId)
        {
            MapId = mapId;
        }
    }
}
