using UnityEngine;
using UnityEngine.Events;

public class ButtonPress : MonoBehaviour
{
    public float DepressDistance = 0.05f;
    public UnityEvent OnPress = new UnityEvent();
    public float PressDebounceTime = 0.25f;
    public GameObject ButtonVisual;

    private bool move = false;
    private GameObject door;
    private SlideDoor slideDoor;
    private float lastStateChange = 0f;

    void Start()
    {
        door = GameObject.Find("Main Door");
        slideDoor = door.GetComponent<SlideDoor>();
    }

    void Update()
    {
        if (Time.time - lastStateChange > PressDebounceTime)
		{
			if (move)
			{
				ButtonVisual.transform.localPosition = new Vector3(0, -DepressDistance, 0);
			}
			else
			{
				ButtonVisual.transform.localPosition = new Vector3(0, 0, 0);
			}
			lastStateChange = Time.time;
		}
    }

    private void OnTriggerEnter(Collider c)
    {
        move = true;
        slideDoor.OpenDoor();
        slideDoor.buttonPressed = true;
    }

    private void OnTriggerExit()
    {
        move = false;
		OnPress.Invoke();
	}
}
