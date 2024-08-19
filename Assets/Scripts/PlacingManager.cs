using System.Collections.Generic;
using UnityEngine;

class PlacingManager : MonoBehaviour
{
	public static PlacingManager Inst;

	[SerializeField] PreviewShape[] previewShapes;
	[SerializeField] UiManager uiManager;
	[SerializeField] Preview previewManager;

	internal List<Shape> placedShapes = new();
	internal int maxHeight = 0;
	internal bool isPlacing = false;
	bool gameHasEnded = false;

	float difficulty = 4f;
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
		previewManager.StartFunc();
		SelectRandomShape();
	}

	void Update() {
		if(gameHasEnded) return;

		Vector2 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		bool canPlace = true;
		if(Vector3.Distance(currentPreviewShape.transform.position, mousePos) > 3)
			canPlace = false; // very sudden movement, probably didnt want to place it

		// place before moving, so that the CanBePlacedHere check works
		if(canPlace && !isPlacing && Input.GetMouseButtonDown(0) && currentPreviewShape.CanBePlacedHere()) {
			placedShapes.Add(currentPreviewShape.Place());
			OnPlaceShapeEffect();
			previewManager.GoNext();
			isPlacing = true;
			SelectRandomShape();
		}

		currentPreviewShape.transform.position = mousePos;
	}

	void OnDestroy() {
		Inst = null;
	}

	void OnPlaceShapeEffect() {
		foreach(Shape shape in placedShapes) {
			if(shape) shape.OnPlacedShape();
		}
	}

	internal void EndGame() {
		GameCamera.follow = false;
		gameHasEnded = true;
		if(placedShapes[^1]) placedShapes[^1].hasCollided = true;
		uiManager.EndGame();
		Destroy(currentPreviewShape.gameObject);
		currentPreviewShape = null;
	}

	void SelectRandomShape() {
		PreviewShape shape = previewShapeQueue.Dequeue();
		currentPreviewShape = Instantiate(shape);
		previewShapeQueue.Enqueue(GetPreviewShape());
	}

	PreviewShape GetPreviewShape() {
		difficulty += 1 / 5f;
		difficulty = Mathf.Clamp(difficulty, 0, previewShapes.Length);
		int index = Random.Range(0, (int)difficulty);
		return previewShapes[index];
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
		height += 1.2f;
		height *= 3f;
		if(maxHeight < height) maxHeight = Mathf.RoundToInt(height);
	}

	public void Restart() {
		GameCamera.follow = true;
		Scenes.Reload();
	}

	public void Quit() {
		GameCamera.follow = true;
		Scenes.Id.MainMenu.Load();
	}

	internal List<(Sprite, string)> GetPreviewSprites() {
		List<(Sprite, string)> sprites = new();
		foreach(PreviewShape shape in previewShapeQueue) {
			sprites.Add((shape.GetPreviewSprite(), shape.gameObject.name));
		}
		return sprites;
	}
}
