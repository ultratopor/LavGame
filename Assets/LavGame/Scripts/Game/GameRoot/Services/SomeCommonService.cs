using UnityEngine;

public class SomeCommonService
{
    // Например провайдер состояния или провайдер настроек, сервис аналитики, платежей
    public SomeCommonService()
    {
        Debug.Log(GetType().Name+"has been created");
    }
}
