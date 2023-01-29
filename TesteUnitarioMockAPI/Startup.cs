
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TesteUnitarioMockAPI.Model;
using TesteUnitarioMockAPI.Services;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;
using System;

namespace TesteUnitarioMockAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.  
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TesteUnitarioMockAPI", Version = "v1" });
            });
            var connectionString = "server=localhost;user=root;password=;database=demeter";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

            services.AddDbContext<AppDbContext>(
           dbContextOptions => dbContextOptions
               .UseMySql(connectionString, serverVersion));
            
            services.AddScoped<IEmployeeRepository, EmployeeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TesteUnitarioMockAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
