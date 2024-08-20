using UnityEngine;

class Shape : MonoBehaviour
{
	[SerializeField] protected Rigidbody2D rb;
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip clip;

	internal bool hasCollided = false;
	internal bool enabeled = false;


	internal virtual void OnPlacedShape() {}

	void OnCollisionEnter2D(Collision2D collision) {
		if(!enabeled) return;
		OnCollision();
	}

	protected void OnCollision() {
		if(!hasCollided) {
			hasCollided = true;
			PlacingManager.Inst.isPlacing = false;
			PlacingManager.Inst.UpdateHeight(transform.position.y);
		}
		float vel = rb.velocity.magnitude;
		if(!audioSource.isPlaying && vel > 1f) {
			float volume = vel / 20f;
			PlayCollisionSound(volume);
		}
	}

	public void PlayCollisionSound(float volume) {
		if(clip == null) return;
		volume = Mathf.Sqrt(volume);
		volume = Mathf.Clamp(volume, 0.1f, 1f);
		audioSource.pitch = Random.Range(0.8f, 1.1f);
		audioSource.volume = volume;
		audioSource.clip = clip;
		audioSource.Play();
	}

	protected void OnTriggerEnter2D(Collider2D other) {
		if(!enabeled) return;
		if(other.TryGetComponent(out WaterTrigger wt)) {
			wt.SpawnSplash(transform.position.x, rb.velocity.magnitude, rb.mass);
			if(GameCamera.follow) PlacingManager.Inst.EndGame();
		}
	}
}
