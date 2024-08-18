using UnityEngine;

namespace Background
{
class CloudSpawner : MonoBehaviour
{
	[SerializeField] Cloud cloudPrefab;

	int downtimer = 400;

	
	void Start() {
		for(int i = 0; i < 40; i++)
			SpawnCloud(true);
	}

	void FixedUpdate() {
		downtimer--;
		if(downtimer <= 0) {
			SpawnCloud(false);
			downtimer = Random.Range(350, 1000);
		}
	}


	void SpawnCloud(bool randomX) {
		float x = randomX ? Random.Range(-200, 250) : 250;
		float y = GetYPos();
		Vector3 pos = new(x, y);
		Instantiate(cloudPrefab, pos, Quaternion.identity, transform.parent);
	}

	float GetYPos() {
		float rng = (Random.value + Random.value + Random.value) / 3f;
		return 7f + 50f * rng;
	}
}
}
