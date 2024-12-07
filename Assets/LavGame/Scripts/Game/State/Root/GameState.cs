using System;
using System.Collections.Generic;
using State.Maps;

[Serializable]
public class GameState
{
    public int GlobalEntityId;
    public int CurrentMapId;        // ID текущей карты. Нужно для сохранения.
    public List<MapState> Maps;     // список карт.

    public int CreateEntityId()     // создание идентификатора объекта.
    {
        return GlobalEntityId++;
    }
}
