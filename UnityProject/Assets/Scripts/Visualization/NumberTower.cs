using UnityEngine;

public class NumberTower : MonoBehaviour
{
	public float HeightPerDollar = 0.01f;
	public float CurrentValue = 50f;
	public float XWidth = 0.2f;
	public float ZWidth = 0.4f;
	public float BaseHeight = 0.1f;

	private void Update()
	{
		UpdateHeight();
	}

	private void UpdateHeight()
	{
		float height = HeightPerDollar * CurrentValue + BaseHeight;
		transform.localScale = new Vector3(XWidth, height, ZWidth);
		transform.localPosition = new Vector3(0f, height / 2, 0f);
	}
}
