using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using OdeTofood.Services;
using OdeTofood.Entities;

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
            services.AddMvc();
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter, Greeter>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddDbContext<OdeToFoodDbContext>(options => 
                    options.UseSqlServer(Configuration.GetConnectionString("OdeToFood")));
            
          
            //Scoped entails all pieces of the application see the same 
            //data for this object, but if a new HTTP request comes in then
            //a new instantiated object is created
            //services.AddScoped<IRestaurantData, InMemoryRestaurantData>();

            
           

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
                    //exception page
                    ExceptionHandler = context => context.Response.WriteAsync("Ooops!!")

                });
            }

            app.UseFileServer();//this line combines both lines below... 

            //app.UseMvcWithDefaultRoute();//this middleware will look for incoming http request
            //and try to map this request to a C# class

            app.UseMvc(ConfigureRoutes);//gives mvc without any routing rules.  You'll need to pass in the method to route to
                                        //there are 3 steps to using MVC framework:  1) intall package, 2) install middleware 3) register the services


            app.Run(ctx => ctx.Response.WriteAsync("Not Found"));
            /*
            app.UseDefaultFiles();//look for incoming request and see if there is a default file that matches the request
            app.UseStaticFiles();//does not serve up a file
            */

            //app.UseWelcomePage();//allows you to see if asp.net core application can come up on the server correctly and
            //everyhting is configured.
            //This is a new piece of middleware that I've added to the application

            
            //app.UseWelcomePage(new WelcomePageOptions
            //{
            //    Path = "/Welcome"
            //});

            //app.Run(async (context) =>
            //{

            //    //throw new Exception("Something bad happened!");
            //    var message = greeter.GetGreeting(); ///Configuration["Greeting"];

            //    await context.Response.WriteAsync(message);
            //});
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            //second parameter describes to MVC framework how to pick apart the parth of URL
            // for example go to controler /Home and Index is the action name /Home/Index 
            //{controller} is the class name i.e. HomeController {action} is Index() method
            //{id?} is the parameter and ? entails the parameter is optional
            //if no controller is found, default is Home
            //if not action is found default is Index
            try
            {
                //convention base routing
                routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}");
            }
            catch (Exception error)
            {
                System.Console.Write("\nException occured:  " + error.StackTrace.ToString());
                throw new NotImplementedException();
            }
            
        }
    }
}
