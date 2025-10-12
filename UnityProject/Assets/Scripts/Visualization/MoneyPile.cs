using NUnit.Framework.Constraints;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MoneyPile : MonoBehaviour
{
	public float HeightPerDollar = 0.01f;
	public GameObject Dollar;
	public string Category = "Account";
	public float MoneyAmount = 100;
	public float spawnDelay = 0.1f;

	private int currentAmount;
	private int spawnedAmount;
	private int spawnDollarSize;
	private float lastSpawn = 0f;
	private bool isSpawning = false;

	private TextMeshProUGUI billboardText;

	private void Start()
	{
		//InitializeDisplay();
	}

	private void Update()
	{
		if (isSpawning)
		{
			// Checking spawn delay
			float currentTime = Time.time;
			float timePassed = currentTime - lastSpawn;
			if (timePassed > spawnDelay)
			{
				// Spawning dollars
				if (currentAmount >= spawnedAmount + spawnDollarSize)
				{
					SpawnDollar(spawnDollarSize);
					spawnedAmount += spawnDollarSize;
					lastSpawn = currentTime;
				}
				else
				{
					EndMoneySpawn();
				}
			}
		}
	}

	public void InitializeDisplay()
	{
		// Setting up billboard
		string moneyString = MoneyAmount.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
		billboardText = transform.Find("Billboard").Find("Text").GetComponent<TextMeshProUGUI>();
		billboardText.text = $"{Category}\n{moneyString}";

		// Spawning money
		InitializeMoneySpawn(MoneyAmount);
	}

	private void InitializeMoneySpawn(float amount)
	{
		if (isSpawning)
		{
			throw new System.Exception("Cannot initialize money spawn while money spawn still active");
		}

		isSpawning = true;
		currentAmount = (int) MathF.Floor(amount);
		spawnDollarSize = GetDollarSize(amount);
		spawnedAmount = 0;
	}

	private void EndMoneySpawn()
	{
		isSpawning = false;
	}

	private void InstantMoneySpawn(float amount, int dollarSize)
	{
		int stackAmount = (int) Mathf.Floor(amount / dollarSize);

		for (int i = 0; i < stackAmount; i++)
		{
			SpawnDollar(dollarSize);
		}
	}

	private void InstantMoneySpawn(float amount)
	{
		InstantMoneySpawn(amount, GetDollarSize(amount));
	}

	private void SpawnDollar(int size)
	{
		GameObject clone = Instantiate(Dollar);
		clone.transform.parent = transform;
		clone.transform.localPosition = Vector3.zero;
		clone.transform.localScale = new Vector3(Dollar.transform.localScale.x, HeightPerDollar * size, Dollar.transform.localScale.z);
	}

	private int GetDollarSize(float amount)
	{
		if (amount <= 100)
		{
			return 1;
		}
		else if (amount <= 500)
		{
			return 5;
		}
		else if (amount <= 1000)
		{
			return 10;
		}
		else if (amount <= 2000)
		{
			return 20;
		}
		else if (amount <= 5000)
		{
			return 50;
		}
		else
		{
			return 100;
		}
	}
}
