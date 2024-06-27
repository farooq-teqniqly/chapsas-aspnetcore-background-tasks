namespace BackgroundTaskApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<HostOptions>(options =>
            {
                options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost;
            });

            builder.Services.AddHostedService<ExampleBackgroundService>();

            var app = builder.Build();

            app.Run();
        }
    }

    public class ExampleBackgroundService : BackgroundService
    {
        private readonly ILogger<ExampleBackgroundService> _logger;

        public ExampleBackgroundService(ILogger<ExampleBackgroundService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var counter = 0;

                while (!stoppingToken.IsCancellationRequested)
                {
                    if (counter == 10)
                    {
                        await StopAsync(stoppingToken);
                    }

                    _logger.LogInformation("Hello");
                    counter++;
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("Cancellation requested.");
            }
            finally
            {
                _logger.LogInformation("Bye");
            }
        }
    }
}
