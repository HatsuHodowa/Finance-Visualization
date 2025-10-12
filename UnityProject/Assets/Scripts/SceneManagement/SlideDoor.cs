using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    public float slideDistance;

    public bool buttonPressed;

    void Start()
    {
        slideDistance = 0.2f;
        buttonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - slideDistance), Time.deltaTime);
        }
    }

    public void OpenDoor()
    {
        Start();
    }
}
