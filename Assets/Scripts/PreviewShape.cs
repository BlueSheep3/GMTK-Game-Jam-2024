using UnityEngine;

class PreviewShape : MonoBehaviour
{
	[SerializeField] Shape placingShape;


	public bool CanBePlacedHere() {
		// TODO
		return true;
	}

	public Shape Place() {
		throw new System.NotImplementedException();
	}
}
