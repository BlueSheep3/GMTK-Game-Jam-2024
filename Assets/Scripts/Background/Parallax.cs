using UnityEngine;

namespace Background
{
class Parallax : MonoBehaviour
{
	[Tooltip("Will perform the same movements as the Camera, except scaled by this")]
	[SerializeField] float movementMult = -1;
	[SerializeField] float scaleMult = -1;

	Vector2 startingPos;
	Vector3 startingScale;


	void Awake() {
		startingPos = transform.position;
		startingScale = transform.localScale;
	}

	void Update() {
		if(movementMult != -1) {
			Vector2 camPos = Camera.main.transform.position;
			transform.position = camPos * movementMult + startingPos;
		}
		if(scaleMult != -1) {
			float camSize = Camera.main.orthographicSize / 5f;
			transform.localScale = camSize * movementMult * startingScale;
		}
	}
}
}
