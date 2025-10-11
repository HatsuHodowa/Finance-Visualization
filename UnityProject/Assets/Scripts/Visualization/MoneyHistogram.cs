using System.Collections.Generic;
using UnityEngine;

public class MoneyHistogram : MonoBehaviour
{
	public List<float> MoneyValues;
	public GameObject MoneyTower;

	private void Start()
	{
		MoneyValues = new List<float>() { 0f, 5f, 20f, 10f, 15f, 200f, 500f, 1000f, 2f, 0f };
		UpdateHistogram();
	}

	private void Update()
	{
	}

	private void UpdateHistogram()
	{
		float length = MoneyValues.Count * MoneyTower.transform.localScale.x;
		float startX = -length / 2;
		NumberTower towerSample = MoneyTower.transform.Find("MoneyTower").GetComponent<NumberTower>();

		for (int i = 0; i < MoneyValues.Count; i++)
		{
			// Instantiating new column/tower
			float x = startX + towerSample.XWidth * i;
			GameObject newTower = Instantiate(MoneyTower);

			// Configuring
			newTower.transform.parent = transform;
			newTower.transform.position = new Vector3(x, 0f, 0f);
			NumberTower numTower = newTower.transform.Find("MoneyTower").GetComponent<NumberTower>();
			numTower.CurrentValue = MoneyValues[i];
		}
	}
}
