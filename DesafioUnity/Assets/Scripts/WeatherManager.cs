using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private RealWorldWeather realWorldWeather;
    [SerializeField] private TextMeshProUGUI cityText;
    [SerializeField] private TextMeshProUGUI tempNowText;
    [SerializeField] private TextMeshProUGUI tempMaxText;
    [SerializeField] private TextMeshProUGUI tempMinText;

    public void ChangeUI(string city,float tempNow, float tempMax, float tempMin)
    {
        cityText.text = city;
        tempNowText.text = $"{tempNow}°C";
        tempMaxText.text = $"{tempMax}°C";
        tempMinText.text = $"{tempMin}°C";
    }

    public void Button(int cityID)
    {
        realWorldWeather.GetRealWeather(cityID);
    }
}
