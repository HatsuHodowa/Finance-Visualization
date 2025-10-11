using UnityEngine;

public class ButtonPress : MonoBehaviour
{
	public float DepressDistance = 0.05f;

	private bool move = false;

    void Update()
    {
        if (move)
        {
            transform.localPosition = new Vector3(0, -DepressDistance, 0);
        } else {
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }

	private void OnTriggerEnter()
	{
        move = true;
	}

	private void OnTriggerExit()
	{
        move = false;
	}


	public void OnClick()
    {
        // Play button animation
        move = true;

    }
}
