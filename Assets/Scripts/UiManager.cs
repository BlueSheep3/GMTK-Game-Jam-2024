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
		scoreText.text = "Height: " + height + "m\nShapes: " + count;
	}

	internal void EndGame() {
		gameOver.SetActive(true);
		PlacingManager pm = PlacingManager.Inst;
		int height = pm.maxHeight;
		int count = pm.placedShapes.Count;
		finalScoreText.text = "Height: " + height + "m\nShapes: " + count;
		Settings settings = Savedata.settings;
		if(settings.maxHeight < height) settings.maxHeight = height;
		if(settings.maxShapes < count) settings.maxShapes = count;
		Save(settings);
	}
}
