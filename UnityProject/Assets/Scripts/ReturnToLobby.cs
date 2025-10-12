using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToLobby : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        SceneManager.LoadScene("LobbyScene", LoadSceneMode.Single);
    }
}
