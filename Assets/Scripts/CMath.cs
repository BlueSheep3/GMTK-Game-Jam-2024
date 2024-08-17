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

	public static Vector3 WithZ(this Vector2 vector, float value) {
		return new Vector3(vector.x, vector.y, value);
	}
}