﻿using System.Net.Http.Headers;
using System.Text.Json;
using AIDevs.Gateway;
using AIDevs.Gateway.OpenAI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AIDevs
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                    config.AddUserSecrets<Program>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<MainService>();
                    services.AddTransient<Main>();
                    services.AddTransient<OpenAIClientFactory>();
                })
                .RunConsoleAsync();
        }
    }
}

