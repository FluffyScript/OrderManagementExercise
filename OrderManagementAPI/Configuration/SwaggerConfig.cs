using Microsoft.OpenApi.Models;

namespace OrderManagementAPI.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                return services;
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OrderManagement",
                });
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "OrderManagement",
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomizedSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderManagement 1.0");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "OrderManagement 2.0");
                });
            }

            return app;
        }
    }
}
