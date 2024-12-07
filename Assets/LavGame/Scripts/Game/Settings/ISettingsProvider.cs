using System.Threading.Tasks;

namespace Settings
{       // интерфейс создания ссылок на строковые ссылки на префабы.
		// Делается это для того, чтобы ссылки на префабы не засоряли оперативную память.
		// интерфейс нужен, чтобы можно было реализовать загрузку разными способами.
	public interface ISettingsProvider
	{
		GameSettings GameSettings { get; }  // возвращает настройки геймплея.
		ApplicationSettings ApplicationSettings { get; }    // возвращает настройки игры. Загружаются при запуске.

		Task<GameSettings> LoadGameSettings();  // загрузчик настроек геймплея.
	}
}
