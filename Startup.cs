using gvsql.Tables;

namespace gvsql
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            ConfigureDatabase(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddSingleton(new DbContext("gvsql_sample"));

            services.AddTransient<Persons>();
            services.AddTransient<Users>();
            services.AddTransient<Customers>();
            services.AddTransient<Partners>();
            services.AddTransient<Sales>();
        }
    }
}