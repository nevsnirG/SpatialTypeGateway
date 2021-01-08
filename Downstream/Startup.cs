using HotChocolate;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Downstream
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

            services
                .AddRouting()
                .AddGraphQLServer()
                .AddSpatialTypes()
                .AddQueryType<Query>()
                .AddFiltering()
                .AddProjections()
                .AddSpatialFiltering()
                .AddErrorFilter<MyErrorFilter>()
                .InitializeOnStartup()
                .ModifyRequestOptions(options => options.IncludeExceptionDetails = true);
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

        public MyErrorFilter()
        {
        }

        public IError OnError(IError error)
        {
            return error.Exception is ArgumentException ? error.WithMessage(error.Exception.Message) : error;
        }
    }
}