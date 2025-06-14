using System.Collections.Generic;
using LavGame.Scripts.Game.State.GameResources;
using LavGame.Scripts.Game.State.Maps;

namespace LavGame.Scripts.Game.State.Root
{
    /// <summary>
    /// Класс, представляющий сериализуемое состояние игры.
    /// Содержит данные о текущей карте, глобальный идентификатор сущностей, списки всех карт и ресурсов.
    /// Является основной структурой для сохранения и загрузки данных прогресса игрока.
    /// </summary>
    public class GameState
    {
        public int GlobalEntityId { get; set; }
        public int CurrentMapId { get; set;}
        public List<MapData> Maps { get; set; }
        public List<ResourceData> Resources { get; set; }

        public int CreateEntityId()
        {
            return GlobalEntityId++;
        }
    }
}