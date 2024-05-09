namespace AIDevs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class MainService(
    ILogger<MainService> logger, 
    IHostApplicationLifetime appLifetime,
    Main main) : IHostedService
{
    private readonly ILogger<MainService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IHostApplicationLifetime _appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
    private readonly Main _main = main ?? throw new ArgumentNullException(nameof(main));

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                try
                {
                    await _main.RunAsync();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while running the main service.");
                }
                finally
                {
                    _appLifetime.StopApplication();
                }
            });
        });
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}