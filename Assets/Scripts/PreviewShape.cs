using UnityEngine;

class PreviewShape : MonoBehaviour
{
	[SerializeField] Shape placingShape;
	[SerializeField] SpriteRenderer sr;

	int collisionCount = 0;


	void OnTriggerEnter2D(Collider2D other) {
		if(collisionCount == 0) CanNoLongerBePlaced();
		collisionCount++;
	}

	void OnTriggerExit2D(Collider2D other) {
		collisionCount--;
		if(collisionCount == 0) CanNowBePlaced();
		#if UNITY_EDITOR
		if(collisionCount < 0)
			Debug.LogWarning($"CollisionCount is less than 0 (collisionCount == {collisionCount})");
		#endif
	}


	public bool CanBePlacedHere() {
		return collisionCount == 0;
	}

	public void Place() {
		Instantiate(placingShape, transform.position, Quaternion.identity);
	}


	void CanNoLongerBePlaced() {
		sr.color = new Color(1, 0.5f, 0.5f, 0.45f);
	}

	void CanNowBePlaced() {
		sr.color = new Color(1, 1, 1, 0.55f);
	}
}
