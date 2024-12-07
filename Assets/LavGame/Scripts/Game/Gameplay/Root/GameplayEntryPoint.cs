using BaCon;
using Gameplay;
using Gameplay.Root;
using LavGame.Scripts;
using MainMenu.Root;
using R3;
using UnityEngine;
using View;
using System.Linq;
using ObservableCollections;
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
			
			// для теста:
			_worldRootBinder.Bind(gameplayViewModelsContainer.Resolve<WorldGameplayRootViewModel>());

			gameplayViewModelsContainer.Resolve<UIGameplayRootViewModel>();

			var uiRoot = gameplayContainer.Resolve<UIRootView>();
			var uiScene = Instantiate(_sceneUIRootPrefab);      // создаём экземпляр префаба.
			uiRoot.AttachSceneUI(uiScene.gameObject);           // добавляет uiScene в uiRoot.

			var exitSceneSignalSubj = new Subject<Unit>(); // 
			uiScene.Bind(exitSceneSignalSubj);

			Debug.Log($"GAMEPLAY ENTRY POINT: level to load = {enterParams.MapId}");

			// в данном случае в качестве входных параметров передаём строку Fatality.
			var mainMenuEnterParams = new MainMenuEnterParams("Fatality");
			var exitParams = new GameplayExitParams(mainMenuEnterParams);
			// отлавливаем сигнал и через select преобразовываем GameplayExitParams
			var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_=> exitParams);

			return exitToMainMenuSceneSignal;
		}

		private Vector3Int GetRandomPosition()  // для теста.
		{
			var rX = Random.Range(-10,10);
			var rY = Random.Range(-10,10);
			var rPosition = new Vector3Int(rX,rY, 0);

			return rPosition;
		}
	}
}