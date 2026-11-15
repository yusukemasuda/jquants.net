# JQuants.Net

A modern .NET client library for the J-Quants API.

## Features

- Supports the J-Quants API
- Built on HttpClientFactory
- Automatic rate limiting
- Asynchronous API access
- Internal pagination handling
- Dependency Injection support
- Compatible with netstandard2.0

## Installation

Package Manager:

```powershell
Install-Package JQuants.Net
```

.NET CLI:

```bash
dotnet add package JQuants.Net
```

## Quick Start

Register JQuants.Net with Dependency Injection.

```csharp
services.AddJQuantsNet(options =>
{
    options.ApiKey = "<YOUR_API_KEY>";
});
```

Resolve the API client.

```csharp
var api = serviceProvider.GetRequiredService<IJQuantsApi>();
```

Retrieve listed issues.

```csharp
await foreach (var issue in api.Equities.GetListedIssuesAsync(new GetListedIssuesRequest(), CancellationToken.None))
{
    Console.WriteLine($"{issue.Code} {issue.CoName}");
}
```

## Source Code

GitHub repository:

https://github.com/yusukemasuda/jquants.net

## License

Licensed under the Apache License, Version 2.0.