using UnityEngine;

class MenuShapesSpawner : MonoBehaviour
{
	[SerializeField] MenuShape shapePrefab;
	[SerializeField] Sprite[] sprites;

	int timer = 0;

	void FixedUpdate() {
		if(timer++ < 10) return;
		int index = Random.Range(0, sprites.Length);
		if(index / (float)sprites.Length > Savedata.settings.maxHeight / 350f) return;
		Sprite sprite = sprites[index];
		MenuShape shape = Instantiate(shapePrefab, transform);
		shape.Init(sprite);
	}
}