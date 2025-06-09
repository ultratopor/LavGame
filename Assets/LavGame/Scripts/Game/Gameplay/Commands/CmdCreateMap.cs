namespace LavGame.Scripts.Game.Gameplay.Commands
{
    /// <summary>
    /// Команда создания состояния карты
    /// </summary>
	public class CmdCreateMap : ICommand
	{
		public readonly int MapId;

        public CmdCreateMap(int mapId)
        {
            MapId = mapId;
        }
    }
}
