﻿using System.Linq;
using R3;

public class GameSettingsStateProxy
{
	public ReactiveProperty<int> MusicVolume { get; }
	public ReactiveProperty<int> SFXVolume { get; }

	public GameSettingsStateProxy(GameSettingsState gameSettingsState)
	{
		MusicVolume = new ReactiveProperty<int>(gameSettingsState.MusicVolume);
		SFXVolume = new ReactiveProperty<int>(gameSettingsState.SFXVolume);

		MusicVolume.Skip(1).Subscribe(value => gameSettingsState.MusicVolume = value);
		SFXVolume.Skip(1).Subscribe(value => gameSettingsState.SFXVolume = value);
	}
}
