using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HttpClientFactoryService.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HttpClientFactoryService
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

            services.AddHttpClient();

            services.AddHttpClient<AccountClient>();

            services.AddHttpClient("AccountClient", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("AccountURL"));
                // Account API ContentType
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient("PayBillClient", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("PayBillURL"));
                // Account API ContentType
                c.DefaultRequestHeaders.Add("Accept", "application/xml");

            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("UserName"), 
                    Configuration.GetValue<string>("Password")),
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
