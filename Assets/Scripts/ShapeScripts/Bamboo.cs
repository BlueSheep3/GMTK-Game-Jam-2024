using UnityEngine;

class Bamboo : Shape
{
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] BoxCollider2D boxCollider;

	internal override void OnPlacedShape() {
		if(!enabeled) return;
		Vector2 size = spriteRenderer.size;
		size += new Vector2(0f, 0.1f);
		spriteRenderer.size = size;
		size = boxCollider.size;
		size += new Vector2(0f, 0.1f);
		boxCollider.size = size;
		rb.mass = size.x * size.y;
	}
}