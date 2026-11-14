# jquants.net
A modern .NET client library for the J-Quants API with rate limiting and async streaming support.

## Features

- Supports the J-Quants API
- Built on `HttpClientFactory`
- Automatic rate limiting
- Asynchronous API access
- Internal pagination handling
- Dependency Injection support
- Compatible with `netstandard2.0`

## Installation

Install the NuGet package:

```powershell
Install-Package JQuants.Net
```

or

```bash
dotnet add package JQuants.Net
```

## Quick Start

### Configure Services

```csharp
var services = new ServiceCollection();

services.AddJQuantsNet(options =>
{
    options.ApiKey = "<YOUR_API_KEY>";
});

var serviceProvider = services.BuildServiceProvider();
```

### Resolve API Client

```csharp
var api = serviceProvider.GetRequiredService<IJQuantsApi>();
```

### Get Listed Issues

```csharp
var request = new GetListedIssuesRequest
{
    Date = new DateTime(2026, 1, 1)
};

await foreach (var issue in api.Equities.GetListedIssuesAsync(request, CancellationToken.None))
{
    Console.WriteLine($"{issue.Code} {issue.CoName}");
}
```

### Filter by Code

```csharp
var request = new GetListedIssuesRequest
{
    Code = "86970"
};

await foreach (var issue in api.Equities.GetListedIssuesAsync(request, CancellationToken.None))
{
    Console.WriteLine($"{issue.Code} {issue.CoName}");
}
```

## Usage

### Cancellation

```csharp
using var cancellationTokenSource = new CancellationTokenSource();

cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));

await foreach (var issue in api.Equities.GetListedIssuesAsync(new GetListedIssuesRequest(), cancellationTokenSource.Token))
{
    Console.WriteLine(issue.Code);
}
```

### Error Handling

```csharp
try
{
    await foreach (var issue in api.Equities.GetListedIssuesAsync(new GetListedIssuesRequest(), CancellationToken.None))
    {
        ....
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation was canceled.");
}
catch (HttpRequestException ex)
{
    Console.WriteLine(ex.Message);
}
```

### Rate Limiting

Requests are automatically throttled according to the configured rate limit.

No additional configuration is required.

## Supported Frameworks

```text
netstandard2.0
```

## Documentation

Detailed project documentation is available in the `docs` directory.

### Development

- [Branching Strategy](docs/branching-strategy.md)

## License

Licensed under the Apache License, Version 2.0.

See [LICENSE](LICENSE) and [NOTICE](NOTICE) for details.