using Unity.VisualScripting;
using UnityEngine;

class Rope : Shape
{
	[SerializeField] BoxCollider2D boxCollider;
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] Sprite ropeTop;

	internal State state = State.inactive;
	internal Shape connectedObject;

	public enum State {
		inactive, placing, active
	}

	void Update() {
		if(state == State.placing) PlacingUpdate();
	}

	void PlacingUpdate() {
		float distance = Vector3.Distance(transform.position, connectedObject.transform.position);
		if(distance > 1f) {
			boxCollider.size = new(0.35f, distance - 1f);
			boxCollider.offset = new(0, -(distance - 1f) / 2);
		}
		spriteRenderer.size = new(1f, distance);
		SetRotation(connectedObject.transform.position);
	}

	void SetRotation(Vector2 position) {
		float angle = 90 + Mathf.Atan2(position.y - transform.position.y, position.x - transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	public void SetPlacing(Shape connectedObject) {
		this.connectedObject = connectedObject;
		spriteRenderer.sprite = ropeTop;
		PlacingUpdate();
		state = State.placing;
	}

	public void SetActive(Shape newConnectedObject) {
		state = State.active;
		Debug.Log("hifdsjk");
		Vector2 currentPos = transform.position;
		BuildRopeChain(currentPos, newConnectedObject);
		transform.position = newConnectedObject.transform.position;
		SetRotation(currentPos);
		float distance = Vector3.Distance(transform.position, currentPos);
		spriteRenderer.size = new(1f, distance);
		Destroy(gameObject.GetComponent<BoxCollider2D>());
		gameObject.AddComponent<HingeJoint2D>().connectedBody = newConnectedObject.GetComponent<Rigidbody2D>();
	}

	void BuildRopeChain(Vector2 start, Shape end) {
		GameObject parent = gameObject;
		float distance = Vector3.Distance(start, end.transform.position);
		float startAngle = 90 + Mathf.Atan2(end.transform.position.y - start.y, end.transform.position.x - start.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.Euler(0, 0, startAngle);
		float partDistance = 1f;
		while(distance > partDistance + 0.5f) {
			GameObject child = new("Rope");
			child.transform.parent = parent.transform;
			child.transform.position = start;
			child.AddComponent<BoxCollider2D>().size = new(0.35f, 0.35f);
			child.AddComponent<SpriteRenderer>().sprite = ropeTop;
			child.GetComponent<SpriteRenderer>().size = new(1f, partDistance);
			child.AddComponent<Rigidbody2D>().mass = 0.1f;
			child.AddComponent<HingeJoint2D>().connectedBody = parent.GetComponent<Rigidbody2D>();
			child.GetComponent<HingeJoint2D>().connectedAnchor = new(0, -partDistance);
			child.GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
			child.transform.rotation = rotation;
			distance -= partDistance;
			start = child.transform.position + (child.transform.right * partDistance);
			parent = child;
		}
		GameObject child2 = new("Rope");
		child2.transform.parent = parent.transform;
		child2.transform.position = start;
		SpriteRenderer sr2 = child2.AddComponent<SpriteRenderer>();
		sr2.sprite = ropeTop;
		sr2.size = new(1f, distance);
		sr2.drawMode = SpriteDrawMode.Tiled;
		child2.AddComponent<Rigidbody2D>().mass = 0.1f;
		HingeJoint2D hj = child2.AddComponent<HingeJoint2D>();
		hj.connectedBody = end.GetComponent<Rigidbody2D>();
		hj.connectedAnchor = new(0, -distance);
		hj.autoConfigureConnectedAnchor = false;
		child2.transform.rotation = rotation;
		hj = connectedObject.gameObject.AddComponent<HingeJoint2D>();
		hj.connectedBody = child2.GetComponent<Rigidbody2D>();
		hj.anchor = new(0, distance);
		hj.autoConfigureConnectedAnchor = false;
	}
}