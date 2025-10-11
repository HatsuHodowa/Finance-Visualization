using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
	public TextAsset CSVFile;

	private void Start()
	{
		GetRollingSums();
	}

	public List<float> GetRollingSums()
	{
		// Iterating through CSV
		List<float> rollingSums = new List<float>();

		IterateCSV((int rowNum, int colNum, string cell) =>
		{
			if (colNum == 3 && rowNum != 0)
			{
				float balanceChange;
				if (!float.TryParse(cell, out balanceChange))
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
				Debug.Log(rollingSums[rowNum - 1]);
			}
		});

		// Returning
		return rollingSums;
	}

	private void IterateCSV(Action<int, int, string> callback)
	{
		if (CSVFile == null)
		{
			throw new FileNotFoundException();
		}

		List<float> rollingSums = new List<float>();
		string[] lines = CSVFile.text.Split('\n');

		for (int rowNum = 0; rowNum < lines.Length; rowNum++)
		{
			// Looping through cells
			string row = lines[rowNum];
			string[] cells = row.Split(',');

			for (int colNum = 0; colNum < cells.Length; colNum++)
			{
				// Running code
				string cell = cells[colNum];
				callback(rowNum, colNum, cell);
			}
		}
	}
}
