using UnityEngine;

namespace Background
{
class Cloud : MonoBehaviour
{
	[SerializeField] Sprite[] cloudSprites;
	[SerializeField] SpriteRenderer sr;
	[SerializeField] float parallaxMult = 0.5f;
	
	float speed;
	Vector2 pos;


	void Awake() {
		sr.sprite = cloudSprites[Random.Range(0, cloudSprites.Length)];
		speed = 0.5f * (1 + Random.value);
		pos = transform.position;
	}

	void Update() {
		pos += Vector2.left * speed * Time.deltaTime;
		if(pos.x < -100) Destroy(gameObject);

		Vector2 camPos = Camera.main.transform.position;
		transform.position = camPos * parallaxMult + pos;
	}
}
}
