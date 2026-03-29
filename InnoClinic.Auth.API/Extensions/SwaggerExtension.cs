namespace InnoClinic.Auth.API.Extensions
{
    public static class SwaggerExtension
    {
        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;

        }
    }
}
