using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class GameCamera : MonoBehaviour
{
	Vector2 minSize;
	Vector2 buffer = new(2, 2);

	void Start() {
		minSize = GetSize(Camera.main) - buffer;
		Debug.Log(minSize);
	}

	void FixedUpdate() {
		Queue<Shape> shapes = PlacingManager.recentShapes;
		if(shapes.Count == 0) return;
		Camera cam = Camera.main;
		Rect targetRect = new(shapes.First().transform.position, new(0, 0));
		foreach(Shape shape in shapes) {
			Vector2 p = shape.transform.position;
			if(!targetRect.Contains(p)) {
				targetRect = targetRect.Encapsulate(p);
			}
		}

		Rect camRect = GetRect(cam).AddScale(-buffer);
		// if(camRect.Contains(targetRect)) return;
		Vector2 targetSize = targetRect.size;
		targetSize = Vector2.Max(targetSize, minSize);
		camRect = camRect.WithScale(targetSize);
		targetRect = camRect.MovementEncapsulate(targetRect);

		targetRect = targetRect.AddScale(buffer);

		Vector2 targetPosition = targetRect.center;
		cam.transform.position = Vector2.Lerp(cam.transform.position, targetPosition, 0.0125f).WithZ(-10f);
		float targetScale = Mathf.Max(targetRect.size.x / cam.aspect, targetRect.size.y);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetScale, 0.0125f);
	}

	Rect GetRect(Camera cam) {
		Vector2 size = GetSize(cam);
		return new Rect((Vector2)cam.transform.position - size / 2, size);
	}

	Vector2 GetSize(Camera cam) {
		return new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize);
	}
}
