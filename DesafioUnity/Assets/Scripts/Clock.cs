﻿using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Clock : MonoBehaviour {
	[SerializeField] TextMeshProUGUI datetimeText;

	void Update ( ) {
		if (WorldTimeAPI.Instance.IsTimeLoaded) 
		{
			DateTime currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
			datetimeText.text = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
		}
	}
}