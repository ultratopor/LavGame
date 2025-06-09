using Assets.LavGame.Scripts.MVVM.UI;
using ObservableCollections;
using R3;
using UnityEngine;

namespace LavGame.Scripts.MVVM.UI
{
    /// <summary>
    /// Компонент-связыватель корня UI сцены.
    /// <para>
    /// Является мостом между корневой ViewModel интерфейса (<see cref="UIRootViewModel"/>) и визуальным контейнером окон в Unity.
    /// </para>
    /// Подписывается на изменения корневой ViewModel (открытие/закрытие экранов и попапов),
    /// автоматически управляя показом, добавлением и удалением соответствующих окон через <see cref="WindowsContainer"/>.
    /// Все подписки удаляются при уничтожении объекта (OnDestroy).
    /// </summary>
    public class UIRootBinder : MonoBehaviour
    {
        /// <summary>
        /// Ссылка на компонент-контейнер, управляющий отображением окон и попапов в UI (реализует создание/удаление окон).
        /// </summary>
        [SerializeField] private WindowsContainer _windowsContainer;

        /// <summary>
        /// Коллекция подписок на события ViewModel, очищается при уничтожении объекта для предотвращения утечек памяти.
        /// </summary>
        private readonly CompositeDisposable _subscriptions = new();

        /// <summary>
        /// Связывает данный биндер с указанной корневой ViewModel UI.
        /// Автоматически подписывается на открытие/закрытие экранов (Windows) и попапов.
        /// </summary>
        /// <param name="viewModel">Экземпляр <see cref="UIRootViewModel"/> для отображения в данном UI.</param>
        public void Bind(UIRootViewModel viewModel)
        {
            // Подписка на смену текущего экрана: при каждом изменении вызывается открытие нового окна.
            _subscriptions.Add(viewModel.OpenedScreen.Subscribe(newScreenViewModel =>
            {
                _windowsContainer.OpenScreen(newScreenViewModel);
            }));

            // Перебор уже открытых попапов, чтобы сразу отобразить их в UI.
            foreach (var openedPopup in viewModel.OpenedPopups)
            {
                _windowsContainer.OpenPopup(openedPopup);
            }

            // Подписка на добавление новых попапов — при добавлении вызывается OpenPopup.
            _subscriptions.Add(viewModel.OpenedPopups.ObserveAdd().Subscribe(e =>
            {
                _windowsContainer.OpenPopup(e.Value);
            }));

            // Подписка на удаление попапов — при удалении вызывается ClosePopup.
            _subscriptions.Add(viewModel.OpenedPopups.ObserveRemove().Subscribe(e =>
            {
                _windowsContainer?.ClosePopup(e.Value);
            }));

            // Метод для реализации дополнительной пользовательской логики при биндинге (можно переопределять в наследниках).
            OnBind(viewModel);
        }

        /// <summary>
        /// Виртуальный метод для расширения логики биндинга — переопределяется в наследуемых классах.
        /// </summary>
        /// <param name="viewModel">Экземпляр ViewModel. Можно использовать для дополнительных подписок или инициализации.</param>
        protected virtual void OnBind(UIRootViewModel viewModel) { }

        /// <summary>
        /// При уничтожении GameObject все подписки автоматически очищаются,
        /// предотвращая утечки памяти и повторные вызовы.
        /// </summary>
        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
}