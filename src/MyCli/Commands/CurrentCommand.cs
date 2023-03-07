using MyCli.Services;
using System.CommandLine;

namespace MyCli.Commands;

class CurrentCommand : Command
{
    private readonly FakeWeatherService _weather;

    public CurrentCommand(FakeWeatherService weather) : base("current", "Gets the current temperature.")
    {
        _weather = weather ?? throw new ArgumentNullException(nameof(weather));

        var cityOption = new Option<string>("--city", () => _weather.Settings.DefaultCity, "The city.");

        AddOption(cityOption);
        
        this.SetHandler(Execute, cityOption);
    }

    private async Task Execute(string city)
    {
        var report = await _weather.GetTemperature(city);
        Console.WriteLine(report);
    }
}
