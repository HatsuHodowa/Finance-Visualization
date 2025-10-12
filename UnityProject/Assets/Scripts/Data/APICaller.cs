using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Networking;

public class APICaller : MonoBehaviour
{
	public int statementId = 2;

	private string transactionsUrl = "https://financialreality.photo/api/statements/{0}/transactions";
	private string categoriesUrl = "https://financialreality.photo/api/statements/{0}/categories";

	private void Start()
	{
		FormatURLs();
	}

	public void InitializeRequests()
	{
		StartCoroutine(GetRequests());
	}

	private void FormatURLs()
	{
		transactionsUrl = String.Format(transactionsUrl, statementId.ToString());
		categoriesUrl = String.Format(categoriesUrl, statementId.ToString());
	}

	public IEnumerator GetRequests()
	{
		// Requesting transactions
		UnityWebRequest transacRequest = UnityWebRequest.Get(transactionsUrl);
		yield return transacRequest.SendWebRequest();

		if (transacRequest.result == UnityWebRequest.Result.Success)
		{
			APIManager.TransactionsJSON = transacRequest.downloadHandler.text;
			Debug.Log(APIManager.TransactionsJSON);
		} else
		{
			Debug.LogError(transacRequest.error);
		}

		// Requesting categories
		UnityWebRequest catRequest = UnityWebRequest.Get(categoriesUrl);
		yield return catRequest.SendWebRequest();

		if (catRequest.result == UnityWebRequest.Result.Success)
		{
			APIManager.CategoriesJSON = catRequest.downloadHandler.text;
			Debug.Log(APIManager.CategoriesJSON);
		} else
		{
			Debug.LogError(catRequest.error);
		}
	}
}
