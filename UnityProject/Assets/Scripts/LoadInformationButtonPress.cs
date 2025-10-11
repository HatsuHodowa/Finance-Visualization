using UnityEngine;

public class LoadInformationButtonPress : MonoBehaviour
{
    private bool move = false;

    void Update()
    {
        if (move)
        {
            transform.position = new Vector3(0, -0.1f, 0) * Time.deltaTime;
            transform.position = new Vector3(0, 0.1f, 0) * Time.deltaTime;
        }
    }

    void OnCollisionEnter()
    {
        Debug.Log("I'm Colliding");
    }


    public void OnClick()
    {
        // Play button animation
        move = true;

    }
}
