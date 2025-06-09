using BaCon;
using Gameplay.Root;
using MainMenu;
using MainMenu.Root;
using R3;
using Root;
using UnityEngine;
using View;
using Random = UnityEngine.Random;

namespace LavGame.Scripts.Game.MainMenu.Root
{
	public class MainMenuEntryPoint : MonoBehaviour
	{
		// � ���� ����� ����� ����������� ��� � ������, �������� �������� MVVM.

		[SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

		public Observable<MainMEnuExitParams> Run(DIContainer mainMenuContainer, MainMenuEnterParams enterParams)   // ����� ����� ����������� DiContainer, �������� ����������� uiRoot.
		{
			// ������� ������������ ��� ��������, ������� ����� ��� ���� �����.
			MainMenuRegistrations.Register(mainMenuContainer, enterParams);     // ��� ����������� �����.
			var mainMenuViewModelsContainer = new DIContainer(mainMenuContainer);   // ��� �� �������� ������ �� view.
			MainMenuViewModelsRegistrations.Register(mainMenuViewModelsContainer);

			//

			// ��� �����:
			mainMenuViewModelsContainer.Resolve<UIMainMenuRootViewModel>();

			var uiRoot = mainMenuContainer.Resolve<UIRootView>();		// ����������� �� ������������� ���������� ������.
			var uiScene = Instantiate(_sceneUIRootPrefab);      // ������ ��������� �������.
			uiRoot.AttachSceneUI(uiScene.gameObject);           // ��������� uiScene � uiRoot.

			var exitSignalSubj = new Subject<Unit>();
			uiScene.Bind(exitSignalSubj);

			Debug.Log($"MAIN MENU ENTRY POINT: Run main menu scene. Results: {enterParams?.Result}");

			var saveFileName = "ololo.save";
			var levelNumber = Random.Range(0,300);
			var gameplayEnterParams = new GameplayEnterParams(0);
			var mainMenuExitParams = new MainMEnuExitParams(gameplayEnterParams);
			// ������������ mainMenuExitParams � Subject.
			var exitToGameplaySceneSignal = exitSignalSubj.Select(_=> mainMenuExitParams);

			return exitToGameplaySceneSignal;
		}
	}
}
