using System;
using UnityEngine;

[System.Serializable]
public class StatementItem
{
	public int id;
	public string date;
	public string amount;
	public string description;
	public string statement_id;

	public float GetAmount()
	{
		float retAmount;
		if (float.TryParse(amount, out retAmount))
		{
			return retAmount;
		} else
		{
			throw new Exception("The amount " + amount + " could not be parsed into a float value");
		}
	}
}

[System.Serializable]
public class BankStatement
{
	public StatementItem[] items;
}
