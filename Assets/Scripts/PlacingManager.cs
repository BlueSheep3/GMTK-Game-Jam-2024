using UnityEngine;

class PlacingManager : MonoBehaviour
{
	[SerializeField] PreviewShape[] previewShapes;
	[SerializeField] Shape[] shapes;
	
	PreviewShape currentPreviewShape = null;
}
