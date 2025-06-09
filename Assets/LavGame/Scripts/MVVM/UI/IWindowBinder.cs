using LavGame.Scripts.MVVM.UI;
using Scripts.MVVM.UI;

namespace Assets.LavGame.Scripts.MVVM.UI
{
    /// <summary>
    /// Интерфейс связывателя окна (UI Window Binder) в архитектуре MVVM для Unity.
    /// <para>
    /// Реализуется компонентами, которые обеспечивают связь между ViewModel окна и его визуальным представлением
    /// (например, для экранов или попапов). Определяет методы для привязки данных и закрытия окна.
    /// Используется контейнером окон (<see cref="WindowsContainer"/>), чтобы взаимодействовать с UI окнами через общую абстракцию.
    /// </para>
    /// </summary>
    public interface IWindowBinder
    {
        /// <summary>
        /// Привязывает к визуальному компоненту переданную ViewModel окна.
        /// Вызывается при создании и инициализации окна/попапа.
        /// </summary>
        /// <param name="viewModel">Экземпляр ViewModel, содержащий состояние и логику окна.</param>
        void Bind(WindowViewModel viewModel);

        /// <summary>
        /// Закрывает связанное окно/попап и выполняет необходимую очистку ресурсов.
        /// </summary>
        void Close();
    }
}