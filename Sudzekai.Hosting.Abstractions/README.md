# Sudzekai.Hosting.Abstractions

> NuGet package: `Sudzekai.Hosting.Abstractions` — the contracts (interfaces and DTOs) of the Sudzekai Hosting library.

This package contains only **abstractions** and has **no implementation**. Reference it when you want to depend on the contracts without pulling in a concrete host, or when you are implementing a service / a factory that the host will invoke.

## What's inside

| Contract | Purpose |
| --- | --- |
| `IHost` | Controls process lifetime: `RunAsync()`, `StopAsync()`, exposes `Configuration` and `Services`; `IAsyncDisposable`. |
| `IHostBuilder` | Configures and builds an `IHost`; exposes `Services` and `ConfigurationBuilder`. |
| `IHostBuilderServiceCollection` | Registers singleton, transient and hosted services (generic, instance and factory overloads). |
| `IHostServiceProvider` | Resolves registered services: `GetRequiredService<T>()`, `TryGetRequiredService<T>()`. |
| `IHostedService` | A long-running background service started/stopped by the host. |
| `IServiceFactory<T>` | AOT-safe service factory that resolves dependencies from an `IHostServiceProvider`. |
| `IConfiguration` / `IConfigurationBuilder` | Reads configuration; configures how it is built. |
| `ConfigurationBuilderOptions` / `ConfigurationProviderType` | Settings for the configuration builder. |

## Usage

```csharp
using Sudzekai.Hosting.Abstractions;

// Implement a hosted service
class MyHostedService : IHostedService
{
    public Task RunAsync()  => Task.CompletedTask;
    public Task StopAsync() => Task.CompletedTask;
}

// Implement an AOT-safe factory
class MyFactory : IServiceFactory<MyService>
{
    public MyService Create(IHostServiceProvider provider)
        => new MyService(provider.GetRequiredService<IDependency>());
}
```

## License

MIT