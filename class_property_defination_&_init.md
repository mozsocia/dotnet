```cs


var wf2 = new WeatherForecast();

wf2.Date = DateTime.Parse("2022-09-12");
wf2.TemperatureCelsius = 30;
wf2.Summary = "Warm";

// Second way of doing above things
var wf1 = new WeatherForecast
{
    Date = DateTime.Parse("2019-08-01"),
    TemperatureCelsius = 25,
    Summary = "Hot"
};

Console.WriteLine(wf2.Date);
Console.WriteLine(wf1.Date);



public class WeatherForecast
{
    public DateTimeOffset Date { get; set; }
    public int TemperatureCelsius { get; set; }
    public string? Summary { get; set; }

}

```
