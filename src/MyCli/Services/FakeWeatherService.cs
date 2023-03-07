using Microsoft.Extensions.Options;

namespace MyCli.Services;

public class FakeWeatherService
{
    public FakeWeatherService(IOptions<FakeWeatherServiceSettings> settings)
    {
        Settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public FakeWeatherServiceSettings Settings { get; }

    public Task<string> GetTemperature(string? city = null)
    {
        if (city == null) city = Settings.DefaultCity;

        var report = $"In {city} it is now {Random.Shared.Next(-20, 40)} degrees celcius.";
        return Task.FromResult(report);
    }

    public Task<string[]> Forecast(int days, string? city = null)
    {
        if (city == null) city = Settings.DefaultCity;

        var reports = new List<string>
        {
            $"Report for {city} for the next {days} days:"
        };

        for (var i = 0; i<days; i++)
        {
            var date = DateTime.Now.AddDays(i + 1).ToString("yyyy-MM-dd");
            var report = $"- {date}: {Random.Shared.Next(-20, 40),3} degrees celcius.";
            reports.Add(report);
        }

        return Task.FromResult(reports.ToArray());
    }
}
