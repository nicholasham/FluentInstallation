using System;
using System.IO;
using System.Reflection;

namespace FluentInstallation.Web.Administration
{
   

    
    public static class WebAdministrationExtensions
    {
        static WebAdministrationExtensions()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolveHandler;
        }

        private static Assembly AssemblyResolveHandler(object sender, ResolveEventArgs args)
        {
            var name = new AssemblyName(args.Name);

            if (name.Name == "Microsoft.Web.Administration")
            {
                const string assemblyPath = @"C:\Windows\System32\inetsrv\Microsoft.Web.Administration.dll";

                //Load the assembly from the specified path.                    
                Assembly loadingAssembly = Assembly.LoadFrom(assemblyPath);

                //Return the loaded assembly.
                return loadingAssembly;

            }

            return null;
        }


        public static IWebServerConfigurer ConfigureWebServer(this IInstallerContext context)
        {
            return new WebServerConfigurer(context.Logger, new WrappedServerManager());
        }


    }
}