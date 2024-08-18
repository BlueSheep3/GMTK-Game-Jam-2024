using System.Collections.Generic;
using UnityEngine;

class PlacingManager : MonoBehaviour
{
	public static PlacingManager Inst;

	[SerializeField] PreviewShape[] previewShapes;
	[SerializeField] UiManager uiManager;

	internal List<Shape> placedShapes = new();
	internal int maxHeight = 0;
	internal bool isPlacing = false;

	float difficulty = 0f;
	Queue<PreviewShape> previewShapeQueue = new();
	PreviewShape currentPreviewShape;

	void Awake() {
		if(Inst == null) Inst = this;
		else {
			Destroy(Inst);
			Inst = this;
			Debug.LogError("shouldn't have 2 placing managers at once");
		}
	}

	void Start() {
		for(int i = 0; i < 5; i++) {
			previewShapeQueue.Enqueue(GetPreviewShape());
		}
		SelectRandomShape();
	}

	void Update() {
		Vector2 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		bool canPlace = true;
		if(Vector3.Distance(currentPreviewShape.transform.position, mousePos) > 3)
			canPlace = false; // very sudden movement, probably didnt want to place it

		// place before moving, so that the CanBePlacedHere check works
		if(canPlace && !isPlacing && Input.GetMouseButtonDown(0) && currentPreviewShape.CanBePlacedHere()) {
			placedShapes.Add(currentPreviewShape.Place());
			isPlacing = true;
			SelectRandomShape();
		}

		currentPreviewShape.transform.position = mousePos;
	}

	void OnDestroy() {
		Inst = null;
	}


	internal void EndGame() {
		GameCamera.follow = false;
		isPlacing = true;
		if(placedShapes[^1]) placedShapes[^1].hasCollided = true;
		uiManager.EndGame();
	}

	void SelectRandomShape() {
		// TODO select a random shape based on difficulty
		PreviewShape shape = previewShapeQueue.Dequeue();
		currentPreviewShape = Instantiate(shape);
		previewShapeQueue.Enqueue(GetPreviewShape());
	}

	PreviewShape GetPreviewShape() {
		int len = previewShapes.Length - 1;
		difficulty += 1 / 5f;
		// when variance is 1 / x it can only go to x
		float variance = 1f / Mathf.Min(len, (int)difficulty + 3);
		return previewShapes[CMath.BinomialRandom(difficulty, variance, len)];
	}

	public List<Shape> GetLastShapes(int count) {
		int startIndex = placedShapes.Count - count;
		if(startIndex < 0) {
			startIndex = 0;
			count = placedShapes.Count;
		}
		return placedShapes.GetRange(startIndex, count);
	}

	internal void UpdateHeight(float height) {
		height *= 1.75f;
		if(maxHeight < height) maxHeight = Mathf.RoundToInt(height);
	}

	public void Restart() {
		GameCamera.follow = true;
		UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
	}
}
