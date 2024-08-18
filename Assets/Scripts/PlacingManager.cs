using System.Collections.Generic;
using UnityEngine;

class PlacingManager : MonoBehaviour
{
	public static PlacingManager Inst;

	[SerializeField] PreviewShape[] previewShapes;

	internal static List<Shape> placedShapes = new();
	internal float maxHeight = 0;
	PreviewShape currentPreviewShape = null;


	void Awake() {
		if(Inst == null) Inst = this;
		else {
			Destroy(this);
			Debug.LogError("shouldn't have 2 placing managers at once");
		}
	}

	void Start() {
		SelectRandomShape();
	}

	void Update() {
		Vector2 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		bool canPlace = true;
		if(Vector3.Distance(currentPreviewShape.transform.position, mousePos) > 3)
			canPlace = false; // very sudden movement, probably didnt want to place it

		// place before moving, so that the CanBePlacedHere check works
		if(canPlace && Input.GetMouseButtonDown(0) && currentPreviewShape.CanBePlacedHere()) {
			placedShapes.Add(currentPreviewShape.Place());
			SelectRandomShape();
		}

		currentPreviewShape.transform.position = mousePos;
	}


	void SelectRandomShape() {
		// TODO select a random shape based on difficulty
		if(currentPreviewShape) Destroy(currentPreviewShape.gameObject);
		PreviewShape shape = previewShapes[Random.Range(0, previewShapes.Length)];
		currentPreviewShape = Instantiate(shape);
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
		if(maxHeight < height) maxHeight = height;
	}
}
