using UnityEngine;

namespace Background
{
class CloudSpawner : MonoBehaviour
{
	[SerializeField] Cloud cloudPrefab;

	int downtimer = 500;

	
	void Start() {
		for(int i = 0; i < 30; i++)
			SpawnCloud(true);
	}

	void FixedUpdate() {
		downtimer--;
		if(downtimer <= 0) {
			SpawnCloud(false);
			downtimer = Random.Range(400, 800);
		}
	}


	void SpawnCloud(bool randomX) {
		float x = randomX ? Random.Range(-80, 100) : 100;
		float y = GetYPos();
		Vector3 pos = new(x, y);
		Instantiate(cloudPrefab, pos, Quaternion.identity, transform.parent);
	}

	float GetYPos() {
		float rng = (Random.value + Random.value + Random.value) / 3f;
		return 50f * rng;
	}
}
}
