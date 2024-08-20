using UnityEngine;

class WaterSplash : MonoBehaviour
{
	[SerializeField] AudioClip[] sounds;
	[SerializeField] AudioSource audioSource;

	void Awake() {
		if(Scenes.Current() == Scenes.Id.MainMenu) return;
		audioSource.clip = sounds[Random.Range(0, sounds.Length)];
		audioSource.Play();
	}

	// called from the animation
	public void DestroySelf() {
		Destroy(gameObject);
	}
}
