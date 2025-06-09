using Assets.LavGame.Scripts.MVVM.UI;
using Scripts.MVVM.UI;
using UnityEngine;

namespace LavGame.Scripts.MVVM.UI
{
	/* интерфейс нужен, чтобы создавать список WindowBinder, ибо невозможно запрашивать из списка обобщённые классы*/
	public abstract class WindowBinder<T> : MonoBehaviour, IWindowBinder where T : WindowViewModel
	{
		protected T ViewModel;

		public void Bind(WindowViewModel viewModel)
		{
			ViewModel = (T)viewModel;

			OnBind(ViewModel);
		}

		public virtual void Close()
		{
			// Здесь мы сначала будем уничтожать, а потом можно делать анимации на закрытие.
			Destroy(gameObject);
		}

		protected virtual void OnBind(T viewModel) { }
	}
}
