using Assets.LavGame.Scripts.Game.Common;
using Assets.LavGame.Scripts.Game.Gameplay.View.UI;
using BaCon;
using Gameplay;
using Gameplay.Root;
using LavGame.Scripts;
using LavGame.Scripts.Game.Gameplay.Root;
using LavGame.Scripts.Game.Gameplay.Root.View;
using MainMenu.Root;
using R3;
using UnityEngine;
using View;
/*в каждой сцене есть точка входа (GameplayEntryPoint). Снаружи запускается метод Run, чтобы загрузить сцену нужную.
менеджмент сцен происходит снаружи, поэтому снаружи передаются данные для загрузки сцен (сохранения и т.д.)
наследуется от монобехи, чтобы можно было ссылку сделать через инспектор.*/
namespace Root
{
	/// <summary>
	/// Точка входа
	/// </summary>
	public class GameplayEntryPoint : MonoBehaviour
	{
		// в этом месте будет связываться вид и логика, согласно паттерну MVVM.

		[SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
		[SerializeField] private WorldGameplayRootBinder _worldRootBinder;

		// передаём входные данные, возвращаем событие с выходными данными.
		public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
		{
			// сначала регистрируем все сущности, которые нужны для этой сцены.
			GameplayRegistrations.Register(gameplayContainer, enterParams);     // это статический метод.
			var gameplayViewModelsContainer = new DIContainer(gameplayContainer);   // так мы отделяем сервис от view.
			GameplayViewModelsRegistrations.Register(gameplayViewModelsContainer);

			InitWorld(gameplayViewModelsContainer);
			InitUI(gameplayViewModelsContainer);

			Debug.Log($"GAMEPLAY ENTRY POINT: level to load = {enterParams.MapId}");

			// в данном случае в качестве входных параметров передаём строку Fatality.
			var mainMenuEnterParams = new MainMenuEnterParams("Fatality");
			var exitParams = new GameplayExitParams(mainMenuEnterParams);
			var exitSceneRequest = gameplayContainer.Resolve<Subject<Unit>>(AppConstants.EXIT_SCENE_REQUEST_TAG);
			// отлавливаем сигнал и через select преобразовываем GameplayExitParams
			var exitToMainMenuSceneSignal = exitSceneRequest.Select(_=> exitParams);

			return exitToMainMenuSceneSignal;
		}

		private void InitWorld(DIContainer viewsContainer)
		{
			// для теста:
			_worldRootBinder.Bind(viewsContainer.Resolve<WorldGameplayRootViewModel>());
		}

		private void InitUI(DIContainer viewsContainer)
		{
			// создали UI для сцены
			var uiRoot = viewsContainer.Resolve<UIRootView>();
			
			// создали конкретный префаб для этого UI
			var uiSceneRootBinder = Instantiate(_sceneUIRootPrefab);
			
			// прикрепили префаб к UI сцены.
			uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);

			// Запрвшиваем корневую модель представления и запихиваем её в связыватель, который создали
			var uiSceneRootViewModel = viewsContainer.Resolve<UIGameplayRootViewModel>();
			uiSceneRootBinder.Bind(uiSceneRootViewModel);

			// можно открывать окошки
			var uiManager = viewsContainer.Resolve<GameplayUIManager>();
			// открыть первое окно по умолчанию
			uiManager.OpenScreenGameplay();
		}
	}
}