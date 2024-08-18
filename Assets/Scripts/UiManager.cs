using UnityEngine;
using UnityEngine.UI;
using TMPro;

class UiManager : MonoBehaviour
{
	[SerializeField] TMP_Text scoreText;
	[SerializeField] Image nextShapeImage;

	// TODO update all the information in the in game UI
	void Update() {
		int height = (int)PlacingManager.Inst.maxHeight;
		int count = PlacingManager.placedShapes.Count;
		scoreText.text = "Height: " + height + "m\nShapes: " + count;
	}
}
