using UnityEngine;

public class NumberTower : MonoBehaviour
{
	public float heightPerDollar = 0.05f;
	public float currentValue = 50f;
	public float xWidth = 0.4f;
	public float zWidth = 0.2f;

	private void Update()
	{
		UpdateHeight();
	}

	private void UpdateHeight()
	{
		float height = heightPerDollar * currentValue;
		transform.localScale = new Vector3(xWidth, height, zWidth);
		transform.localPosition = new Vector3(0f, height / 2, 0f);
	}
}
