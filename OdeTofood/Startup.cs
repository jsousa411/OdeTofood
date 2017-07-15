using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace OdeTofood
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

            //add NuGetPackages
            var builder = new ConfigurationBuilder()
                          .SetBasePath(env.ContentRootPath)
                          .AddJsonFile("appsettings.json")
                          .AddEnvironmentVariables();

            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter, Greeter>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IGreeter greeter)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                //you need this register before the app.run(), so we can capture errors
                //remove this information before deployment
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //it's production env.
                //app.UseExceptionHandler("/error");//use a different path for application to respond differently

                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    //use lambda expression to display errors for no dev env.

                    ExceptionHandler = context => context.Response.WriteAsync("Ooops!!")

                });
            }

            app.UseFileServer();//this line combines both lines below... 

            /*
            app.UseDefaultFiles();//look for incoming request and see if there is a default file that matches the request
            app.UseStaticFiles();//does not serve up a file
            */

            //app.UseWelcomePage();//allows you to see if asp.net core application can come up on the server correctly and
            //everyhting is configured.
            //This is a new piece of middleware that I've added to the application

            app.UseWelcomePage(new WelcomePageOptions
            {
                Path = "/Welcome"
            });

            app.Run(async (context) =>
            {

                //throw new Exception("Something bad happened!");
                var message = greeter.GetGreeting(); ///Configuration["Greeting"];

                await context.Response.WriteAsync(message);
            });
        }
    }
}
