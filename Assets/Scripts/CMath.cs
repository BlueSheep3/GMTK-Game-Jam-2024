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
		if(rect.xMin > other.xMin) rect.xMin = other.xMin;
		else if(rect.xMax < other.xMax) rect.xMax = other.xMax;
		if(rect.yMin > other.yMin) rect.yMin = other.yMin;
		else if(rect.yMax < other.yMax) rect.yMax = other.yMax;
		return rect;
	}

	public static Rect LimitMinSize(this Rect rect, Vector2 minSize){
		if(rect.width < minSize.x) {
			float midX = rect.xMin + rect.width / 2;
			rect.xMax = midX + minSize.x / 2;
			rect.xMin = midX - minSize.x / 2;
		}
		if(rect.height < minSize.y) {
			float midY = rect.yMin + rect.height / 2;
			rect.yMax = midY + minSize.y / 2;
			rect.yMin = midY - minSize.y / 2;
		}
		return rect;
	}

	public static bool Contains(this Rect rect, Rect other) {
		if(!rect.Contains(other.position)) return false;
		if(!rect.Contains(other.position + new Vector2(other.width, other.height))) return false;
		return true;
	}

	public static Rect WithScale(this Rect rect, Vector2 scale) {
		return new Rect(rect.position - scale / 2, scale);
	}

	public static Vector3 WithZ(this Vector2 vector, float value) {
		return new Vector3(vector.x, vector.y, value);
	}
}