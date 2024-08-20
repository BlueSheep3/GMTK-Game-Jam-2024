using UnityEngine;

class SuperGluePreview : PreviewShape
{
	[SerializeField] bool strong = true;

	public override bool CanBePlacedHere() {
		return collisionCount != 0;
	}

	public override Shape Place() {
		// dont enable shape, because this shouldnt have physics
		if(!gameObject.TryGetComponent(out Shape shape)) Debug.LogError("no shape script on the object");
		if(!gameObject.TryGetComponent(out Rigidbody2D rb)) Debug.LogError("no rigidbody on the object");

		RaycastHit2D[] hits = new RaycastHit2D[32];
		// cant use GetContacts() because rb is kinematic
		int amount = rb.Cast(Vector2.up, hits, 0.1f);
		hits = hits[0..amount];
		Rigidbody2D prevBody = null;
		foreach(RaycastHit2D hit in hits) {
			if(hit.collider.isTrigger) continue;
			GameObject gluedObject = hit.rigidbody.gameObject;
			transform.parent = gluedObject.transform;
			if(prevBody) {
				FixedJoint2D joint = gluedObject.AddComponent<FixedJoint2D>();
				joint.connectedBody = prevBody;
				if(!strong) joint.breakForce = 400;
			}
			prevBody = hit.rigidbody;
		}

		Destroy(rb);
		Destroy(GetComponent<Collider2D>());
		// Shape is a required component by the PacingManager

		Invoke(nameof(CleanupAfterPlacing), 0.25f);
		Invoke(nameof(RemoveAudioSource), 1f);
		shape.PlayCollisionSound(1f);
		sr.color = new Color(1, 1, 1, 1);
		return shape;
	}

	void CleanupAfterPlacing() {
		PlacingManager.Inst.isPlacing = false;
		// removing this earlier stops the Invoke from happening
		Destroy(this);
		// the color can sometimes change after placing it
		sr.color = new Color(1, 1, 1, 1);
	}

	void RemoveAudioSource() {
		// removing this earlier stops the sound effect
		Destroy(GetComponent<AudioSource>());
	}

	protected override void IsNowColliding() {
		sr.color = new Color(1, 1, 1, 0.55f);
	}

	protected override void IsNoLongerColliding() {
		sr.color = new Color(1, 0.5f, 0.5f, 0.45f);
	}
}
