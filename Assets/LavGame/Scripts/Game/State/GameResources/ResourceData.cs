using System;

namespace LavGame.Scripts.Game.State.GameResources
{
    // все файлы с Data - оригинал, без неё - заместители.
    
    /// <summary>
    /// Игровые расходники
    /// </summary>
    [Serializable]
    public class ResourceData
    {
        public ResourceType ResourceType;
        public int Amount;
    }
}