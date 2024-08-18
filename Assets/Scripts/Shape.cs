using UnityEngine;

class Shape : MonoBehaviour
{
	[SerializeField] Rigidbody2D rb;

	internal bool hasCollided = false;
	internal bool enabeled = false;


	void OnCollisionEnter2D(Collision2D collision) {
		if(!enabeled) return;
		if(!hasCollided) {
			hasCollided = true;
			PlacingManager.Inst.isPlacing = false;
			PlacingManager.Inst.UpdateHeight(transform.position.y);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(!enabeled) return;
		if(other.TryGetComponent(out WaterTrigger wt)) {
			wt.SpawnSplash(transform.position.x, rb.velocity.magnitude, rb.mass);
			if(GameCamera.follow) PlacingManager.Inst.EndGame();
		}
	}
}
