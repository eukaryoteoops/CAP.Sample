using CAP.Sample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace CAP.Sample
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<ICapService, CapService>(); //enable service layer subscriber
            services.AddCap(o =>
            {
                o.DefaultGroup = "IAmQueue"; //QueueName
                o.UseDashboard();  // http://localhost:XXX/cap,
                o.UseInMemoryStorage();  // for testing, use durable db in prod
                o.UseRabbitMQ(config =>
                {
                    config.HostName = "127.0.0.1";
                    config.Port = AmqpTcpEndpoint.UseDefaultPort;
                    config.UserName = "guest";
                    config.Password = "guest";
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
