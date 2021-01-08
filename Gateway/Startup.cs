using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Gateway
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });
            services.AddHttpClient("Downstream", c => c.BaseAddress = new Uri("https://localhost:5003/"));

            services
                .AddGraphQLServer()
                .ConfigureSchemaServices(collection => collection.AddLogging())
                .AddType(new AnyType("Position"))
                .AddType(new AnyType("Geometry"))
                .AddErrorFilter<MyErrorFilter>()
                .ModifyRequestOptions(options => options.IncludeExceptionDetails = true)
                .AddRemoteSchema("Downstream");

            services.AddGraphQL("Downstream")
                .AddType(new AnyType("Position"))
                .AddType(new AnyType("Geometry"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/");
            });
        }
    }

    public class MyErrorFilter : IErrorFilter
    {
        private readonly ILogger<MyErrorFilter> _logger;

        public MyErrorFilter(ILogger<MyErrorFilter> logger)
        {
            _logger = logger;
        }

        public IError OnError(IError error)
        {
            _logger.LogError(error.Exception, error.Message);
            return error.Exception is ArgumentException ? error.WithMessage(error.Exception.Message) : error;
        }
    }
}
