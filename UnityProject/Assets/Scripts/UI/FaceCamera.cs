using UnityEngine;

public class FaceCamera : MonoBehaviour
{
	public Transform FaceObj;

	private void Start()
	{
		if (FaceObj == null)
		{
			FaceObj = Camera.main.transform;
		}
	}

	private void Update()
	{
		transform.rotation = Quaternion.LookRotation(-(FaceObj.position - transform.position).normalized);
	}
}
