using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APICaller : MonoBehaviour
{
	public string LoadedJSON;

	private string url = "http://ec2-54-152-191-164.compute-1.amazonaws.com/api/transactions";

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
			LoadedJSON = request.downloadHandler.text;
		} else
		{
			Debug.LogError(request.error);
		}
	}

	public List<float> GetRollingSums()
	{
		if (LoadedJSON == null)
		{
			throw new Exception("There was no loaded JSON file");
		}
		List<float> rollingSums = new List<float>();

		// Parsing JSON file
		string wrappedJson = "{\"items\":" + LoadedJSON + "}";
		BankStatement statement = JsonUtility.FromJson<BankStatement>(wrappedJson);

		// Looping statement items
		for (int i = 0; i < statement.items.Length; i++)
		{
			if (rollingSums.Count <= 0)
			{
				rollingSums.Add(statement.items[i].GetAmount());
			} else
			{
				float lastSum = rollingSums[i - 1];
				rollingSums.Add(lastSum + statement.items[i].GetAmount());
			}
		}

		return rollingSums;
	}

	public void SetURL(string url)
	{
		this.url = url;
	}
}
