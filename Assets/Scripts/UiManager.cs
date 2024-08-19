using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Savedata;

class UiManager : MonoBehaviour
{
	[SerializeField] TMP_Text scoreText;
	[SerializeField] Image nextShapeImage;
	[SerializeField] GameObject gameOver;
	[SerializeField] TMP_Text finalScoreText;

	// TODO update all the information in the in game UI
	void Update() {
		PlacingManager pm = PlacingManager.Inst;
		int height = pm.maxHeight;
		int count = pm.placedShapes.Count;
		scoreText.text = $"Height: {height}m\nScore: {count}";
	}

	internal void EndGame() {
		gameOver.SetActive(true);
		PlacingManager pm = PlacingManager.Inst;
		int height = pm.maxHeight;
		int count = pm.placedShapes.Count;
		finalScoreText.text = "Game Over!";
		bool gotNewHighscore = false;
		Settings settings = Savedata.settings;
		if(settings.maxHeight < height) {
			settings.maxHeight = height;
			gotNewHighscore = true;
		}
		if(settings.maxShapes < count) {
			settings.maxShapes = count;
			gotNewHighscore = true;
		}
		if(gotNewHighscore)
			finalScoreText.text = "New Highscore!";
		Save(settings);
	}
}
