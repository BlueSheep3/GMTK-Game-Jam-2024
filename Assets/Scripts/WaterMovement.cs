using UnityEngine;

class WaterMovement : MonoBehaviour
{
	Vector2 startingPosition;

	void Start() {
		startingPosition = transform.position;
	}

	void FixedUpdate() {
		float cos = Mathf.Cos(Time.time);
		float halfSin = Mathf.Sin(Time.time / 2);
		Vector2 offset = new Vector2(halfSin, -cos / 4);
		transform.position = startingPosition + offset;
	}
}