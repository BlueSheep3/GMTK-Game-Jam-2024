using UnityEngine;

class Pile : Shape
{
	bool hasSpiked = false;

	protected new void OnTriggerEnter2D(Collider2D other) {
		base.OnTriggerEnter2D(other);
		if(!enabeled) return;
		if(hasSpiked) return;
		if(!other.TryGetComponent(out Island isl)) {
			if(!other.TryGetComponent(out Shape wt)) return;
			if(other.TryGetComponent(out PreviewShape ps)) return;
		}
		hasSpiked = true;
		transform.position -= transform.up * 0.15f;
		gameObject.AddComponent<FixedJoint2D>().connectedBody = other.attachedRigidbody;
		OnCollision();
	}
}