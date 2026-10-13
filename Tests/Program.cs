using Sudzekai.Hosting;
using Sudzekai.Hosting.Abstractions;
using Sudzekai.Hosting.Types.Exceptions;

namespace Tests
{
    internal class Program
    {
        private static int _failures = 0;
        private static int _passed = 0;

        // ---------- helper types ----------

        private interface ITestService
        {
            int Marker { get; }
        }

        private sealed class TestService : ITestService
        {
            public int Marker => 42;
        }

        private interface ITestHosted : IHostedService
        {
            bool Ran { get; }
        }

        private sealed class TestHostedImpl : ITestHosted
        {
            public bool Ran { get; private set; }
            public Task RunAsync() { Ran = true; return Task.CompletedTask; }
            public Task StopAsync() => Task.CompletedTask;
        }

        private sealed class DisposableTestService : IDisposable
        {
            public bool Disposed { get; private set; }
            public void Dispose() => Disposed = true;
        }

        private sealed class AsyncDisposableService : IAsyncDisposable
        {
            public bool Disposed { get; private set; }
            public ValueTask DisposeAsync() { Disposed = true; return ValueTask.CompletedTask; }
        }

        private sealed class DepService
        {
            public int Value { get; } = 7;
        }

        private sealed class NeedsDepService
        {
            public DepService Dep { get; }
            public NeedsDepService(DepService dep) => Dep = dep;
        }

        private sealed class DepFactory : IServiceFactory<NeedsDepService>
        {
            public NeedsDepService Create(IHostServiceProvider provider)
                => new NeedsDepService(provider.GetRequiredService<DepService>());
        }

        private sealed class RecordingHosted : IHostedService
        {
            public bool Ran { get; private set; }
            public bool Stopped { get; private set; }
            public Task RunAsync() { Ran = true; return Task.CompletedTask; }
            public Task StopAsync() { Stopped = true; return Task.CompletedTask; }
        }

        private sealed class SecondHosted : IHostedService
        {
            public bool Ran { get; private set; }
            public Task RunAsync() { Ran = true; return Task.CompletedTask; }
            public Task StopAsync() => Task.CompletedTask;
        }

        private sealed class RecordingHostedFactory : IServiceFactory<RecordingHosted>
        {
            public RecordingHosted Create(IHostServiceProvider provider) => new();
        }

        private sealed class DisposableHosted : IHostedService, IDisposable
        {
            public bool Disposed { get; private set; }
            public Task RunAsync() => Task.CompletedTask;
            public Task StopAsync() => Task.CompletedTask;
            public void Dispose() => Disposed = true;
        }

        private sealed class CountingService
        {
            public static int Instances;
            public CountingService() => Instances++;
        }

        // ---------- main ----------

