using System.Collections.Generic;
using Scripts.MVVM.UI;
using UnityEngine;

namespace Assets.LavGame.Scripts.MVVM.UI
{
	/// <summary>
	/// Контейнер окон и попапов UI.
	/// <para>
	/// Отвечает за создание, отображение и удаление экранов (screen) и всплывающих окон (popup) в пользовательском интерфейсе Unity.
	/// Позволяет открывать новые окна/попапы на основе ViewModel, автоматически загружая необходимые префабы,
	/// а также корректно закрывать и удалять их экземпляры.
	/// Контейнер ведет учёт текущего открытого экрана и всех открытых попапов на сцене.
	/// </para>
	/// Используется MVVM-инфраструктурой, взаимодействуя с <see cref="WindowViewModel"/> и <see cref="IWindowBinder"/>.
	/// </summary>
	public class WindowsContainer : MonoBehaviour
	{
		[SerializeField] private Transform _screensContainer;
		[SerializeField] private Transform _popupsContainer;

		private readonly Dictionary<WindowViewModel, IWindowBinder> _openedPopupBinders = new();
		private IWindowBinder _openedScreenBinder;

		public void OpenPopup(WindowViewModel viewModel)
		{
			// достаём префаб из папки
			var prefabPath = GetPrefabPath(viewModel);
			// и загружаем его как GameObject, потому что как IWindowBinder его не получится загрузить
			var prefab = Resources.Load<GameObject>(prefabPath);
			
			// создаём Popup
			var createdPopup = Instantiate(prefab, _popupsContainer);
			var binder = createdPopup.GetComponent<IWindowBinder>();

			binder.Bind(viewModel);
			_openedPopupBinders.Add(viewModel, binder);	// запихиваем в местный словарь, чтоб потом закрыть окна
		}

		public void ClosePopup(WindowViewModel popupViewModel)
		{
			var binder = _openedPopupBinders[popupViewModel];

			binder?.Close();
			_openedPopupBinders.Remove(popupViewModel);
		}

		/// Открывает неоткрытое окно, закрывает то, что открыто, и открывает новое
		public void OpenScreen(WindowViewModel viewModel)
		{       
			if(viewModel == null)
				return;

			_openedScreenBinder?.Close();

			var prefabPath = GetPrefabPath(viewModel);
			var prefab = Resources.Load<GameObject>(prefabPath);
			
			var createdScreen = Instantiate(prefab, _screensContainer);
			var binder = createdScreen.GetComponent<IWindowBinder>();

			binder.Bind(viewModel);
			_openedScreenBinder = binder;
		}

		private static string GetPrefabPath(WindowViewModel viewModel)
		{
			return $"Prefabs/UI/{viewModel.Id}";
		}
	}
}
