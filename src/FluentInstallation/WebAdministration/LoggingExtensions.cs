using Microsoft.Web.Administration;

namespace FluentInstallation.WebAdministration
{
    public static class LoggingExtensions
    {

        internal static void ContructAddMessage(this ApplicationPool applicationPool, IMessageBuilder builder)
        {
            builder
                .WriteLine("Added Application Pool")
                .IncreaseIndent()
                .WriteLine("Name: {0}", applicationPool.Name)
                .WriteLine("Managed Pipe Line Mode: {0}", applicationPool.ManagedPipelineMode)
                .WriteLine("Runtime version: {0}", applicationPool.ManagedRuntimeVersion);
        }

        internal static void ContructAddMessage(this VirtualDirectory virtualDirectory, IMessageBuilder builder)
        {
            builder
                .WriteLine("Added Virtual Directory")
                .IncreaseIndent()
                .WriteLine("Alias: {0}", virtualDirectory.Path)
                .WriteLine("Physcial Path {0}", virtualDirectory.PhysicalPath);
        }

        internal static void ContructAddMessage(this Application application, IMessageBuilder builder)
        {
            builder
                .WriteLine("Added Application")
                .IncreaseIndent()
                .WriteLine("Alias: {0}", application.VirtualDirectory().Path)
                .WriteLine("Physcial Path {0}", application.VirtualDirectory().PhysicalPath)
                .WriteLine("Application Pool: {0}", application.ApplicationPoolName);
        }

        internal static void ContructAddMessage(this Site site, IMessageBuilder builder)
        {
            builder
                .WriteLine("Added Website")
                .IncreaseIndent()
                .WriteLine("Name: {0}", site.Name)
                .WriteLine("Id: {0}", site.Id)
                .WriteLine("Application Pool: {0}", site.Application().ApplicationPoolName);
        }

        internal static void ContructAddMessage(this Binding binding, IMessageBuilder builder)
        {
            var bindingInformation = binding.ToBindingInformation();

            builder
                .WriteLine("Added Binding")
                .IncreaseIndent()
                .WriteLine("Protocol: {0}", binding.Protocol)
                .WriteLine("Port: {0}", bindingInformation.Port)
                .WriteLine("IP: {0}", bindingInformation.IpAddress)
                .WriteLine("Host Name: {0}", bindingInformation.HostName);
        }

        internal static void ContructRemoveMessage(this ApplicationPool applicationPool, IMessageBuilder builder)
        {
            builder
                .WriteLine("Removed Application Pool")
                .IncreaseIndent()
                .WriteLine("Name: {0}", applicationPool.Name)
                .WriteLine("Managed Pipe Line Mode: {0}", applicationPool.ManagedPipelineMode)
                .WriteLine("Runtime version: {0}", applicationPool.ManagedRuntimeVersion);
        }

        internal static void ContructRemoveMessage(this VirtualDirectory virtualDirectory, IMessageBuilder builder)
        {
            builder
                .WriteLine("Removed Virtual Directory")
                .IncreaseIndent()
                .WriteLine("Alias: {0}", virtualDirectory.Path)
                .WriteLine("Physcial Path {0}", virtualDirectory.PhysicalPath);
        }

        internal static void ContructRemoveMessage(this Application application, IMessageBuilder builder)
        {
            builder
                .WriteLine("Removed Application")
                .IncreaseIndent()
                .WriteLine("Alias: {0}", application.VirtualDirectory().Path)
                .WriteLine("Physcial Path {0}", application.VirtualDirectory().PhysicalPath)
                .WriteLine("Application Pool: {0}", application.ApplicationPoolName);
        }

        internal static void ContructRemoveMessage(this Site site, IMessageBuilder builder)
        {
            builder
                .WriteLine("Removed Website")
                .IncreaseIndent()
                .WriteLine("Name: {0}", site.Name)
                .WriteLine("Id: {0}", site.Id)
                .WriteLine("Application Pool: {0}", site.Application().ApplicationPoolName);
        }

        internal static void ContructRemoveMessage(this Binding binding, IMessageBuilder builder)
        {
            var bindingInformation = binding.ToBindingInformation();

            builder
                .WriteLine("Removed Binding")
                .IncreaseIndent()
                .WriteLine("Protocol: {0}", binding.Protocol)
                .WriteLine("Port: {0}", bindingInformation.Port)
                .WriteLine("IP: {0}", bindingInformation.IpAddress)
                .WriteLine("Host Name: {0}", bindingInformation.HostName);
        }


    }
}