using FilmsLibrary.Extensions;
using FilmsLibrary.Middleware;
using FilmsLibrary.SqlRepository;
using Microsoft.EntityFrameworkCore;

namespace FilmsLibrary
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ConfigureSwagger();
            services.ConfigureAutoMapper();
            services.ConfigureSqlContext(_configuration);
            services.ConfigureApplicationServices(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<RequestMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseRouting();

            app.ConfigureSwagger();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MovieContext>();

                context.Database.Migrate();
            }

        }
    }
}
