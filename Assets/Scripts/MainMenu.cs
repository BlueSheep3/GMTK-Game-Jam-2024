using UnityEngine;
using UnityEngine.UI;
using TMPro;

class MainMenu : MonoBehaviour
{
	[SerializeField] TMP_Text highscoresText;
	[SerializeField] Slider volumeSlider;
	[SerializeField] TMP_Text volumeText;
	[SerializeField] RectTransform topButtons;
	[SerializeField] RectTransform bottomButtons;
	[SerializeField] Button fullscreenButton;


	void Awake() {
		#if UNITY_WEBGL
			topButtons.anchoredPosition += Vector2.down * 75f;
			bottomButtons.anchoredPosition += Vector2.up * 75f;
			Destroy(fullscreenButton.gameObject);
		#endif
	}

	void Start() {
		int height = Savedata.settings.maxHeight;
		int count = Savedata.settings.maxShapes;
		highscoresText.text = $"Best Height: {height}m\nHigh Score: {count}";
		volumeSlider.value = Savedata.settings.volume / 100f;
		volumeText.text = "Volume: " + Mathf.RoundToInt(Savedata.settings.volume) + "%";
	}


	public void StartGame() {
		Scenes.Id.Game.Load();
	}

	public void ChangeFullscreen() {
		if(Screen.fullScreen) {
			Screen.SetResolution(960, 540, false);
		} else {
			const float ASPECT_RATIO = 16f / 9f;
			int width = Screen.currentResolution.width;
			int height = Screen.currentResolution.height;
			float aspectRatio = width / (float)height;

			if(Mathf.Abs(aspectRatio - ASPECT_RATIO) < 0.01)
				Screen.SetResolution(width, height, true);
			else if(aspectRatio < ASPECT_RATIO)
				Screen.SetResolution(width, (int)(width / ASPECT_RATIO), true);
			else
				Screen.SetResolution((int)(height * ASPECT_RATIO), height, true);
		}
	}

	public void ChangeVolume() {
		float newValue = volumeSlider.value;
		Savedata.settings.volume = Mathf.RoundToInt(newValue * 100);
		AudioListener.volume = newValue;
		volumeText.text = "Volume: " + Mathf.RoundToInt(newValue * 100) + "%";
	}

	public void Quit() {
		Scenes.QuitGame();
	}
}
