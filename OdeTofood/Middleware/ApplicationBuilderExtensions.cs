using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;


//extension builder
namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use static files from root path
        /// only respond if request path starts with node_modules
        /// </summary>
        /// <param name="app"></param>
        /// <param name="root"></param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseNodeModules(
            this IApplicationBuilder app, string root)
        {
            
            //build absolute physical path
            //this is done by forcing the caller of method
            //to pass in the root of the project where
            //which is the same path where project.json files lives
            //OdeTofood\node_modules\bootstrap\dist
            var path = Path.Combine(root, "node_modules");

            //pass in path of physical file 
            var provider = new PhysicalFileProvider(path);

            //
            var options = new StaticFileOptions();//css static files
            var optionsBrowse = new StaticFileOptions();

            //only try to responde if request path
            //contains "/node_modules
            options.RequestPath = "/node_modules";

            //where to look for physical files
            options.FileProvider = provider; 

            //allow for static files
            //on project's folder to be served
            app.UseStaticFiles(options);

            //setup images files to be accessible
            //optionsBrowse.RequestPath = "/node_modules/images";
            //optionsBrowse.FileProvider = provider;

            //Root = "C:\\dev\\aptomy\\OdeTofood\\OdeTofood\\node_modules\\"
            //C:/dev/aptomy/OdeTofood/OdeTofood/node_modules
            app.UseDirectoryBrowser("/node_modules");

            return app;

   
          //  app.UseStaticFiles(); // For the wwwroot folder
          /*
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
            root),
                RequestPath = new PathString("/node_modules")
            });

            return app;

    */

        }

    }
}
