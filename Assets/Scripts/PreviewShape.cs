using UnityEngine;

class PreviewShape : MonoBehaviour
{
	[SerializeField] protected SpriteRenderer sr;

	protected int collisionCount = 0;


	void Awake() {
		IsNoLongerColliding();
		if(gameObject.TryGetComponent(out Rigidbody2D rb)) {
			rb.bodyType = RigidbodyType2D.Kinematic;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(collisionCount == 0) IsNowColliding();
		collisionCount++;
	}

	void OnTriggerExit2D(Collider2D other) {
		collisionCount--;
		if(collisionCount == 0) IsNoLongerColliding();
		#if UNITY_EDITOR
		if(collisionCount < 0)
			Debug.LogWarning($"CollisionCount is less than 0 (collisionCount == {collisionCount})");
		#endif
	}


	internal virtual Sprite GetPreviewSprite() {
		return sr.sprite;
	}

	public virtual bool CanBePlacedHere() {
		return collisionCount == 0;
	}

	public virtual Shape Place() {
		if(!gameObject.TryGetComponent(out Shape shape)) Debug.LogError("no shape script on the object");
		shape.enabled = true;
		shape.enabeled = true;
		if(gameObject.TryGetComponent(out Rigidbody2D rb)) {
			rb.bodyType = RigidbodyType2D.Dynamic;
		}
		if(gameObject.TryGetComponent(out Collider2D col)) {
			col.isTrigger = false;
		}
		shape.PlayCollisionSound(0.5f);
		sr.color = new Color(1, 1, 1, 1);
		Destroy(this);
		return shape;
	}


	protected virtual void IsNowColliding() {
		sr.color = new Color(1, 0.5f, 0.5f, 0.45f);
	}

	protected virtual void IsNoLongerColliding() {
		sr.color = new Color(1, 1, 1, 0.55f);
	}
}