        static async Task Main(string[] args)
        {
            // Smoke test of the demo-style host.
            var builder = HostBuilder.NewBuilder();
            builder.Services.AddHosted<TestHostedService>();
            builder.Services.AddTransiet<TestTransietService>();
            builder.Services.AddSingleton<TestSingletonService>();
            await using (var smoke = builder.Build())
            {
                await smoke.RunAsync();
                await smoke.StopAsync();
            }

            Console.WriteLine();
            Console.WriteLine("== host & provider ==");
            ConfigurationPropertyAvailable();
            ConfigurationEqualsResolvedService();
            GetRequiredServiceMissingThrows();
            GetRequiredServiceResolvesByBaseType();
            TryGetMissingReturnsFalse();
            TryGetPresentReturnsTrueAndValue();
            TryGetMatchesGetRequired();
            DisposeTwiceThrowsObjectDisposed();

            Console.WriteLine();
            Console.WriteLine("== singleton lifetimes ==");
            SingletonGenericReturnsSameInstance();
            SingletonInstancedReturnsPassedInstance();
            SingletonBaseImplResolves();
            SingletonFactoryFuncReturnsSameInstance();
            SingletonFactoryInterfaceResolvesDependency();

            Console.WriteLine();
            Console.WriteLine("== transient lifetimes ==");
            TransientGenericReturnsNewInstance();
            TransientBaseImplResolves();
            TransientFactoryReturnsNewInstance();
            TransientFactoryResolvesDependency();

            Console.WriteLine();
            Console.WriteLine("== hosted lifecycle ==");
            HostedGenericRunsAndStops();
            await HostedInstancedRunsAsync();
            await HostedBaseImplRunsAsync();
            await HostedFactoryRunsAndStopsAsync();
            await HostedFactoryInterfaceRunsAsync();
            await MultipleHostedAllRunAsync();
            await StopWithoutRunIsNoOpAsync();

            Console.WriteLine();
            Console.WriteLine("== duplicate registration ==");
            DuplicateRegistrationThrows();
            DuplicateAcrossOverloadsThrows();

            Console.WriteLine();
            Console.WriteLine("== factories require running host ==");
            FactoryWithoutRunThrows();
            await FactoryResolvesAfterRunAsync();

            Console.WriteLine();
            Console.WriteLine("== configuration ==");
            ConfigRootString();
            ConfigNestedString();
            ConfigNestedInteger();
            ConfigNestedDouble();
            ConfigNestedBoolTrue();
            ConfigNestedBoolFalse();
            ConfigArrayItem();
            ConfigNullValue();
            ConfigMissingKeyIsNull();

            Console.WriteLine();
            Console.WriteLine("== disposal ==");
            SingletonInstancedDisposed();
            SingletonGenericDisposed();
            SingletonAsyncDisposed();
            await HostedDisposedAsync();
            await TransientNotDisposedAsync();

            Console.WriteLine();
            Console.WriteLine($"PASSED {_passed} / {_passed + _failures}");
            if (_failures == 0)
            {
                Console.WriteLine("ALL CHECKS PASSED");
                return;
            }

            Console.WriteLine($"{_failures} CHECK(S) FAILED");
            Environment.ExitCode = 1;
        }

        // ===================== host & provider =====================

        private static void ConfigurationPropertyAvailable()
        {
            Check(nameof(ConfigurationPropertyAvailable), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                if (host.Configuration is null)
                    throw new Exception("Configuration is null");
            });
        }

