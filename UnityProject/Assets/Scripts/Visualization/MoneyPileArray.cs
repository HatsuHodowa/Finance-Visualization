using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPileArray : MonoBehaviour
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
		float length = categoryCount * PileDistance;
		float startXPos = -length / 2;

		// Generating piles
		int index = 0;
		foreach (string category in categoryAmounts.Keys)
		{
			// Variables
			float amount = categoryAmounts[category];
			float xPos = startXPos + index * PileDistance;

			// Creating pile
			GameObject pileObj = Instantiate(MoneyPile);
			pileObj.transform.parent = transform;
			pileObj.transform.localPosition = new Vector3(xPos, 0, 0);
			
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
