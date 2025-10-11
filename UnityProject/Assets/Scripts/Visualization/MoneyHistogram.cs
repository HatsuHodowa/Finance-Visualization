using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHistogram : MonoBehaviour
{
	public List<float> MoneyValues;
	public GameObject MoneyTower;
	public float UpdateTime = 1f;
	public bool UpdateActive = false;

	private List<GameObject> moneyTowers = new List<GameObject>();
	private float lastUpdateTime = 0f;
	private CSVReader csvReader;

	private void Start()
	{
		csvReader = GetComponent<CSVReader>();
		MoneyValues = csvReader.GetRollingSums();
		UpdateHistogram();
	}

	private void Update()
	{
		float currentTime = Time.time;
		float timePassed = currentTime - lastUpdateTime;
		if (timePassed > UpdateTime && UpdateActive)
		{
			UpdateHistogram();
			lastUpdateTime = currentTime;
		}
	}

	private void UpdateHistogram()
	{
		ClearHistogram();
		GenerateHistogram();
	}

	private void ClearHistogram()
	{
		for (int i = 0; i < moneyTowers.Count; i++)
		{
			Destroy(moneyTowers[i]);
		}
	}

	private void GenerateHistogram()
	{
		// Calculating total length and starting position
		NumberTower towerSample = MoneyTower.transform.Find("MoneyTower").GetComponent<NumberTower>();
		float length = MoneyValues.Count * towerSample.XWidth;
		float startX = -length / 2;

		for (int i = 0; i < MoneyValues.Count; i++)
		{
			// Instantiating new column/tower
			float x = startX + towerSample.XWidth * i;
			GameObject newTower = Instantiate(MoneyTower);
			moneyTowers.Add(newTower);

			// Configuring
			newTower.transform.parent = transform;
			newTower.transform.position = new Vector3(x, 0f, 0f);
			NumberTower numTower = newTower.transform.Find("MoneyTower").GetComponent<NumberTower>();
			numTower.CurrentValue = MoneyValues[i];
		}
	}
}
