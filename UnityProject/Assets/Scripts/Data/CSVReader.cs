using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
	public TextAsset CSVFile;
	public char CategoryDelimiter = ' ';

	private void Start()
	{
		GetRollingSums();
	}

	public List<float> GetRollingSums()
	{
		// Iterating through CSV
		List<float> rollingSums = new List<float>();

		IterateCSV((string colName, int rowNum, int colNum, string cell) =>
		{
			if (colName.ToLower().Trim().Equals("amount") && rowNum != 0)
			{
				float balanceChange;
				if (cell.Trim().Equals(""))
				{
					balanceChange = 0f;
				} else if (!float.TryParse(cell, out balanceChange))
				{
					throw new Exception("Invalid formatting for money value: " + cell);
				}

				if (rollingSums.Count <= 0)
				{
					rollingSums.Add(balanceChange);
				} else
				{
					float lastSum = rollingSums[rowNum - 2];
					rollingSums.Add(lastSum + balanceChange);
				}
			}
		});

		// Returning
		return rollingSums;
	}

	public Dictionary<string, float> GetCategorySpendings()
	{
		Dictionary<string, float> catSpendings = new Dictionary<string, float>();

		string lastCatCell = null;
		IterateCSV((string colName, int rowNum, int colNum, string cell) =>
		{
			if (rowNum != 0)
			{
				if (colName.ToLower().Trim().Equals("category"))
				{
					lastCatCell = cell;
				} else if (colName.ToLower().Trim().Equals("amount") && lastCatCell != null)
				{
					String[] cats = lastCatCell.Split(CategoryDelimiter);
					for (int i = 0; i < cats.Length; i++)
					{
						// Converting to float
						float numericAmount;
						if (cell.Trim().Equals(""))
						{
							numericAmount = 0f;
						} else if (!float.TryParse(cell, out numericAmount))
						{
							throw new Exception($"Unable to parse as numeric value: {cell}");
						}

						// Adding to spendings totals
						if (!catSpendings.ContainsKey(cats[i]))
						{
							catSpendings.Add(cats[i], numericAmount);
						}
						else
						{
							catSpendings[cats[i]] += numericAmount;
						}
					}
				}
			}
		});

		return catSpendings;
	}

	private void IterateCSV(Action<string, int, int, string> callback)
	{
		if (CSVFile == null)
		{
			throw new FileNotFoundException();
		}

		List<float> rollingSums = new List<float>();
		string[] lines = CSVFile.text.Split('\n');
		string[] headerRow = lines[0].Split(',');

		for (int rowNum = 0; rowNum < lines.Length; rowNum++)
		{
			// Looping through cells
			string row = lines[rowNum];
			string[] cells = row.Split(',');

			for (int colNum = 0; colNum < cells.Length; colNum++)
			{
				// Running code
				string cell = cells[colNum];
				callback(headerRow[colNum], rowNum, colNum, cell);
			}
		}
	}
}
