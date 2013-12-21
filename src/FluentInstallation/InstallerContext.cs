using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentInstallation
{
    public class InstallerContext : IInstallerContext
    {
        public InstallerContext(IDictionary parameters, ILogger logger)
        {
           
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            Parameters = parameters;
            Logger = logger;

        }

        public IDictionary Parameters { get; private set; }

      

        public ILogger Logger { get; private set; }
    }
}