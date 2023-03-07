using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCli.Commands;
using MyCli.Services;
using System.CommandLine;

static void ConfigureServices(IServiceCollection services)
{
    // build config
    var configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .Build();

    // settings
    services.Configure<FakeWeatherServiceSettings>(configuration.GetSection("Weather"));

    // add commands:
    services.AddTransient<Command, CurrentCommand>();
    services.AddTransient<Command, ForecastCommand>();

    // add services:
    services.AddTransient<FakeWeatherService>();
}

// create service collection
var services = new ServiceCollection();
ConfigureServices(services);

// create service provider
using var serviceProvider = services.BuildServiceProvider();

// entry to run app
var commands = serviceProvider.GetServices<Command>();
var rootCommand = new RootCommand("Weather information using a fake weather service.");
commands.ToList().ForEach(command => rootCommand.AddCommand(command));

await rootCommand.InvokeAsync(args);
