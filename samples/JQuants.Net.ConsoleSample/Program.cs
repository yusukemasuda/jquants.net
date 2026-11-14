using JQuants.Net.Abstractions;
using JQuants.Net.Abstractions.Interfaces;
using JQuants.Net.Abstractions.Requests;
using JQuants.Net.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JQuants.Net.ConsoleSample;

internal static class Program
{
    private static async Task Main()
    {
        var configuration =
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

        var services =
            new ServiceCollection();

        services.AddJQuantsNet(options =>
        {
            options.ApiKey = configuration["JQuants:ApiKey"];
        });

        var serviceProvider = services.BuildServiceProvider();

        var api = serviceProvider.GetRequiredService<IJQuantsApi>();

        await foreach (var issue in api.Equities.GetListedIssuesAsync(new GetListedIssuesRequest(), CancellationToken.None))
        {
            Console.WriteLine($"{issue.Code} {issue.CoName}");
        }
    }
}