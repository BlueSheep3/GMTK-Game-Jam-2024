using UnityEngine;

class WaterSplash : MonoBehaviour
{
	// called from the animation
	public void DestroySelf() {
		Destroy(gameObject);
	}
}
