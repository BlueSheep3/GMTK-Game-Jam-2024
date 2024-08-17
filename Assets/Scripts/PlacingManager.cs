using System.Collections.Generic;
using UnityEngine;

class PlacingManager : MonoBehaviour
{
	[SerializeField] PreviewShape[] previewShapes;

	public static Queue<Shape> recentShapes = new();
	
	PreviewShape currentPreviewShape = null;


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
			recentShapes.Enqueue(currentPreviewShape.Place());
			if(recentShapes.Count > 20) {
				recentShapes.Dequeue();
			}
		}

		currentPreviewShape.transform.position = mousePos;
	}


	void SelectRandomShape() {
		// TODO select a random shape based on difficulty
		PreviewShape shape = previewShapes[Random.Range(0, previewShapes.Length)];
		currentPreviewShape = Instantiate(shape);
	}
}
