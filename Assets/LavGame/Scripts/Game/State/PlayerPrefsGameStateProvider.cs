using System.Collections.Generic;
using R3;

using State.Maps;

using UnityEngine;

public class PlayerPrefsGameStateProvider : IGameStateProvider
{
	private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);	// создаём ключ.
	private const string GAME_SETTINGS_STATE_KEY = nameof( GAME_SETTINGS_STATE_KEY);
	public GameStateProxy GameState { get; private set; }   // меняется внутри.
	public GameSettingsStateProxy SettingsState { get; private set; }

	private GameState _gameStateOrigin;						// состояние, которое будет загружаться или сохраняться.
	private GameSettingsState _gameSettingsStateOrigin;

	public Observable<GameStateProxy> LoadGameState()
	{
		if(!PlayerPrefs.HasKey(GAME_STATE_KEY))     // если в PlayerPrefs нет значений по ключу.
		{
			GameState = CreateGameStateFromSettings();	// то создаём его.
			Debug.Log("Game State created from settings: "+ JsonUtility.ToJson(_gameStateOrigin, true));

			SaveGameState();		// сохраним дефолтное состояние.
		} else
		{	// если ключ есть.
				// загружаем
			var json = PlayerPrefs.GetString(GAME_STATE_KEY);
			_gameStateOrigin = JsonUtility.FromJson<GameState> (json);
			GameState = new GameStateProxy(_gameStateOrigin);		// создаём прокси при помощи проксирования.

			Debug.Log("Game State loaded: " + json);
		}

		return Observable.Return(GameState);
	}

	public Observable<GameSettingsStateProxy> LoadSettingsState()
	{
		if(!PlayerPrefs.HasKey(GAME_SETTINGS_STATE_KEY))
		{
			SettingsState = CreateGameSettingsStateFromSettings();

			SaveSettingsState();		// сохраним дефолтное состояние.
		} else
		{
			// загружаем
			var json = PlayerPrefs.GetString (GAME_SETTINGS_STATE_KEY);
			_gameSettingsStateOrigin = JsonUtility.FromJson<GameSettingsState> (json);
			SettingsState = new GameSettingsStateProxy(_gameSettingsStateOrigin);
		}

		return Observable.Return(SettingsState);
	}

	public Observable<bool> ResetGameState()
	{
		GameState = CreateGameStateFromSettings();
		SaveGameState();

		return Observable.Return(true);
	}

	public Observable<GameSettingsStateProxy> ResetSettingsState()
	{
		SettingsState = CreateGameSettingsStateFromSettings();
		SaveSettingsState();

		return Observable.Return(SettingsState);
	}

	public Observable<bool> SaveGameState()
	{		// оригинальное сериализуемое состояние синхронизовано с прокси, поэтому его можно запихнуть в json строку.
		var json = JsonUtility.ToJson(_gameStateOrigin, true);
		PlayerPrefs.SetString(GAME_STATE_KEY, json);

		return Observable.Return(true);
	}

	public Observable<bool> SaveSettingsState()
	{
		var json = JsonUtility.ToJson (_gameSettingsStateOrigin, true);
		PlayerPrefs.SetString(GAME_SETTINGS_STATE_KEY, json);

		return Observable.Return(true);
	}

	private GameStateProxy CreateGameStateFromSettings()	// здесь синхронизируется прокси и оригинальное состояние.
	{       // создаём состояние игры из сохранённых настроек.
		_gameStateOrigin = new GameState    // создаём оригинальное сериализуемое состояние через GameState
		{
			Maps = new List<MapState>()
		};

		return new GameStateProxy(_gameStateOrigin);	// возвращаем завёрнутое в прокси состояние.
	}

	private GameSettingsStateProxy CreateGameSettingsStateFromSettings()
	{
		// состояние по умолчанию из настроек, мы делаем фейк.
		_gameSettingsStateOrigin = new GameSettingsState()
		{
			MusicVolume = 8,
			SFXVolume = 8
		};

		return new GameSettingsStateProxy(_gameSettingsStateOrigin);
	}
}