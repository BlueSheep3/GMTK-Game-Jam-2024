using UnityEngine;

class Shape : MonoBehaviour
{
	bool hasCollided = false;

	void OnCollisionEnter2D(Collision2D collision) {
		if(!hasCollided) {
			hasCollided = true;
			PlacingManager.Inst.UpdateHeight(transform.position.y);
		}
	}
}
