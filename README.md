# Sudzekai Hosting

An **AOT-friendly application-hosting library for .NET**. It lets you build and run a *host*: register services (singleton, transient, hosted), wire up configuration, start and stop the process — in the spirit of `Microsoft.Extensions.Hosting`, but without the reflection-based activation that breaks NativeAOT.

The solution is organised into a set of small, focused projects published to NuGet as a **`Sudzekai.Hosting`** family of packages.

## Packages

| NuGet package | Project | Description |
| --- | --- | --- |
| **`Sudzekai.Hosting.Abstractions`** | `Sudzekai.WebOs.Hosting.Abstractions` | Public contracts: `IHost`, `IHostBuilder`, `IHostServiceProvider`, `IHostedService`, configuration and service-registration contracts, `IServiceFactory<T>`. |
| **`Sudzekai.Hosting`** | `Sudzekai.WebOs.Hosting` | The host implementation: `HostBuilder`, DI-style service registration, JSON configuration, AOT-safe factory activation. |
| `Sudzekai.Hosting.Types` | `Sudzekai.Hosting.Types` | Shared exception types (`ServiceNotFoundException`, `ServiceAlreadyRegisteredException`, `ServiceInstanceCreationException`). Required dependency of `Sudzekai.Hosting`. |

The rest of the projects (`Sudzekai.Hosting.Logging*`, `Sudzekai.Hosting.WebHost*`, `Sudzekai.WebOs.Hosting.Plugin*`, `Tests`) are supporting/companion and demonstration projects that are part of this repository.

## Getting started

```csharp
using Sudzekai.Hosting;

var builder = HostBuilder.NewBuilder();

builder.Services.AddHosted<MyHostedService>();      // runs when the host runs
builder.Services.AddSingleton<IService, MyService>(); // one shared instance
builder.Services.AddTransiet<TransientService>();     // new instance on every resolution

await using var host = builder.Build();

await host.RunAsync();
await host.StopAsync();
```

### Resolving services

```csharp
var service = host.Services.GetRequiredService<IService>();      // throws if not registered
var ok = host.Services.TryGetRequiredService<IService>(out var s); // safe lookup
```

### Configuration

JSON configuration is flattened with `:` for nested sections and `[i]` for arrays:

```csharp
var connection = host.Configuration["Database:ConnectionString"];
```

The default source is `appsettings.json` in the current working directory. It can be changed:

```csharp
builder.ConfigurationBuilder.Configure(o =>
{
    o.ConfigurationSource = "myapp.json";
});
```

## Service lifetimes

- **Singleton** — one instance for the whole host lifetime (`AddSingleton<T>()`), or a pre-created instance (`AddSingleton<T>(instance)`).
- **Transient** — a new instance on every resolution (`AddTransiet<T>()`).
- **Hosted** — a long-running service started on `RunAsync()` and stopped on `StopAsync()` (`AddHosted<T>()`).

Every lifetime also has a **factory** overload (`AddSingletonFactory`, `AddTransietFactory`, `AddHostedFactory`) — see below.

## AOT-safe activation via factories

Instead of resolving constructors through reflection, services are created by **compiled factories**. The concrete type and its constructor are known at the call site, so the trimmer keeps everything and NativeAOT works out of the box.

```csharp
// A factory that resolves its dependencies from the provider at creation time
class MyServiceFactory : IServiceFactory<MyService>
{
    public MyService Create(IHostServiceProvider provider)
        => new MyService(provider.GetRequiredService<IDependency>());
}

builder.Services.AddSingletonFactory(new MyServiceFactory());
```

A function delegate works too:

```csharp
builder.Services.AddSingletonFactory<MyService>(p => new MyService(p.GetRequiredService<IDependency>()));
```

> Factories receive the provider only after the host starts (`RunAsync`). Resolving a factory-created service before the host runs throws `ServiceInstanceCreationException`.

## Building & testing

```bash
dotnet build hosting.slnx -c Release
dotnet run --project Tests   # or run the built Tests.exe from its output directory
```

The `Tests` project is a self-contained verification suite (no test framework) that covers lifetimes, factory injection, hosted lifecycle, configuration parsing, duplicate-registration guards and disposal semantics.

## Packaging

Pack the NuGet packages (published to nuget.org in the future; nothing is pushed from this repository automatically):

```bash
dotnet pack Sudzekai.WebOs.Hosting/Sudzekai.Hosting.csproj -c Release
dotnet pack Sudzekai.WebOs.Hosting.Abstractions/Sudzekai.Hosting.Abstractions.csproj -c Release
```

## License

MIT