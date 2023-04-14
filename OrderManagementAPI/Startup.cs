using OrderManagementAPI.Configuration;
using OrderManagementApplication.Mapper;
using OrdersManagement.Infrastructure;
using System.Reflection;
using System.Text.Json.Serialization;

namespace OrderManagementAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _environment;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase(Configuration, _environment);

            //services.AddAutoMapper(MapperConfig.RegisterMappings());
            services.AddAutoMapper(config => config.AddProfile<MappingProfile>());

            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            ServiceConfiguration.RegisterServices(services);

            services.AddControllers()
                .AddJsonOptions(x => {
                    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            services.AddEndpointsApiExplorer();
            services.ConfigureSwagger(_environment);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCustomizedSwagger(_environment);
        }
    }
}
