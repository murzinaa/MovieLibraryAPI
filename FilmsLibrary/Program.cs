using NLog.Web;

namespace FilmsLibrary
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = CreateWebHostBuilder(args);
            var app = builder.Build();
            app.Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
