using Assets.LavGame.Scripts.Game.Common;
using Assets.LavGame.Scripts.Game.Gameplay.View.UI.PopupA;
using Assets.LavGame.Scripts.Game.Gameplay.View.UI.PopupB;
using Assets.LavGame.Scripts.Game.Gameplay.View.UI.ScreenGameplay;
using Assets.LavGame.Scripts.MVVM.UI;
using BaCon;
using R3;

namespace Assets.LavGame.Scripts.Game.Gameplay.View.UI
{
	public class GameplayUIManager : UIManager
	{
		private readonly Subject<Unit> _exitSceneRequest;

		public GameplayUIManager(DIContainer container) : base(container)
		{
			_exitSceneRequest = container.Resolve<Subject<Unit>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
		}

		public ScreenGameplayViewModel OpenScreenGameplay()
		{
			// создаём вьюмодель, всунув туда этот менеджер, чтобы можно было воспользоваться другими методами этого класса.
			var viewModel = new ScreenGameplayViewModel(this, _exitSceneRequest);
			// чтобы избежать циклических зависимостей, кешируем RootViewModel при запросе окна, а не в конструкторе.
			var rootUI = Container.Resolve<UIGameplayRootViewModel>();

			rootUI.OpenScreen(viewModel);
			return viewModel;
		}

		public PopupAViewModel OpenPopupA()
		{
			var a = new PopupAViewModel();
			var rootUI = Container.Resolve<UIGameplayRootViewModel>();

			rootUI.OpenPopup(a);

			return a;
		}

		public PopupBViewModel OpenPopupB()
		{
			var b = new PopupBViewModel();
			var rootUI = Container.Resolve<UIGameplayRootViewModel>();

			rootUI.OpenPopup(b);

			return b;
		}
	}
}
