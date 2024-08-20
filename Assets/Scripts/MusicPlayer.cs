using UnityEngine;

class MusicPlayer : MonoBehaviour
{
	static MusicPlayer inst = null;

	[SerializeField] AudioSource audioSource;


	void Awake() {
		if(inst != null) {
			Destroy(gameObject);
			return;
		}
		inst = this;
		DontDestroyOnLoad(gameObject);
		audioSource.Play();
	}
}
