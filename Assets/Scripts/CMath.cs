using UnityEngine;

static class CMath
{
	public static Rect Encapsulate(this Rect rect, Vector2 point) {
		if(point.x < rect.xMin)
			rect.xMin = point.x;
		else if(point.x > rect.xMax)
			rect.xMax = point.x;

		if(point.y < rect.yMin)
			rect.yMin = point.y;
		else if(point.y > rect.yMax)
			rect.yMax = point.y;

		return rect;
	}

	public static Rect MovementEncapsulate(this Rect rect, Rect other) {
		Vector2 size = rect.size;
		Vector2 otherSize = other.size;
		if(size.x < otherSize.x) throw new System.Exception("Can't encapsulate a bigger rect in a smaller one");
		if(size.y < otherSize.y) throw new System.Exception("Can't encapsulate a bigger rect in a smaller one");
		if(rect.xMin > other.xMin) rect.x = other.xMin;
		else if(rect.xMax < other.xMax) rect.x = other.xMax - rect.width;
		if(rect.yMin > other.yMin) rect.y = other.yMin;
		else if(rect.yMax < other.yMax) rect.y = other.yMax - rect.height;
		return rect;
	}

	public static Rect AdjustRatio(this Rect rect, float ratio) {
		if(rect.width < rect.height * ratio) {
			Vector2 s = new(rect.height * ratio, rect.height);
			return new Rect(rect.center - s / 2, s);
		}
		Vector2 scale = new(rect.width, rect.width / ratio);
		return new Rect(rect.center - scale / 2, scale);
	}

	public static Rect AddScale(this Rect rect, Vector2 scale) {
		return new Rect(rect.position - scale / 2, rect.size + scale);
	}

	public static bool Contains(this Rect rect, Rect other) {
		if(!rect.Contains(other.position)) return false;
		if(!rect.Contains(other.position + new Vector2(other.width, other.height))) return false;
		return true;
	}

	public static Rect WithScale(this Rect rect, Vector2 scale) {
		return new Rect(rect.center - scale / 2, scale);
	}

	public static Vector2 XY(this Vector3 vector) {
		return new Vector3(vector.x, vector.y);
	}

	public static Vector3 WithZ(this Vector2 vector, float value) {
		return new Vector3(vector.x, vector.y, value);
	}

	public static Vector3 SetZ(this Vector3 vector, float value) {
		return new Vector3(vector.x, vector.y, value);
	}

	public static int BinomialRandom(int n, float p) {
		int c = 0;
		for(int i = 0; i < n; i++) {
			if(Random.value < p) c++;
		}
		return c;
	}

	public static int BinomialRandom(float n, float p) {
		int c = 0;
		for(int i = 0; i < n; i++) {
			if(Random.value < p) c++;
		} if(Random.value < p * (n % 1)) c++;
		return c;
	}
}