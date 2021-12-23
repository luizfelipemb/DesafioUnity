
// Conditions explained: https://openweathermap.org/weather-conditions


public class WeatherStatus {
	public int weatherId;
	public string name;
	public string main;
	public string description;
	public float temperature; // in kelvin
	public float maxTemperature;
	public float minTemperature;
	public float pressure;
	public float windSpeed;

	public float ToCelsius (float temp) {
		return temp - 273.15f;
	}

	public float ToFahrenheit (float temp) {
		return ToCelsius(temp) * 9.0f / 5.0f + 32.0f;
	}
}