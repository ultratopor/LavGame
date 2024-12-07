using Gameplay.Root;
using GameRoot;
using MainMenu.Root;
using R3;
using Root;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using BaCon;
using Settings;

namespace LavGame.Scripts
{   
    public class GameEntryPoint /* паттерн точка входа */
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines; /* создаём экземпляр класса с монобехой для корутины */
        private UIRootView _uiRoot; // создаём экземпляр класса, из которого будем управлять экраном загрузки
        private readonly DIContainer _rootContainer = new ();       // контейнер проекта.
        private DIContainer _cachedSceneContainer;                          // кэшированный контейнер сцены.

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] /* сработает перед загрузкой любой сцены */
        public static void AutoStartGame()
        {
            /* здесь можно задать самые системные настройки */
            Application.targetFrameRate = 60; /* 60 фпс */
            Screen.sleepTimeout = SleepTimeout.NeverSleep; /* отключает таймаут выключения подсветки экрана при бездействии */

            _instance = new GameEntryPoint(); /* создаём экземпляр класса, чтобы не работать со статическими методами */
            _instance.RunGame();
        }

        private GameEntryPoint()    /* приватный конструктор, потому что только внутри используется. Всё создаётся в статичном методе */
        {
            // создаём объект на сцене и добавляем в него класс Coroutines
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();

            // корутины удалятся во время загрузки, поэтому запрещаем удалять родительский объект корутины.
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            // загружаем в префаб созданный юнити экземпляр класса UIRootView.
            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");

            // создаём объект из префаба.
            _uiRoot = Object.Instantiate(prefabUIRoot);

            // UIRoot - ссылка на скрипт, поэтому запрещаем удалять родительский объект.
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
            _rootContainer.RegisterInstance(_uiRoot);       // запихиваем в общий контейнер. 

            // настройки приложения.
            var settingsProvider= new SettingsProvider();
                // регистрируем интерфейс, потому что настройки могут быть загружены из разных мест.
            _rootContainer.RegisterInstance<ISettingsProvider>(settingsProvider);

            var gameStateProvider = new PlayerPrefsGameStateProvider();
            gameStateProvider.LoadSettingsState();                                              // загружаем настройки.
            _rootContainer.RegisterInstance<IGameStateProvider>(gameStateProvider);

            // этот контейнер создастся не сразу, а по запросу.
            _rootContainer.RegisterFactory(_=> new SomeCommonService()).AsSingle(); // регистрируем как синглтон.
        }

        private async void RunGame() /* приватный, потому что вызывается внутри класса */
        {
                // загружаем настройки геймплея. Эта ссылка будет всё время в памяти.
            await _rootContainer.Resolve<ISettingsProvider>().LoadGameSettings();
// этот макрос нужен для определения загружаемой сцены. Работает только в редакторе (UNITY_EDITOR).
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;     // кэшируем название текущей сцены.

            if(sceneName == Scenes.GAMEPLAY)    // если загруженная сцена - нужная нам сцена (GAMEPLAY).
            {
                var enterParams = new GameplayEnterParams(1);   // затычка с фейковыми параметрами.
                _coroutines.StartCoroutine(LoadAndStartGameplay(enterParams));     // стартуем игру из редактора.
                return;
            }

            if(sceneName == Scenes.MAIN_MENU)                       // если загружаемая сцена - главное меню.
            {
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
            }

            if(sceneName != Scenes.BOOT)    // если заргужаемая сцена не BOOT и не GAMEPLAY, то её не нужно загружать.
            {
                return;
            }
#endif
            _coroutines.StartCoroutine(LoadAndStartMainMenu());     // стартуем не из редактора.
        }

        private IEnumerator LoadAndStartGameplay(GameplayEnterParams enterParams) // входные параметры внутри
        {
            _uiRoot.ShowLoadingScreen();    // включаем экран загрузки из UIRootView.
            _cachedSceneContainer?.Dispose();       // очищаем закэшированный контейнер, если он существует.

            /* загружает пустую сцену, чтобы выгрузилась старая, а новая загрузилась после пустой */
            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);    // загрузка основной сцены после пустой.

            yield return new WaitForSeconds(1);         // ждём две секунды, чтобы увидеть загрузочный экран.

            var isGameStateLoaded = false;      // создаём флажок для управления корутиною (для ожидания состояния).
            // достаём из контейнера GameStateProvider в абстрактной форме, загружаем состояние, на которое подписываемся, переключаем флажок.
            _rootContainer.Resolve<IGameStateProvider>().LoadGameState().Subscribe(_ => isGameStateLoaded = true);
            yield return new WaitUntil(()=> isGameStateLoaded);

            // ищем и кэшируем объект с GameplayEntryPoint.
            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();

			/* создаём и сразу кэшируем контейнер для сцены gameplay в родительском общем контейнере.
			 * создаём его не внутри сцены, чтобы не засовывать родительский контейнер на сцену, тем самым 
			 * не отходя от MVVM и SOLID*/
			var gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer);

			// sceneEntryPoint - монобеха, хранящаяся на сцене, которая будет уничтожена.
			sceneEntryPoint.Run(gameplayContainer, enterParams).Subscribe(GameplayExitParams =>
            { 
                _coroutines.StartCoroutine(LoadAndStartMainMenu(GameplayExitParams.MainMenuEnterParams)); 
            });

            _uiRoot.HideLoadingScreen();    // скрываем экран загрузки из UIRootView.
        }

        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null)      // веменная корутина для зауска сцены меню.
        {
            _uiRoot.ShowLoadingScreen();    // включаем экран загрузки из UIRootView.
			_cachedSceneContainer?.Dispose();       // очищаем закэшированный контейнер, если он существует.

			/* загружает пустую сцену, чтобы выгрузилась старая, а новая загрузилась после пустой */
			yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAIN_MENU);    // загрузка сцены меню после пустой.

            yield return new WaitForSeconds(1);         // ждём две секунды, чтобы увидеть загрузочный экран.

            // ищем и кэшируем объект с MainMenuEntryPoint.
            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();

			/* создаём и сразу кэшируем контейнер для сцены mainMenu в родительском общем контейнере.
			 * создаём его не внутри сцены, чтобы не засовывать родительский контейнер на сцену, тем самым 
			 * не отходя от MVVM и SOLID*/
			var mainMenuContainer = _cachedSceneContainer = new DIContainer (_rootContainer);

            sceneEntryPoint.Run(mainMenuContainer, enterParams).Subscribe(MainMEnuExitParams=>
            {
                var targetSceneName = MainMEnuExitParams.TargetSceneEnterParams.SceneName;

                if(targetSceneName == Scenes.GAMEPLAY)      // если ваыбранная сцена - основная, то запускаем её корутину.
                {
                    _coroutines.StartCoroutine(LoadAndStartGameplay(MainMEnuExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
                }
            });      


            _uiRoot.HideLoadingScreen();    // скрываем экран загрузки из UIRootView.
        }

        private IEnumerator LoadScene(string sceneName)     // корутина для загрузки заданной сцены.
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }

    }
}
