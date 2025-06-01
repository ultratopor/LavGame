using System;
using R3;

namespace Scripts.MVVM.UI
{
	/// <summary>
	/// Абстрактная базовая ViewModel для окон и попапов в MVVM-архитектуре UI.
	/// Обеспечивает механизм закрытия окна через событие, а также идентификацию экземпляров через Id.
	/// Все дочерние ViewModel окон должны реализовать идентификатор, при необходимости — логику освобождения ресурсов.
	/// </summary>
	public abstract class WindowViewModel : IDisposable
	{
		/// <summary>
		/// Событие запроса закрытия окна.
		/// </summary>
		public Observable<WindowViewModel> CloseRequested => _closeRequested;

		/// <summary>
		/// Уникальный идентификатор ViewModel окна (например, используется для поиска и закрытия окон).
		/// </summary>
		public abstract string Id { get; }

		// Приватное поле для хранения объекта-субъекта события закрытия
		private readonly Subject<WindowViewModel> _closeRequested = new();

		/// <summary>
		/// Запросить закрытие данного окна. Вызывает событие CloseRequested.
		/// </summary>
		public void RequestClose()
		{
			_closeRequested.OnNext(this);
		}

		/// <summary>
		/// Освободить ресурсы ViewModel. Переопределяется в наследниках при необходимости.
		/// </summary>
		public virtual void Dispose() { }
	}
}