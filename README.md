# jquants.net
A modern .NET client library for the J-Quants API with rate limiting and async streaming support.

## Features

- Supports the J-Quants API.
- Built on `HttpClientFactory`.
- Automatic rate limiting.
- Asynchronous API access.
- Pagination is handled internally.
- Dependency Injection support.
- Compatible with `netstandard2.0`.

---

## Installation

Install the NuGet package:

```powershell
Install-Package JQuants.Net
```

or

```bash
dotnet add package JQuants.Net
```

---

## Configuration

Configure the API key during service registration.

```csharp
services.AddJQuantsNet(options =>
{
    options.ApiKey = "<YOUR_API_KEY>";
});
```

---

## Dependency Injection

```csharp
var services = new ServiceCollection();

services.AddJQuantsNet(options =>
{
    options.ApiKey = "<YOUR_API_KEY>";
});

var serviceProvider =
    services.BuildServiceProvider();
```

---

## Getting Started

Resolve `IJQuantsApi` from the service provider.

```csharp
var api =
    serviceProvider.GetRequiredService<IJQuantsApi>();
```

---

## Get Listed Issues

Retrieve listed issues from the equities master endpoint.

```csharp
var request =
    new GetListedIssuesRequest
    {
        Date = new DateTime(2026, 1, 1)
    };

await foreach (var issue in api.Equities.GetListedIssuesAsync(
                   request,
                   CancellationToken.None))
{
    Console.WriteLine(
        $"{issue.Code} {issue.CoName}");
}
```

---

## Filter by Code

```csharp
var request =
    new GetListedIssuesRequest
    {
        Code = "86970"
    };

await foreach (var issue in api.Equities.GetListedIssuesAsync(
                   request,
                   CancellationToken.None))
{
    Console.WriteLine(
        $"{issue.Code} {issue.CoName}");
}
```

---

## Cancellation

The library supports cancellation through `CancellationToken`.

```csharp
using var cancellationTokenSource =
    new CancellationTokenSource();

cancellationTokenSource.CancelAfter(
    TimeSpan.FromSeconds(5));

await foreach (var issue in api.Equities.GetListedIssuesAsync(
                   new GetListedIssuesRequest(),
                   cancellationTokenSource.Token))
{
    Console.WriteLine(issue.Code);
}
```

---

## Rate Limiting

Requests are automatically throttled based on the configured rate limit.

No additional configuration is required.

---

## Error Handling

```csharp
try
{
    await foreach (var issue in api.Equities.GetListedIssuesAsync(
                       new GetListedIssuesRequest(),
                       CancellationToken.None))
    {
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

---

## Supported Frameworks

```text
netstandard2.0
```

---

## License

Licensed under the Apache License, Version 2.0.

See the LICENSE file for details.
