using System;
using System.Collections.Generic;
using System.Linq;
using ObservableCollections;
using R3;
using Scripts.MVVM.UI;

namespace Assets.LavGame.Scripts.MVVM.UI
{
	/// <summary>
	/// ViewModel главного UI корня.
	/// Управляет состоянием главного экрана и коллекцией открытых попапов (диалоговых окон),
	/// а также предоставляет методы для открытия и закрытия окон/попапов.
	/// Реализует механизм подписки на изменения и корректное освобождение ресурсов.
	/// </summary>
	public class UIRootViewModel : IDisposable
	{
		/// <summary>
		/// Открытый экран (главное окно) — реагируем на его изменения и отображаем в UI
		/// </summary>
		public ReadOnlyReactiveProperty<WindowViewModel> OpenedScreen => _openedScreen;

		/// <summary>
		/// Коллекция открытых попапов (диалоговых окон)
		/// </summary>
		public IObservableCollection<WindowViewModel> OpenedPopups => _openedPopups;

		// Реактивное свойство для текущего открытого экрана (не забывайте его инициализировать!)
		private readonly ReactiveProperty<WindowViewModel> _openedScreen = new(null);

		// Коллекция открытых попапов
		private readonly ObservableList<WindowViewModel> _openedPopups = new();

		// Словарь для хранения подписок на закрытие попапа по ViewModel (чтобы можно было отписаться)
		private readonly Dictionary<WindowViewModel,IDisposable> _popupSubscriptions = new();

		public void Dispose()
		{
			// Закрываем все попапы и освобождаем память
			CloseAllPopups();
			_openedScreen.Value?.Dispose();
		}

		/// <summary>
		/// Открыть новое окно — предыдущее закроется и удалится
		/// </summary>
		/// <param name="screenViewModel">ViewModel открываемого окна</param>
		public void OpenScreen(WindowViewModel screenViewModel)
		{
			// Закрываем (удаляем) предыдущее окно, если оно было открыто
			_openedScreen.Value?.Dispose();
			_openedScreen.Value = screenViewModel;
		}

		/// <summary>
		/// Открыть попап (если не открыт)
		/// </summary>
		/// <param name="popupViewModel">ViewModel попапа</param>
		public void OpenPopup(WindowViewModel popupViewModel)
		{
			// Если попап уже открыт — не открываем ещё раз
			if(_openedPopups.Contains(popupViewModel))
				return;

			// Подписываемся на событие закрытия для этого попапа (при закрытии — автоматически вызовется ClosePopup)
			var subscription = popupViewModel.CloseRequested.Subscribe(ClosePopup);
			_popupSubscriptions.Add(popupViewModel, subscription);

			_openedPopups.Add(popupViewModel);
		}

		/// <summary>
		/// Закрыть попап по ViewModel
		/// </summary>
		/// <param name="popupViewModel">ViewModel закрываемого попапа</param>
		public void ClosePopup(WindowViewModel popupViewModel)
		{
			if(_openedPopups.Contains(popupViewModel))
			{
				popupViewModel.Dispose();
				_openedPopups.Remove(popupViewModel);

				// Освобождаем подписку на событие закрытия
				var popupSubscription = _popupSubscriptions[popupViewModel];
				popupSubscription?.Dispose();
				_popupSubscriptions.Remove(popupViewModel);
			}
		}

		/// <summary>
		/// Закрыть попап по Id (определяется через ViewModel.Id)
		/// </summary>
		/// <param name="popupId">Id попапа</param>
		public void ClosePopup(string popupId)
		{
			var openedPopupViewModel = _openedPopups.FirstOrDefault(p => p.Id == popupId);
			ClosePopup(openedPopupViewModel);
		}

		/// <summary>
		/// Закрыть все открытые попапы
		/// </summary>
		public void CloseAllPopups()
		{
			foreach(var openedPopup in _openedPopups)
			{
				ClosePopup(openedPopup);
			}
		}
	}
}