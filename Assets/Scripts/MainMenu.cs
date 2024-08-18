using UnityEngine;
using UnityEngine.UI;

class MainMenu : MonoBehaviour
{
	// NOTE this entire MainMenu is very temporary right now
	// it mostly just exists to have something here

	[SerializeField] Button fullscreenButton;


	void Awake() {
		#if UNITY_WEBGL
			Destroy(fullscreenButton.gameObject);
		#endif
	}


	public void StartGame() {
		Scenes.Id.Game.Load();
	}

	public void ChangeFullscreen() {
		Screen.fullScreen = !Screen.fullScreen;
	}

	public void Quit() {
		Scenes.QuitGame();
	}
}
