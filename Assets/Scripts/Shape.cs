using UnityEngine;

class Shape : MonoBehaviour
{
	[SerializeField] Rigidbody2D rb;

	bool hasCollided = false;


	void OnCollisionEnter2D(Collision2D collision) {
		if(!hasCollided) {
			hasCollided = true;
			PlacingManager.Inst.isPlacing = false;
			PlacingManager.Inst.UpdateHeight(transform.position.y);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.TryGetComponent(out WaterTrigger wt)) {
			wt.SpawnSplash(transform.position.x, rb.velocity.magnitude, rb.mass);
			PlacingManager.Inst?.EndGame();
		}
	}
}
