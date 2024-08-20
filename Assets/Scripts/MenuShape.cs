using UnityEngine;

class MenuShape : MonoBehaviour
{
	[SerializeField] SpriteRenderer sr;
	[SerializeField] Rigidbody2D rb;

	internal void Init(Sprite sprite) {
		sr.sprite = sprite;
		Camera cam = Camera.main;
		Vector2 camPos = cam.transform.position;
		Vector2 camSize = new(cam.orthographicSize * cam.aspect, cam.orthographicSize);
		transform.position = new Vector2(Random.Range(camPos.x - camSize.x, camPos.x + camSize.x), camPos.y + camSize.y + 2);
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