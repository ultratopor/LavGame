using UnityEngine;

namespace Settings
{
	/// <summary>
	/// Ковырябельные в настройках настройки игры
	/// </summary>
	[CreateAssetMenu(fileName = "ApplicationSettings", menuName = "Game Settings/New Application Settings")]
	public class ApplicationSettings : ScriptableObject
	{
		// настройки игры.
		public int MusicVolume;
		public int SFXVolume;
		public string Difficalty;
	}
}