using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public float DepressDistance = 0.05f;

    private bool move = false;

    private GameObject door;

    private SlideDoor slideDoor;

    void Start()
    {
        door = GameObject.Find("Main Door");
        slideDoor = door.GetComponent<SlideDoor>();
    }

    void Update()
    {
        if (move)
        {
            transform.localPosition = new Vector3(0, -DepressDistance, 0);
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter()
    {
        move = true;
        slideDoor.OpenDoor();
        slideDoor.buttonPressed = true;
    }

    private void OnTriggerExit()
    {
        move = false;
    }
}
