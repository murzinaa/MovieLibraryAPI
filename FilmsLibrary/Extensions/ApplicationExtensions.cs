namespace FilmsLibrary.Extensions
{
    public static class ApplicationExtensions
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/FilmsLibrary/swagger.json", "Films Library");

            });
        }
    }
}
