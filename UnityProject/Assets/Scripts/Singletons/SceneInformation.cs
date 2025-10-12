using UnityEngine;

public static class SceneInformation
{
	public static Transform LastLobbyDoor;
	public static float DoorOffset = 0.5f;
	public static bool InLobby = true;
	public static Vector3 LobbyLoadPosition = new Vector3();

	private static string lobbyScene = "LobbyScene";

	public static void OnDoorEnter(EnterGate enterDoor)
	{
		if (enterDoor.SceneName == lobbyScene)
		{
			InLobby = true;
		}
		else
		{
			GameObject enterDoorObj = enterDoor.transform.parent.gameObject;
			LobbyLoadPosition = enterDoorObj.transform.position - enterDoorObj.transform.forward * DoorOffset;
			InLobby = false;
		}
	}

}