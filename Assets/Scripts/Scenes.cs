using UnityEngine;
using UnityEngine.SceneManagement;

static class Scenes
{
	public enum Id : byte {
		MainMenu,
		Game,
	}


	public static void Load(this Id id) {
		SceneManager.LoadScene((byte)id);
	}

	public static void Reload() {
		Current().Load();
	}

	public static Id Current() {
		return (Id)SceneManager.GetActiveScene().buildIndex;
	}

	public static void QuitGame() {
		Savedata.Save(Savedata.settings);
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
		#elif UNITY_STANDALONE
			Application.Quit();
		#elif UNITY_WEBGL
			// FIXME this freezes the game, but i dont see a better thing to do here
			Application.Quit();
		#else
			#error Uknown Platform, don't know how to quit Game
		#endif
	}
}
