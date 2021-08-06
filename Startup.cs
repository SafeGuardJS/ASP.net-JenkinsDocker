using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Net.Http;
using WebApiASP.Models;
using WebApiASP.Services;

namespace WebApiASP
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

            // requires using Microsoft.Extensions.Options
            services.Configure<MonitorstoreDatabaseSettings>(
                Configuration.GetSection(nameof(MonitorstoreDatabaseSettings)));

            services.AddSingleton<IMonitorstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MonitorstoreDatabaseSettings>>().Value);

            services.AddSingleton<MonitorService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiASP", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Map("/index", Index);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiASP v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void Index(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                HttpResponseMessage httpResponseMessage;

                HttpClient httpClient = new HttpClient();

                try
                {
                    httpResponseMessage = await httpClient.GetAsync("http://localhost:27017/");
                }
                catch
                {
                    await context.Response.WriteAsync("Page not found");
                }

                //httpResponseMessage.Content.ToString();

                //await context.Response.WriteAsync(httpResponseMessage.StatusCode.ToString());
            });
        }
    }
}
