using UnityEngine;

class PlacingManager : MonoBehaviour
{
	[SerializeField] PreviewShape[] previewShapes;
	
	PreviewShape currentPreviewShape = null;


	void Start() {
		SelectRandomShape();
	}

	void Update() {
		Vector2 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		currentPreviewShape.transform.position = mousePos;
		
		if(Input.GetMouseButtonDown(0) && currentPreviewShape.CanBePlacedHere()) {
			currentPreviewShape.Place();
		}
	}


	void SelectRandomShape() {
		// TODO select a random shape based on difficulty
		PreviewShape shape = previewShapes[Random.Range(0, previewShapes.Length)];
		currentPreviewShape = Instantiate(shape);
	}
}
