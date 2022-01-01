using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private RealWorldWeather realWorldWeather;
    [SerializeField] private TextMeshProUGUI cityText;
    [SerializeField] private TextMeshProUGUI tempNowText;
    [SerializeField] private TextMeshProUGUI tempMaxText;
    [SerializeField] private TextMeshProUGUI tempMinText;
    [SerializeField] private List<WeatherObject> weatherObjList;

    public void ChangeUI(string city,float tempNow, float tempMax, float tempMin, string weatherName)
    {
        cityText.text = city;
        tempNowText.text = $"{tempNow}°C";
        tempMaxText.text = $"{tempMax}°C";
        tempMinText.text = $"{tempMin}°C";

        foreach (var weatherObj in weatherObjList)
        {
            if (weatherObj.Name == weatherName)
            {
                weatherObj.ImageGameObject.SetActive(true);
            }
            else
            {
                weatherObj.ImageGameObject.SetActive(false);
            }
        }
    }

    public void Button(int cityID)
    {
        realWorldWeather.GetRealWeather(cityID);
    }
}
