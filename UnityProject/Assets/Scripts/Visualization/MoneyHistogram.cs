using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoneyHistogram : MonoBehaviour
{
	// General public variables
	public List<float> MoneyValues;
	public GameObject MoneyTower;
	public float UpdateTime = 1f;
	public bool UpdateActive = false;

	// Modules
	private APICaller apiCaller = null;
	private CSVReader csvReader = null;

	// Private variables
	private List<GameObject> moneyTowers = new List<GameObject>();
	private float lastUpdateTime = 0f;

	private void Start()
	{
		apiCaller = GetComponent<APICaller>();
		csvReader = GetComponent<CSVReader>();

		if (apiCaller != null)
		{
			StartCoroutine(InitiateAPICall());
		} else if (csvReader != null)
		{
			MoneyValues = csvReader.GetRollingSums();
			UpdateHistogram();
		}
	}

	private IEnumerator InitiateAPICall()
	{
		if (apiCaller == null)
		{
			throw new Exception("Attempt to initiate API caller without caller object attached to GameObject");
		}

		// Performing get request and updating data
		yield return apiCaller.GetRequest();
		MoneyValues = apiCaller.GetRollingSums();
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
		if (MoneyValues.Count == 0)
		{
			Debug.LogError("The histogram data is currently empty");
			return;
		}

		// Calculating total length and starting position
		NumberTower towerSample = MoneyTower.transform.Find("MoneyTower").GetComponent<NumberTower>();
		float length = MoneyValues.Count * towerSample.XWidth;
		float startX = -length / 2;
		float minValue = MoneyValues.Min();

		for (int i = 0; i < MoneyValues.Count; i++)
		{
			// Instantiating new column/tower
			float x = startX + towerSample.XWidth * i;
			GameObject newTower = Instantiate(MoneyTower);
			moneyTowers.Add(newTower);

			// Configuring
			newTower.transform.parent = transform;
			newTower.transform.localPosition = new Vector3(x, 0f, 0f);
			NumberTower numTower = newTower.transform.Find("MoneyTower").GetComponent<NumberTower>();
			numTower.CurrentValue = MoneyValues[i] - minValue;
		}
	}
}
