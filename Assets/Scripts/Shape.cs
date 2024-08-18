using UnityEngine;

class Shape : MonoBehaviour
{
	bool hasCollided = false;
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
		if(other.CompareTag("Water")) {
			PlacingManager.Inst?.EndGame();
		}
	}
}
