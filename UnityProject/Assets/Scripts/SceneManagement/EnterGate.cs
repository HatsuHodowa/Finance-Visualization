using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGate : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        SceneManager.LoadScene("StatementViewScene", LoadSceneMode.Single);
    }
}
