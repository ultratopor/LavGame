using BaCon;

using Gameplay.Root;

using LavGame.Scripts;

using MainMenu.Root;

using R3;

using Root;

using UnityEngine;

using Random = UnityEngine.Random;

public class MainMenuEntryPoint : MonoBehaviour
{
	// в этом месте будет св€зыватьс€ вид и логика, согласно паттерну MVVM.

	[SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

	public Observable<MainMEnuExitParams> Run(DIContainer mainMenuContainer, MainMenuEnterParams enterParams)   // здесь будет загружатьс€ DiContainer, временно загружаетс€ uiRoot.
	{
		var uiRoot = mainMenuContainer.Resolve<UIRootView>();		// вытаскиваем из родительского контейнера данные.
		var uiScene = Instantiate(_sceneUIRootPrefab);      // создаЄм экземпл€р префаба.
		uiRoot.AttachSceneUI(uiScene.gameObject);           // добавл€ет uiScene в uiRoot.

		var exitSignalSubj = new Subject<Unit>();
		uiScene.Bind(exitSignalSubj);

		Debug.Log($"MAIN MENU ENTRY POINT: Run main menu scene. Results: {enterParams?.Result}");

		var saveFileName = "ololo.save";
		var levelNumber = Random.Range(0,300);
		var gameplayEnterParams = new GameplayEnterParams(saveFileName, levelNumber);
		var mainMenuExitParams = new MainMEnuExitParams(gameplayEnterParams);
		// конвертируем mainMenuExitParams в Subject.
		var exitToGameplaySceneSignal = exitSignalSubj.Select(_=> mainMenuExitParams);

		return exitToGameplaySceneSignal;
	}
}
