using MainMenu.Root;

namespace Gameplay.Root
{
    public  class GameplayExitParams
    {
        public MainMenuEnterParams MainMenuEnterParams { get; }
        // выходные параметры сцены игры - это входные параметры главного меню
        public GameplayExitParams(MainMenuEnterParams mainMenuEnterParams)
        {  
            MainMenuEnterParams = mainMenuEnterParams; 
        }
    }
}
