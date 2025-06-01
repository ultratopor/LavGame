using R3;
using Scripts.MVVM.UI;

namespace Assets.LavGame.Scripts.Game.Gameplay.View.UI.ScreenGameplay
{
	public class ScreenGameplayViewModel : WindowViewModel
	{
		private readonly GameplayUIManager _uIManager;
		private readonly Subject<Unit> _exitSceneRequest;

		public override string Id => "ScreenGameplay";

        public ScreenGameplayViewModel(GameplayUIManager uIManager, Subject<Unit> exitSceneRequest)
        {
			this._uIManager = uIManager;
			this._exitSceneRequest = exitSceneRequest;
		}

        public void RequestOpenPopupA()
		{
			_uIManager.OpenPopupA();
		}

		public void RequestOpenPopupB()
		{
			_uIManager.OpenPopupB();
		}

		public void RequestGoToMainMenu()
		{
			_exitSceneRequest.OnNext(Unit.Default);
		}
	}
}
