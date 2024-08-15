using BaCon;

using Gameplay.Root;
using LavGame.Scripts;
using MainMenu.Root;
using R3;
using UnityEngine;
using View;
/*в каждой сцене есть точка входа (GameplayEntryPoint). Снаружи запускается метод Run, чтобы загрузить сцену нужную.
менеджмент сцен происходит снаружи, поэтому снаружи передаются данные для загрузки сцен (сохранения и т.д.)
наследуется от монобехи, чтобы можно было ссылку сделать через инспектор.*/
namespace Root
{        
    public class GameplayEntryPoint : MonoBehaviour
    {
        // в этом месте будет связываться вид и логика, согласно паттерну MVVM.

        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
        // передаём входные данные, возвращаем событие с выходными данными.
        public Observable<GameplayExitParams> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
        {
            var uiRoot = gameplayContainer.Resolve<UIRootView>();
            var uiScene = Instantiate(_sceneUIRootPrefab);      // создаём экземпляр префаба.
            uiRoot.AttachSceneUI(uiScene.gameObject);           // добавляет uiScene в uiRoot.
            
            var exitSceneSignalSubj = new Subject<Unit>(); // 
            uiScene.Bind(exitSceneSignalSubj);

            Debug.Log($"GAMEPLAY ENTRY POINT: save file name = {enterParams.SaveFileName}, level to load = {enterParams.LevelNumber}");

			// в данном случае в качестве входных параметров передаём строку Fatality.
			var mainMenuEnterParams = new MainMenuEnterParams("Fatality");
            var exitParams = new GameplayExitParams(mainMenuEnterParams);
			// отлавливаем сигнал и через select преобразовываем GameplayExitParams
			var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_=> exitParams);

            return exitToMainMenuSceneSignal;
        }
    }
}