using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using System;

namespace Raymaker.HttpClientDotnetFramework
{
    public class Bootstrapper : IDisposable
    {
        public static IServiceProvider ServiceProvider;

        public Bootstrapper()
        {
            Init();
        }

        public void Init()
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureServices(ConfigureServices);
            var host = hostBuilder.Build();

            ServiceProvider = host.Services;
        }

        void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfig, Config>();
            services.AddSingleton<IManager, ManagerWithFactory>();
            services.AddTransient<ValidateHeaderHandler>();
            services.AddTransient<HmacAuthenticationHandler>();

            services.AddHttpClient(name: "GitHub", client =>
             {
                 client.BaseAddress = new Uri("https://api.github.com/");
                 client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                 client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryExample");
             })
                //.AddHttpMessageHandler<ValidateHeaderHandler>()
                .AddHttpMessageHandler<HmacAuthenticationHandler>()
                .AddTransientHttpErrorPolicy(x =>
                    x.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

            services.AddHttpClient(name: "GitHub2", client =>
            {
                client.BaseAddress = new Uri("https://api.github.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryExample");
            });
        }

        public void Dispose()
        {

        }
    }
}
