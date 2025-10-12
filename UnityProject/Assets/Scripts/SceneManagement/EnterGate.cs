using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGate : MonoBehaviour
{
    public string SceneName;
    private void OnTriggerEnter()
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        SceneInformation.OnDoorEnter(this);
    }
}
