namespace BackgroundTaskApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Hello");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
