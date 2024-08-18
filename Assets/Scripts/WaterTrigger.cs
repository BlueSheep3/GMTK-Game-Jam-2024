using UnityEngine;

class WaterTrigger : MonoBehaviour
{
	const float SPLASH_SIZE_MULT = 0.5f;

	[SerializeField] WaterSplash waterSplash;
	[SerializeField] float yOffset;


	public void SpawnSplash(float xPos, float speed, float mass) {
		WaterSplash ws = Instantiate(waterSplash, transform.parent);
		Vector3 pos = ws.transform.position;
		pos.x = xPos;
		pos.y += yOffset;
		ws.transform.position = pos;

		float power = Mathf.Abs(speed * mass);
		float size = SPLASH_SIZE_MULT * Mathf.Pow(1 + power, 1f / 3f);
		ws.transform.localScale = Vector3.one * size;
	}
}
