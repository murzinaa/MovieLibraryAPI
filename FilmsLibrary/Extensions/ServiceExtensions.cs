using FilmsLibrary.Application;
using FilmsLibrary.Infrastructure;
using FilmsLibrary.Mappers;
using FilmsLibrary.SqlRepository;
using FilmsLibrary.SqlRepository.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FilmsLibrary.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IActorService, ActorService>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:SqlConnectionString"];
            services.AddDbContext<MovieContext>(options => options.UseSqlServer(connectionString));
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(mapperConfigurationExpression =>
                {
                    mapperConfigurationExpression.AddProfile<SqlToDomainProfile>();
                    mapperConfigurationExpression.AddProfile<DomainToContractProfile>();
                    mapperConfigurationExpression.AddProfile<ContractToDomainProfile>();
                    mapperConfigurationExpression.AddProfile<DomainToSqlProfile>();
                });
            // Array.Empty<Assembly>()
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "FilmsLibrary.xml");
            services.AddSwaggerGen(delegate (SwaggerGenOptions c)
            {
                c.SwaggerDoc("FilmsLibrary", new OpenApiInfo
                {
                    Title = "Films Library",
                    Version = "v1"
                });
                c.CustomSchemaIds(type => type.ToString());
                c.IncludeXmlComments(filePath);
            });
        }
    }
}
