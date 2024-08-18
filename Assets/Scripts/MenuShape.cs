using UnityEngine;

class MenuShape : MonoBehaviour
{
	[SerializeField] SpriteRenderer sr;

	Vector2 speed;
	Quaternion rotationalSpeed;

	internal void Init(Sprite sprite) {
		sr.sprite = sprite;
		transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
		speed = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-1.2f, -0.8f)) * Time.fixedDeltaTime;
		rotationalSpeed = Quaternion.Euler(0, 0, Random.Range(-1f, 1f) * Time.fixedDeltaTime);
	}

	void FixedUpdate() {
		transform.position += (Vector3)speed;
		transform.rotation *= rotationalSpeed;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.TryGetComponent(out WaterTrigger wt)) {
			wt.SpawnSplash(transform.position.x, speed.magnitude, 1);
		}
	}
}