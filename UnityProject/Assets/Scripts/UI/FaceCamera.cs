using UnityEngine;

public class FaceCamera : MonoBehaviour
{
	public Transform Camera;

	private void Update()
	{
		transform.rotation = Quaternion.LookRotation(-(Camera.position - transform.position).normalized);
	}
}
