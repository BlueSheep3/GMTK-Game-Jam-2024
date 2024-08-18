using UnityEngine;

namespace Background
{
class Sky : MonoBehaviour
{
	[SerializeField] Material skyMaterial;


	void Update() {
		float height = Camera.main.transform.position.y;
		skyMaterial.SetFloat("_Height", height);
	}

	void OnDisable() {
		skyMaterial.SetFloat("_Height", 0);
	}
}
}
