using Server.Services;

namespace InvestmentSimulator.Api.Workers;

public class InvestmentExpirationWorker : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<InvestmentExpirationWorker> _logger;

    public InvestmentExpirationWorker(IServiceProvider services, ILogger<InvestmentExpirationWorker> logger)
    {
        _services = services;
        _logger = logger;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                using var scope = _services.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IInvestmentService>();

              
                await service.ProcessExpiredInvestmentsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in worker");
            }
        }
    }
}