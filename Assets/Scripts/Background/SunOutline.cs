using UnityEngine;

namespace Background
{
class SunOutline : MonoBehaviour
{
	[SerializeField] float angularFrequency = 1;
	[SerializeField] float amplitude = 1;
	[SerializeField] float rotationSpeed = 1;

	float timer = 0;


	void Update() {
		timer += Time.deltaTime;
		float scale = 1 + amplitude * (1 + Mathf.Sin(timer * angularFrequency));
		transform.localScale = Vector3.one * scale;
		transform.rotation = Quaternion.Euler(0, 0, timer * rotationSpeed);
	}
}
}
