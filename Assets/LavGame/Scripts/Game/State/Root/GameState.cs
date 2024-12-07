using System;
using System.Collections.Generic;
using State.Maps;

[Serializable]
public class GameState
{
    public int GlobalEntityId;
    public int CurrentMapId;        // ID ������� �����. ����� ��� ����������.
    public List<MapState> Maps;     // ������ ����.

    public int CreateEntityId()     // �������� �������������� �������.
    {
        return GlobalEntityId++;
    }
}
