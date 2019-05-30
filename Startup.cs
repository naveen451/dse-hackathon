using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Hackathon
{
    public class Startup
    {
        private static readonly JsonParser jsonParser = 
   new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static JsonParser JsonParser => jsonParser;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            /*app.Run(async (context) =>
            {
                WebhookRequest request;
                using (var reader = new StreamReader(context.Request.Body))
                {
                request = jsonParser.Parse<WebhookRequest>(reader);

                var response = new WebhookResponse
                {
                    FulfillmentText = "Hello from " + request.QueryResult.Intent.DisplayName
                };
                await context.Response.WriteAsync(response.ToString());
                }
            });*/
        }
    }
}
