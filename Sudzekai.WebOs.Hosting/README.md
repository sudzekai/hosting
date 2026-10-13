# Sudzekai.Hosting

> NuGet package: `Sudzekai.Hosting` — the implementation package of the Sudzekai Hosting library.

An **AOT-friendly application-hosting library**. It builds and runs a *host*: register services (singleton, transient, hosted), wire up configuration, start and stop the process — without the reflection-based activation that breaks NativeAOT.

## Install

```
dotnet add package Sudzekai.Hosting
```

This package depends on `Sudzekai.Hosting.Abstractions` (the contracts) and `Sudzekai.Hosting.Types` (shared exceptions).

## Quick start

```csharp
using Sudzekai.Hosting;

var builder = HostBuilder.NewBuilder();

builder.Services.AddHosted<MyHostedService>();
builder.Services.AddSingleton<IService, MyService>();
builder.Services.AddTransiet<TransientService>();

await using var host = builder.Build();

await host.RunAsync();
await host.StopAsync();
```

## Resolving services

```csharp
var service = host.Services.GetRequiredService<IService>();        // throws if not registered
var ok = host.Services.TryGetRequiredService<IService>(out var s); // safe lookup
```

## Service lifetimes

| Method | Behaviour |
| --- | --- |
| `AddSingleton<T>()` / `AddSingleton<T>(instance)` | one shared instance for the whole host lifetime |
| `AddTransiet<T>()` | a new instance on every resolution |
| `AddHosted<T>()` / `AddHosted<T>(instance)` | started on `RunAsync()`, stopped on `StopAsync()` |

Base/implementation overloads are available for each lifetime, e.g. `AddSingleton<TBase, TImplementation>()`.

## AOT-safe activation via factories

Services can be created by **compiled factories** so constructor resolution stays static and NativeAOT keeps everything (no reflection, no `[DynamicallyAccessedMembers]` needed).

```csharp
class MyServiceFactory : IServiceFactory<MyService>
{
    public MyService Create(IHostServiceProvider provider)
        => new MyService(provider.GetRequiredService<IDependency>());
}

builder.Services.AddSingletonFactory(new MyServiceFactory());
// or with a delegate:
builder.Services.AddSingletonFactory<MyService>(p => new MyService(p.GetRequiredService<IDependency>()));
```

Factory overloads: `AddSingletonFactory`, `AddTransietFactory`, `AddHostedFactory`.

> Factories receive the provider only after the host starts (`RunAsync`). Resolving a factory-created service before the host runs throws `ServiceInstanceCreationException`.

## Configuration

JSON configuration is flattened with `:` for nested sections and `[i]` for arrays.

```csharp
var connection = host.Configuration["Database:ConnectionString"];
var item = host.Configuration["Items[0]"];
```

The default source is `appsettings.json` in the current working directory:

```csharp
builder.ConfigurationBuilder.Configure(o => o.ConfigurationSource = "myapp.json");
```

## Hosted services

A hosted service implements `IHostedService`:

```csharp
class MyHostedService : IHostedService
{
    public Task RunAsync()  { /* start */ return Task.CompletedTask; }
    public Task StopAsync() { /* stop  */ return Task.CompletedTask; }
}
```

## License

MIT