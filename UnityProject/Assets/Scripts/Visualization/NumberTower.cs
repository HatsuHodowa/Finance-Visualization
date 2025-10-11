using UnityEngine;

public class NumberTower : MonoBehaviour
{
	public float HeightPerDollar = 0.05f;
	public float CurrentValue = 50f;
	public float XWidth = 0.2f;
	public float ZWidth = 0.4f;

	private void Update()
	{
		UpdateHeight();
	}

	private void UpdateHeight()
	{
		float height = HeightPerDollar * CurrentValue;
		transform.localScale = new Vector3(XWidth, height, ZWidth);
		transform.localPosition = new Vector3(0f, height / 2, 0f);
	}
}
