using Microsoft.Extensions.Options;
using MyCli.Services;
using System.CommandLine;

namespace MyCli.Commands;

class ForecastCommand : Command
{
    private readonly FakeWeatherService _weather;

    public ForecastCommand(FakeWeatherService weather) : base("forecast", "Get the forecast. Almost always wrong.")
    {
        _weather = weather ?? throw new ArgumentNullException(nameof(weather));

        var cityOption = new Option<string>("--city", ()=> _weather.Settings.DefaultCity, "The city.");
        var daysOption = new Option<int>("--days", () => _weather.Settings.DefaultForecastDays, "Number of days.");

        AddOption(cityOption);
        AddOption(daysOption);

        this.SetHandler(Execute, cityOption, daysOption);
    }

    private async Task Execute(string city, int days)
    {
        var report = await _weather.Forecast(days, city);
        foreach (var item in report)
        {
            Console.WriteLine(item);
        }
    }
}
