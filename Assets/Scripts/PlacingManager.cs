using System.Collections.Generic;
using UnityEngine;
using static Savedata;

class PlacingManager : MonoBehaviour
{
	public static PlacingManager Inst;

	[SerializeField] PreviewShape[] previewShapes;

	internal static List<Shape> placedShapes = new();
	internal int maxHeight = 0;
	internal bool isPlacing = false;

	float difficulty = 0f;
	Queue<PreviewShape> previewShapeQueue = new();
	PreviewShape currentPreviewShape;

	void Awake() {
		if(Inst == null) Inst = this;
		else {
			Destroy(this);
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


	internal void EndGame() {
		// TODO open buttons to retry
		GameCamera.follow = false;
		Settings settings = Savedata.settings;
		if(settings.maxHeight < maxHeight) settings.maxHeight = maxHeight;
		if(settings.maxShapes < placedShapes.Count) settings.maxShapes = placedShapes.Count;
		Save(settings);
		Debug.Log("game ended");
		// Destroy(gameObject);
	}

	void SelectRandomShape() {
		// TODO select a random shape based on difficulty
		if(currentPreviewShape) Destroy(currentPreviewShape.gameObject);
		PreviewShape shape = previewShapeQueue.Dequeue();
		currentPreviewShape = Instantiate(shape);
		previewShapeQueue.Enqueue(GetPreviewShape());
	}

	PreviewShape GetPreviewShape() {
		int len = previewShapes.Length - 1;
		float variance = 1f / len;
		difficulty += 1 / 10f;
		return previewShapes[CMath.BinomialRandom(difficulty, variance, len)];
	}

	public static List<Shape> GetLastShapes(int count) {
		int startIndex = placedShapes.Count - count;
		if(startIndex < 0) {
			startIndex = 0;
			count = placedShapes.Count;
		}
		return placedShapes.GetRange(startIndex, count);
	}

	internal void UpdateHeight(float height) {
		if(maxHeight < height) maxHeight = Mathf.RoundToInt(height);
	}
}
