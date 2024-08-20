using UnityEngine;

class RopePreview : PreviewShape
{
	[SerializeField] Rope rope;

	bool firstPlace = true;

	public override Shape Place() {
		Vector2 pos = transform.position;
		Shape closestShape = GetClosestShape(pos);
		if(closestShape is null) return null;
		if(firstPlace) {
			firstPlace = false;
			rope.SetPlacing(closestShape);
			return null;
		}
		rope.SetActive(closestShape);
		return base.Place();
	}



	Shape GetClosestShape(Vector2 pos) {
		Shape[] shapes = FindObjectsOfType<Shape>();
		if(shapes.Length == 0) return null;
		Shape closestShape = shapes[^1];
		foreach(Shape s in shapes) {
			if(s.gameObject == gameObject) continue;
			if(Vector2.Distance(pos, s.transform.position) < Vector2.Distance(pos, closestShape.transform.position)) {
				closestShape = s;
			}
		}
		return closestShape;
	}
}