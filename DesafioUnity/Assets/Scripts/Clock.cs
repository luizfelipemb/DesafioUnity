using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Clock : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI datetimeText;
	[SerializeField] private WorldTimeAPI worldTimeAPI;

	void Update ( ) 
	{
		if (worldTimeAPI.IsTimeLoaded) 
		{
			DateTime currentDateTime = worldTimeAPI.GetCurrentDateTime();
			datetimeText.text = currentDateTime.ToString("dd-MM-yyyy HH:mm:ss");
		}
	}
}
