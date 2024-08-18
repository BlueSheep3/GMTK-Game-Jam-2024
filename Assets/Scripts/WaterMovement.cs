using UnityEngine;

class WaterMovement : MonoBehaviour
{
	[SerializeField] float speed = 1.0f;
	[SerializeField] float offset = 0f;
	[SerializeField] float amplitude = 1.0f;
	[SerializeField] float width = 1.0f;

	Vector2 startingPosition;

	void Start() {
		startingPosition = transform.position;
	}

	void FixedUpdate() {
		float time = Time.time * speed + offset;
		float cos = Mathf.Cos(time) * amplitude;
		float halfSin = Mathf.Sin(time / 2) * width;
		Vector2 offsetv2 = new(halfSin, -cos / 4);
		transform.position = startingPosition + offsetv2;
	}
}