using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class Preview : MonoBehaviour
{
	[SerializeField] Image image;

	RectTransform rt;

	int timer = 50;
	List<Image> images = new();

	void Awake() {
		rt = GetComponent<RectTransform>();
	}

	void FixedUpdate() {
		if(timer >= 50) return;
		timer++;
		SetStuff();
	}

	void SetStuff() {
		Color c = images[^1].color;
		c.a = timer / 50f;
		images[^1].color = c;
		rt.anchoredPosition = new(-300, timer * 3 - 400);
	}

	internal void StartFunc() {
		for(int i = 0; i < 5; i++) {
			Image img = Instantiate(image, transform);
			RectTransform rt = img.GetComponent<RectTransform>();
			rt.anchoredPosition = new(0, i * -150);
			images.Add(img);
		}
		SetImages();
	}

	void SetImages() {
		List<(Sprite sprite, string name)> sprites = PlacingManager.Inst.GetPreviewSprites();
		rt.anchoredPosition = new(-300, -250);
		if(images.Count != sprites.Count) throw new System.Exception("Wrong number of images");
		for(int i = 0; i < images.Count; i++) {
			images[i].sprite = sprites[i].sprite;
			images[i].color = Color.white;
			string name = sprites[i].name;
			TMP_Text text = images[i].GetComponentInChildren<TMP_Text>();
			text.text = name;
		}
	}

	internal void GoNext() {
		SetImages();
		timer = 0;
		SetStuff();
	}
}