﻿using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class RealWorldWeather : MonoBehaviour {

	/*
		In order to use this API, you need to register on the website.

		Source: https://openweathermap.org

		Request by city: api.openweathermap.org/data/2.5/weather?q={city id}&appid={your api key}
		Request by lat-long: api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={your api key}

		Api response docs: https://openweathermap.org/current
	*/

	public string apiKey = "e0ce379f73829744d877e5366961a1e1";

	public string city;
	public bool useLatLng = false;
	public string latitude;
	public string longitude;

	private void Start()
	{
		GetRealWeather ();
	}

	public void GetRealWeather () {
		string uri = "api.openweathermap.org/data/2.5/weather?";
		if (useLatLng) {
			uri += "lat=" + latitude + "&lon=" + longitude + "&appid=" + apiKey;
		} else {
			uri += "q=" + city + "&appid=" + apiKey;
		}
		StartCoroutine (GetWeatherCoroutine (uri));
	}

	IEnumerator GetWeatherCoroutine (string uri) {
		using (UnityWebRequest webRequest = UnityWebRequest.Get (uri)) {
			yield return webRequest.SendWebRequest ();
			if (webRequest.isNetworkError) {
				Debug.Log ("Web request error: " + webRequest.error);
			} else {
				ParseJson (webRequest.downloadHandler.text);
			}
		}
	}

	WeatherStatus ParseJson (string json) {
		WeatherStatus weather = new WeatherStatus ();
		try {
			dynamic obj = JObject.Parse (json);

			weather.weatherId = obj.weather[0].id;
			weather.main = obj.weather[0].main;
			weather.description = obj.weather[0].description;
			weather.temperature = obj.main.temp;
			weather.pressure = obj.main.pressure;
			weather.windSpeed = obj.wind.speed;
		} catch (Exception e) {
			Debug.Log (e.StackTrace);
		}
		
		Debug.Log ("Temp in °C: " + weather.Celsius ());
		Debug.Log ("Wind speed: " + weather.windSpeed);

		return weather;
	}

}