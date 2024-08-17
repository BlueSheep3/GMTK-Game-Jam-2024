using System.Collections.Generic;
using UnityEngine;

class GameCamera : MonoBehaviour
{
	void FixedUpdate() {
		Queue<Shape> shapes = PlacingManager.recentShapes;
		Camera cam = Camera.main;
		Vector2 pos = cam.transform.position;
		Rect targetRect = new(pos, new(0, 0));
		foreach(Shape shape in shapes) {
			Vector2 p = shape.transform.position;
			if(!targetRect.Contains(p)) {
				targetRect = targetRect.Encapsulate(p);
			}
		}
		Rect camRect = GetRect(cam);
		if(camRect.Contains(targetRect)) return;
		Vector2 targetSize = targetRect.size;
		targetSize = Vector2.Max(targetSize, new(5 * cam.aspect, 5));
		camRect = camRect.WithScale(targetSize);
		targetRect = camRect.MovementEncapsulate(targetRect);

		// TODO test and add extra to the sides

		Vector2 targetPosition = targetRect.center;
		cam.transform.position = Vector2.Lerp(cam.transform.position, targetPosition, 0.05f).WithZ(-10f);
		float targetScale = Mathf.Max(targetRect.size.x / cam.aspect, targetRect.size.y);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetScale, 0.05f);
	}

	Rect GetRect(Camera cam) {
		Vector2 size = GetSize(cam);
		return new Rect((Vector2)cam.transform.position - size / 2, size);
	}

	Vector2 GetSize(Camera cam) {
		return new Vector2(cam.orthographicSize * 2 * cam.aspect, cam.orthographicSize * 2);
	}
}
