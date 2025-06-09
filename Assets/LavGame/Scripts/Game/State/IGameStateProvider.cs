using LavGame.Scripts.Game.State.Root;
using R3;

public interface IGameStateProvider
{
	public GameStateProxy GameState { get; }    // задаётся внутри.
	public GameSettingsStateProxy SettingsState { get; }

	public Observable<GameStateProxy> LoadGameState();  // Observable - чтобы подождать. Запустить загрузку и подождать.
	public Observable<GameSettingsStateProxy> LoadSettingsState();

	public Observable<bool> SaveGameState();            // проверяем сохранено или нет.
	public Observable<bool> SaveSettingsState();

	public Observable<bool> ResetGameState();
	public Observable<GameSettingsStateProxy> ResetSettingsState();
}