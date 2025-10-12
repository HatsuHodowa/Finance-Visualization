using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPileGrid : MonoBehaviour
{
	public Dictionary<string, float> categoryAmounts;
	public float PileDistance = 5f;
	public GameObject MoneyPile;

	private CSVReader csvReader;

	private void Start()
	{
		csvReader = GetComponent<CSVReader>();
		if (csvReader != null)
		{
			categoryAmounts = csvReader.GetCategorySpendings();
			GeneratePiles();
		}
	}

	private void GeneratePiles()
	{
		if (categoryAmounts.Keys.Count <= 0)
		{
			throw new System.Exception("Cannot generate piles for an empty categoryAmounts dictionary");
		}

		// Finding starting position
		int categoryCount = categoryAmounts.Keys.Count;
		int length = (int) MathF.Ceiling(MathF.Sqrt(categoryCount));
		float gridWidth = (length - 1) * PileDistance;
		float startPos = -gridWidth / 2;

		// Generating piles
		int index = 0;
		foreach (string category in categoryAmounts.Keys)
		{
			// Variables
			float amount = categoryAmounts[category];
			float xPos = startPos + (index % length) * PileDistance;
			float zPos = startPos * 2 + (index / length) * PileDistance;

			// Creating pile
			GameObject pileObj = Instantiate(MoneyPile);
			pileObj.transform.parent = transform;
			pileObj.transform.localPosition = new Vector3(xPos, 0, zPos);
			
			// Configuring pile
			MoneyPile pile = pileObj.GetComponent<MoneyPile>();
			Assert.NotNull(pile);
			pile.MoneyAmount = amount;
			pile.Category = category;
			pile.InitializeDisplay();

			// Increasing index
			index++;
		}
	}
}
