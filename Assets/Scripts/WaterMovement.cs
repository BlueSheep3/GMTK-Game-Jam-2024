using UnityEngine;

class WaterMovement : MonoBehaviour
{
	Vector2 startingPosition;

	void Start() {
		startingPosition = transform.position;
	}

	void FixedUpdate() {
		float sin = Mathf.Sin(Time.time / 50);
		Vector2 offset = new Vector2(0.2f * sin, sin) * 20;
		transform.position = startingPosition + offset;
	}
}