        private static void ConfigurationEqualsResolvedService()
        {
            Check(nameof(ConfigurationEqualsResolvedService), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                var viaProperty = host.Configuration;
                var viaProvider = host.Services.GetRequiredService<IConfiguration>();
                if (!ReferenceEquals(viaProperty, viaProvider))
                    throw new Exception("Configuration property != resolved IConfiguration");
            });
        }

        private static void GetRequiredServiceMissingThrows()
        {
            Check(nameof(GetRequiredServiceMissingThrows), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                try
                {
                    host.Services.GetRequiredService<TestTransietService>();
                    throw new Exception("expected ServiceNotFoundException");
                }
                catch (ServiceNotFoundException) { /* expected */ }
            });
        }

        private static void GetRequiredServiceResolvesByBaseType()
        {
            Check(nameof(GetRequiredServiceResolvesByBaseType), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingleton<ITestService, TestService>();
                var host = hb.Build();
                var svc = host.Services.GetRequiredService<ITestService>();
                if (svc is not TestService || svc.Marker != 42)
                    throw new Exception("did not resolve to implementation");
            });
        }

        private static void TryGetMissingReturnsFalse()
        {
            Check(nameof(TryGetMissingReturnsFalse), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                if (host.Services.TryGetRequiredService<TestTransietService>(out var svc))
                    throw new Exception("TryGet returned true for unregistered type");
                if (svc is not null)
                    throw new Exception("out param should be null");
            });
        }

        private static void TryGetPresentReturnsTrueAndValue()
        {
            Check(nameof(TryGetPresentReturnsTrueAndValue), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingleton<TestSingletonService>();
                var host = hb.Build();
                if (!host.Services.TryGetRequiredService<TestSingletonService>(out var svc))
                    throw new Exception("TryGet returned false for registered type");
                if (svc is null)
                    throw new Exception("out param should not be null");
            });
        }

        private static void TryGetMatchesGetRequired()
        {
            Check(nameof(TryGetMatchesGetRequired), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingleton<TestSingletonService>();
                var host = hb.Build();
                var viaTry = host.Services.TryGetRequiredService<TestSingletonService>(out var svc)
                    ? svc : throw new Exception("TryGet failed");
                var viaGet = host.Services.GetRequiredService<TestSingletonService>();
                if (!ReferenceEquals(viaTry, viaGet))
                    throw new Exception("TryGet and Get returned different instances");
            });
        }

        private static void DisposeTwiceThrowsObjectDisposed()
        {
            Check(nameof(DisposeTwiceThrowsObjectDisposed), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                host.DisposeAsync().AsTask().GetAwaiter().GetResult();
                try
                {
                    host.DisposeAsync().AsTask().GetAwaiter().GetResult();
                    throw new Exception("expected ObjectDisposedException");
                }
                catch (ObjectDisposedException) { /* expected */ }
            });
        }

        // ===================== singleton =====================

        private static void SingletonGenericReturnsSameInstance()
        {
            Check(nameof(SingletonGenericReturnsSameInstance), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingleton<TestSingletonService>();
                var host = hb.Build();
                var a = host.Services.GetRequiredService<TestSingletonService>();
                var b = host.Services.GetRequiredService<TestSingletonService>();
                if (!ReferenceEquals(a, b))
                    throw new Exception("singleton returned different instances");
            });
        }

        private static void SingletonInstancedReturnsPassedInstance()
        {
            Check(nameof(SingletonInstancedReturnsPassedInstance), () =>
            {
                var instance = new TestSingletonService();
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingleton(instance);
                var host = hb.Build();
                var resolved = host.Services.GetRequiredService<TestSingletonService>();
                if (!ReferenceEquals(instance, resolved))
                    throw new Exception("instanced singleton did not return passed instance");
            });
        }

        private static void SingletonBaseImplResolves()
        {
            Check(nameof(SingletonBaseImplResolves), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingleton<ITestService, TestService>();
                var host = hb.Build();
                var svc = host.Services.GetRequiredService<ITestService>();
                if (svc is not TestService || svc.Marker != 42)
                    throw new Exception("base/impl singleton did not resolve");
            });
        }

        private static void SingletonFactoryFuncReturnsSameInstance()
        {
            Check(nameof(SingletonFactoryFuncReturnsSameInstance), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingletonFactory<TestSingletonService>(_ => new TestSingletonService());
                var host = hb.Build();
                host.RunAsync().GetAwaiter().GetResult();
                var a = host.Services.GetRequiredService<TestSingletonService>();
                var b = host.Services.GetRequiredService<TestSingletonService>();
                if (!ReferenceEquals(a, b))
                    throw new Exception("factory singleton returned different instances");
            });
        }

        private static void SingletonFactoryInterfaceResolvesDependency()
        {
            Check(nameof(SingletonFactoryInterfaceResolvesDependency), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingleton<DepService>();
                hb.Services.AddSingletonFactory<NeedsDepService>(new DepFactory());
                var host = hb.Build();
                host.RunAsync().GetAwaiter().GetResult();
                var svc = host.Services.GetRequiredService<NeedsDepService>();
                if (svc.Dep is null || svc.Dep.Value != 7)
                    throw new Exception("factory did not inject dependency");
            });
        }

        // ===================== transient =====================

        private static void TransientGenericReturnsNewInstance()
        {
            Check(nameof(TransientGenericReturnsNewInstance), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddTransiet<TestTransietService>();
                var host = hb.Build();
                var a = host.Services.GetRequiredService<TestTransietService>();
                var b = host.Services.GetRequiredService<TestTransietService>();
                if (ReferenceEquals(a, b))
                    throw new Exception("transient returned the same instance");
            });
        }

        private static void TransientBaseImplResolves()
        {
            Check(nameof(TransientBaseImplResolves), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddTransiet<ITestService, TestService>();
                var host = hb.Build();
                var a = host.Services.GetRequiredService<ITestService>();
                var b = host.Services.GetRequiredService<ITestService>();
                if (a is not TestService || ReferenceEquals(a, b))
                    throw new Exception("transient base/impl did not resolve to new instances");
            });
        }

        private static void TransientFactoryReturnsNewInstance()
        {
            Check(nameof(TransientFactoryReturnsNewInstance), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddTransietFactory<TestTransietService>(_ => new TestTransietService());
                var host = hb.Build();
                host.RunAsync().GetAwaiter().GetResult();
                var a = host.Services.GetRequiredService<TestTransietService>();
                var b = host.Services.GetRequiredService<TestTransietService>();
                if (ReferenceEquals(a, b))
                    throw new Exception("transient factory returned the same instance");
            });
        }

        private static void TransientFactoryResolvesDependency()
        {
            Check(nameof(TransientFactoryResolvesDependency), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingleton<DepService>();
                hb.Services.AddTransietFactory<NeedsDepService>(p => new NeedsDepService(p.GetRequiredService<DepService>()));
                var host = hb.Build();
                host.RunAsync().GetAwaiter().GetResult();
                var svc = host.Services.GetRequiredService<NeedsDepService>();
                if (svc.Dep is null || svc.Dep.Value != 7)
                    throw new Exception("transient factory did not inject dependency");
            });
        }

        // ===================== hosted =====================

        private static void HostedGenericRunsAndStops()
        {
            Check(nameof(HostedGenericRunsAndStops), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddHosted<RecordingHosted>();
                var host = hb.Build();
                host.RunAsync().GetAwaiter().GetResult();
                host.StopAsync().GetAwaiter().GetResult();
                var svc = host.Services.GetRequiredService<RecordingHosted>();
                if (!svc.Ran || !svc.Stopped)
                    throw new Exception("hosted generic did not run/stop");
            });
        }

        private static async Task HostedInstancedRunsAsync()
        {
            await CheckAsync(nameof(HostedInstancedRunsAsync), async () =>
            {
                var instance = new RecordingHosted();
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddHosted(instance);
                var host = hb.Build();
                await host.RunAsync();
                var resolved = host.Services.GetRequiredService<RecordingHosted>();
                if (!ReferenceEquals(instance, resolved) || !instance.Ran)
                    throw new Exception("hosted instanced did not run the passed instance");
            });
        }

        private static async Task HostedBaseImplRunsAsync()
        {
            await CheckAsync(nameof(HostedBaseImplRunsAsync), async () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddHosted<ITestHosted, TestHostedImpl>();
                var host = hb.Build();
                await host.RunAsync();
                var svc = host.Services.GetRequiredService<ITestHosted>();
                if (svc is not TestHostedImpl impl || !impl.Ran)
                    throw new Exception("hosted base/impl did not run");
            });
        }

        private static async Task HostedFactoryRunsAndStopsAsync()
        {
            await CheckAsync(nameof(HostedFactoryRunsAndStopsAsync), async () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddHostedFactory<RecordingHosted>(_ => new RecordingHosted());
                var host = hb.Build();
                await host.RunAsync();
                await host.StopAsync();
                var svc = host.Services.GetRequiredService<RecordingHosted>();
                if (!svc.Ran || !svc.Stopped)
                    throw new Exception("hosted factory did not run/stop");
            });
        }

        private static async Task HostedFactoryInterfaceRunsAsync()
        {
            await CheckAsync(nameof(HostedFactoryInterfaceRunsAsync), async () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddHostedFactory<RecordingHosted>(new RecordingHostedFactory());
                var host = hb.Build();
                await host.RunAsync();
                var svc = host.Services.GetRequiredService<RecordingHosted>();
                if (!svc.Ran)
                    throw new Exception("hosted IServiceFactory did not run");
            });
        }

        private static async Task MultipleHostedAllRunAsync()
        {
            await CheckAsync(nameof(MultipleHostedAllRunAsync), async () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddHosted<RecordingHosted>();
                hb.Services.AddHosted<SecondHosted>();
                var host = hb.Build();
                await host.RunAsync();
                var first = host.Services.GetRequiredService<RecordingHosted>();
                var second = host.Services.GetRequiredService<SecondHosted>();
                if (!first.Ran || !second.Ran)
                    throw new Exception("not all hosted services ran");
            });
        }

        private static async Task StopWithoutRunIsNoOpAsync()
        {
            await CheckAsync(nameof(StopWithoutRunIsNoOpAsync), async () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddHosted<RecordingHosted>();
                var host = hb.Build();
                await host.StopAsync();
                var svc = host.Services.GetRequiredService<RecordingHosted>();
                if (svc.Ran)
                    throw new Exception("hosted ran without RunAsync");
            });
        }

        // ===================== duplicate =====================

        private static void DuplicateRegistrationThrows()
        {
            Check(nameof(DuplicateRegistrationThrows), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingleton<TestSingletonService>();
                ExpectServiceAlreadyRegistered(() => hb.Services.AddSingleton<TestSingletonService>());
            });
        }

        private static void DuplicateAcrossOverloadsThrows()
        {
            Check(nameof(DuplicateAcrossOverloadsThrows), () =>
            {
                var hb = HostBuilder.NewBuilder();
                var instance = new TestSingletonService();
                hb.Services.AddSingleton<TestSingletonService>(instance);
                ExpectServiceAlreadyRegistered(() => hb.Services.AddSingleton<TestSingletonService>());
            });
        }

        // ===================== factories require running host =====================

        private static void FactoryWithoutRunThrows()
        {
            Check(nameof(FactoryWithoutRunThrows), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingletonFactory<TestSingletonService>(_ => new TestSingletonService());
                var host = hb.Build();
                try
                {
                    host.Services.GetRequiredService<TestSingletonService>();
                    throw new Exception("expected ServiceInstanceCreationException (provider not set)");
                }
                catch (ServiceInstanceCreationException) { /* expected */ }
            });
        }

        private static async Task FactoryResolvesAfterRunAsync()
        {
            await CheckAsync(nameof(FactoryResolvesAfterRunAsync), async () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingletonFactory<TestSingletonService>(_ => new TestSingletonService());
                var host = hb.Build();
                await host.RunAsync();
                var svc = host.Services.GetRequiredService<TestSingletonService>();
                if (svc is null)
                    throw new Exception("factory service not resolved after RunAsync");
            });
        }

        // ===================== configuration =====================

        private static void ConfigRootString()
        {
            Check(nameof(ConfigRootString), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                var v = host.Configuration["root"];
                if (v is not string s || s != "root-value")
                    throw new Exception($"root != root-value ({v?.ToString() ?? "null"})");
            });
        }

        private static void ConfigNestedString()
        {
            Check(nameof(ConfigNestedString), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                var v = host.Configuration["nested:name"];
                if (v is not string s || s != "deep-value")
                    throw new Exception("nested:name != deep-value");
            });
        }

        private static void ConfigNestedInteger()
        {
            Check(nameof(ConfigNestedInteger), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                var v = host.Configuration["nested:count"];
                bool ok = v switch
                {
                    long l => l == 3,
                    double d => d == 3,
                    _ => false
                };
                if (!ok)
                    throw new Exception($"nested:count != 3 ({v?.GetType().Name ?? "null"})");
            });
        }

        private static void ConfigNestedDouble()
        {
            Check(nameof(ConfigNestedDouble), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                var v = host.Configuration["nested:ratio"];
                if (v is not double d || d != 2.5)
                    throw new Exception($"nested:ratio != 2.5 ({v?.GetType().Name ?? "null"})");
            });
        }

        private static void ConfigNestedBoolTrue()
        {
            Check(nameof(ConfigNestedBoolTrue), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                if (host.Configuration["nested:flag"] is not true)
                    throw new Exception("nested:flag != true");
            });
        }

        private static void ConfigNestedBoolFalse()
        {
            Check(nameof(ConfigNestedBoolFalse), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                if (host.Configuration["nested:off"] is not false)
                    throw new Exception("nested:off != false");
            });
        }

        private static void ConfigArrayItem()
        {
            Check(nameof(ConfigArrayItem), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                var a = host.Configuration["items[0]"];
                var b = host.Configuration["items[1]"];
                if (a is not "a" || b is not "b")
                    throw new Exception("array items not flattened");
            });
        }

        private static void ConfigNullValue()
        {
            Check(nameof(ConfigNullValue), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                if (host.Configuration["nested:empty"] is not null)
                    throw new Exception("nested:empty should be null");
            });
        }

        private static void ConfigMissingKeyIsNull()
        {
            Check(nameof(ConfigMissingKeyIsNull), () =>
            {
                var host = HostBuilder.NewBuilder().Build();
                if (host.Configuration["does:not:exist"] is not null)
                    throw new Exception("expected null for missing key");
            });
        }

        // ===================== disposal =====================

        private static void SingletonInstancedDisposed()
        {
            Check(nameof(SingletonInstancedDisposed), () =>
            {
                var svc = new DisposableTestService();
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingleton(svc);
                var host = hb.Build();
                host.Services.GetRequiredService<DisposableTestService>();
                host.DisposeAsync().AsTask().GetAwaiter().GetResult();
                if (!svc.Disposed)
                    throw new Exception("instanced singleton not disposed");
            });
        }

        private static void SingletonGenericDisposed()
        {
            Check(nameof(SingletonGenericDisposed), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingleton<DisposableTestService>();
                var host = hb.Build();
                var svc = host.Services.GetRequiredService<DisposableTestService>();
                host.DisposeAsync().AsTask().GetAwaiter().GetResult();
                if (!svc.Disposed)
                    throw new Exception("generic singleton not disposed");
            });
        }

        private static void SingletonAsyncDisposed()
        {
            Check(nameof(SingletonAsyncDisposed), () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddSingleton<AsyncDisposableService>();
                var host = hb.Build();
                var svc = host.Services.GetRequiredService<AsyncDisposableService>();
                host.DisposeAsync().AsTask().GetAwaiter().GetResult();
                if (!svc.Disposed)
                    throw new Exception("IAsyncDisposable singleton not disposed");
            });
        }

        private static async Task HostedDisposedAsync()
        {
            await CheckAsync(nameof(HostedDisposedAsync), async () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddHosted<DisposableHosted>();
                var host = hb.Build();
                await host.RunAsync();
                var svc = host.Services.GetRequiredService<DisposableHosted>();
                await host.DisposeAsync();
                if (!svc.Disposed)
                    throw new Exception("hosted not disposed");
            });
        }

        private static async Task TransientNotDisposedAsync()
        {
            await CheckAsync(nameof(TransientNotDisposedAsync), async () =>
            {
                var hb = HostBuilder.NewBuilder();
                hb.Services.AddTransiet<DisposableTestService>();
                var host = hb.Build();
                var svc = host.Services.GetRequiredService<DisposableTestService>();
                await host.DisposeAsync();
                if (svc.Disposed)
                    throw new Exception("transient should not be disposed by host");
            });
        }

        // ===================== helpers =====================

        private static void ExpectServiceAlreadyRegistered(Action action)
        {
            try
            {
                action();
                throw new Exception("expected ServiceAlreadyRegisteredException");
            }
            catch (ServiceAlreadyRegisteredException) { /* expected */ }
        }

        private static void Check(string name, Action body)
        {
            try
            {
                body();
                _passed++;
                Console.WriteLine($"PASS  {name}");
            }
            catch (Exception ex)
            {
                _failures++;
                Console.WriteLine($"FAIL  {name}: {ex.Message}");
            }
        }

        private static async Task CheckAsync(string name, Func<Task> body)
        {
            try
            {
                await body();
                _passed++;
                Console.WriteLine($"PASS  {name}");
            }
            catch (Exception ex)
            {
                _failures++;
                Console.WriteLine($"FAIL  {name}: {ex.Message}");
            }
        }
    }
}