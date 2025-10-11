using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APICaller : MonoBehaviour
{
	public string LoadedJSON;

	private string url = "http://ec2-54-152-191-164.compute-1.amazonaws.com/transactions";

	private void Start()
	{
		StartCoroutine(GetRequest());
	}

	public IEnumerator GetRequest()
	{
		UnityWebRequest request = UnityWebRequest.Get(url);
		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.Success)
		{
			Debug.Log(request.downloadHandler.text);
			LoadedJSON = request.downloadHandler.text;
		} else
		{
			Debug.LogError(request.error);
		}
	}

	public void SetURL(string url)
	{
		this.url = url;
	}
}
