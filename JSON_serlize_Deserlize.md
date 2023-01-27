```cs
using System.Text.Json;

var weatherForecast = new WeatherForecast
{
    Date = DateTime.Parse("2019-08-01"),
    TemperatureCelsius = 25,
    Summary = "Hot"
};

string jsonString = JsonSerializer.Serialize(weatherForecast);

Console.WriteLine(jsonString);

var res = JsonSerializer.Deserialize<WeatherForecast>(jsonString);

Console.WriteLine(res?.Date);


public class WeatherForecast
{
    public DateTimeOffset Date { get; set; }
    public int TemperatureCelsius { get; set; }
    public string? Summary { get; set; }

}

```