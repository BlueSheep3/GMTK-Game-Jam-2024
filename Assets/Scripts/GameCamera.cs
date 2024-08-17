using System.Collections.Generic;
using UnityEngine;

class GameCamera : MonoBehaviour
{


	void FixedUpdate() {
		Queue<Shape> shapes = PlacingManager.recentShapes;
		Camera cam = Camera.main;
		Vector2 pos = cam.transform.position;
		Rect minSize = new(pos, new(0, 0));
		foreach(Shape shape in shapes) {
			Vector2 p = shape.transform.position;
			if(!minSize.Contains(p)) {
				minSize = minSize.Encapsulate(p);
			}
		}
		minSize = minSize.LimitMinSize(new Vector2(8, 5));
		cam.transform.position = minSize.center;
		cam.orthographicSize = Mathf.Max(minSize.size.x / 2, minSize.size.y) * 0.5f;
	}
}
