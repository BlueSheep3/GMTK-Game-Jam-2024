using UnityEngine;

class PreviewShape : MonoBehaviour
{
	[SerializeField] SpriteRenderer sr;

	int collisionCount = 0;


	void Awake() {
		CanNowBePlaced();
	}

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

	public Shape Place() {
		if(!gameObject.TryGetComponent(out Shape shape)) Debug.LogError("no shape script on the object");
		shape.enabled = true;
		shape.enabeled = true;
		if(gameObject.TryGetComponent(out Rigidbody2D rb)) {
			rb.bodyType = RigidbodyType2D.Dynamic;
		}
		if(gameObject.TryGetComponent(out Collider2D col)) {
			col.isTrigger = false;
		}
		sr.color = new Color(1, 1, 1, 1);
		Destroy(this);
		return shape;
	}


	void CanNoLongerBePlaced() {
		sr.color = new Color(1, 0.5f, 0.5f, 0.45f);
	}

	void CanNowBePlaced() {
		sr.color = new Color(1, 1, 1, 0.55f);
	}
}
