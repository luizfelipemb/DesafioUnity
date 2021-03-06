using System;
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
	[SerializeField] private WeatherManager weatherManager;
	public string apiKey = "e0ce379f73829744d877e5366961a1e1";

	public void GetRealWeather(int cityID) 
	{
		string uri = "api.openweathermap.org/data/2.5/weather?";
		uri += "id=" + cityID + "&appid=" + apiKey;
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
		Debug.Log("Json: "+json);
		WeatherStatus weather = new WeatherStatus ();
		try {
			dynamic obj = JObject.Parse (json);

			weather.weatherId = obj.weather[0].id;
			weather.name = obj.name;
			weather.main = obj.weather[0].main;
			weather.description = obj.weather[0].description;
			weather.temperature = obj.main.temp;
			weather.maxTemperature = obj.main.temp_max;
			weather.minTemperature = obj.main.temp_min;
			weather.pressure = obj.main.pressure;
			weather.windSpeed = obj.wind.speed;
		} catch (Exception e)
		{
			Debug.LogError(e.Message);
			Debug.LogError(e.StackTrace);
		}
		
		weatherManager.ChangeUI(
			weather.name,
			weather.ToCelsius(weather.temperature),
			weather.ToCelsius(weather.maxTemperature),
			weather.ToCelsius(weather.minTemperature),
			weather.main
			);
		Debug.Log("Weather is: "+ weather.main);
		Debug.Log ("Temp in °C: " + weather.ToCelsius(weather.temperature));
		Debug.Log ("Max Temp in °C: " + weather.ToCelsius(weather.maxTemperature));
		Debug.Log ("Min Temp in °C: " + weather.ToCelsius(weather.minTemperature));

		return weather;
	}
}