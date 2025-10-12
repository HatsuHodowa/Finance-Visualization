using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionLoader : MonoBehaviour
{
	private void Start()
	{
		LoadPosition();
	}

	private void LoadPosition()
	{
		Scene activeScene = SceneManager.GetActiveScene();
		if (activeScene.name == "LobbyScene")
		{
			GameObject player = GameObject.FindWithTag("Player");
			Assert.NotNull(player, "Must have an object with Player tag in the scene to use PositionLoader");
			player.transform.position = SceneInformation.LobbyLoadPosition;
		}
	}
}
