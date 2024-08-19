using UnityEngine;

class MenuShape : MonoBehaviour
{
	[SerializeField] SpriteRenderer sr;
	[SerializeField] Rigidbody2D rb;

	internal void Init(Sprite sprite) {
		sr.sprite = sprite;
		transform.position = new Vector2(Random.Range(-20f, 10f), 10);
		transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
		rb.velocity =  new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-1.2f, -0.8f));
		rb.AddTorque(Random.Range(-20f, 20f));
	}

	void Update() {
		if(transform.position.y < -100) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.TryGetComponent(out WaterTrigger wt)) {
			wt.SpawnSplash(transform.position.x, rb.velocity.magnitude, 1);
		}
	}
}