using System;
using System.Collections.Generic;
using UnityEngine;

public static class APIManager
{
	public static string TransactionsJSON;
	public static string CategoriesJSON;

	public static List<float> GetRollingSums()
	{
		string loadedJson = TransactionsJSON;
		if (loadedJson == null)
		{
			throw new Exception("There was no loaded JSON file");
		}
		List<float> rollingSums = new List<float>();

		// Parsing JSON file
		string wrappedJson = "{\"items\":" + loadedJson + "}";
		BankStatement statement = JsonUtility.FromJson<BankStatement>(wrappedJson);

		// Looping statement items
		for (int i = 0; i < statement.items.Length; i++)
		{
			if (rollingSums.Count <= 0)
			{
				rollingSums.Add(statement.items[i].GetAmount());
			}
			else
			{
				float lastSum = rollingSums[i - 1];
				rollingSums.Add(lastSum + statement.items[i].GetAmount());
			}
		}

		return rollingSums;
	}

	public static Dictionary<string, float> GetCategorySums()
	{
		string loadedJson = CategoriesJSON;
		if (loadedJson == null)
		{
			throw new Exception("There was no loaded JSON file");
		}

		// Creating dictionary
		Dictionary<string, float> catSums = new Dictionary<string, float>();

		// Getting API information
		string wrappedJson = "{\"items\":" + loadedJson + "}";
		Debug.Log(wrappedJson);
		Categories categories = JsonUtility.FromJson<Categories>(wrappedJson);
		Debug.Log(categories);
		Debug.Log(categories.items);

		// Looping through categories
		for (int i = 0; i < categories.items.Length; i++)
		{
			string catName = categories.items[i].category;
			float catVal;
			if (!float.TryParse(categories.items[i].total, out catVal))
			{
				throw new Exception($"Unable to convert the following value into float: {categories.items[i].total}");
			}
			catSums.Add(categories.items[i].category, catVal);
		}

		// Returning
		return catSums;
	}
}
