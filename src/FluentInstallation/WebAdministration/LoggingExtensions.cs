using Microsoft.Web.Administration;

namespace FluentInstallation.WebAdministration
{
    public static class LoggingExtensions
    {

        internal static void ContructAddMessage(this ApplicationPool applicationPool, IMessageBuilder builder)
        {
            builder
                .WriteLine("Adding Application Pool")
                .IncreaseIndent()
                .WriteLine("Name: {0}", applicationPool.Name)
                .WriteLine("Managed Pipe Line Mode: {0}", applicationPool.ManagedPipelineMode)
                .WriteLine("Runtime version: {0}", applicationPool.ManagedRuntimeVersion);
        }

        internal static void ContructAddMessage(this VirtualDirectory virtualDirectory, IMessageBuilder builder)
        {
            builder
                .WriteLine("Adding Virtual Directory")
                .IncreaseIndent()
                .WriteLine("Alias: {0}", virtualDirectory.Path)
                .WriteLine("Physical Path {0}", virtualDirectory.PhysicalPath);
        }

        internal static void ContructAddMessage(this Application application, IMessageBuilder builder)
        {
            builder
                .WriteLine("Adding Application")
                .IncreaseIndent()
                .WriteLine("Alias: {0}", application.Path)
                .WriteLine("Physical Path {0}", application.VirtualDirectory().PhysicalPath)
                .WriteLine("Application Pool: {0}", application.ApplicationPoolName);
        }

        internal static void ContructAddMessage(this Site site, IMessageBuilder builder)
        {
            builder
                .WriteLine("Adding Website")
                .IncreaseIndent()
                .WriteLine("Name: {0}", site.Name)
                .WriteLine("Id: {0}", site.Id)
                .WriteLine("Physical Path {0}", site.Application().VirtualDirectory().PhysicalPath)
                .WriteLine("Application Pool: {0}", site.Application().ApplicationPoolName);
        }

        internal static void ContructAddMessage(this Binding binding, IMessageBuilder builder)
        {
            var bindingInformation = binding.ToBindingInformation();

            builder
                .WriteLine("Adding Binding")
                .IncreaseIndent()
                .WriteLine("Protocol: {0}", binding.Protocol)
                .WriteLine("Port: {0}", bindingInformation.Port)
                .WriteLine("IP: {0}", bindingInformation.IpAddress)
                .WriteLine("Host Name: {0}", bindingInformation.HostName);
        }

        internal static void ContructRemoveMessage(this ApplicationPool applicationPool, IMessageBuilder builder)
        {
            builder
                .WriteLine("Removing Application Pool")
                .IncreaseIndent()
                .WriteLine("Name: {0}", applicationPool.Name)
                .WriteLine("Managed Pipe Line Mode: {0}", applicationPool.ManagedPipelineMode)
                .WriteLine("Runtime version: {0}", applicationPool.ManagedRuntimeVersion);
        }

        internal static void ContructRemoveMessage(this VirtualDirectory virtualDirectory, IMessageBuilder builder)
        {
            builder
                .WriteLine("Removing Virtual Directory")
                .IncreaseIndent()
                .WriteLine("Alias: {0}", virtualDirectory.Path)
                .WriteLine("Physical Path {0}", virtualDirectory.PhysicalPath);
        }

        internal static void ContructRemoveMessage(this Application application, IMessageBuilder builder)
        {
            builder
                .WriteLine("Removing Application")
                .IncreaseIndent()
                .WriteLine("Alias: {0}", application.Path)
                .WriteLine("Physical Path {0}", application.VirtualDirectory().PhysicalPath)
                .WriteLine("Application Pool: {0}", application.ApplicationPoolName);
        }

        internal static void ContructRemoveMessage(this Site site, IMessageBuilder builder)
        {
            builder
                .WriteLine("Removing Website")
                .IncreaseIndent()
                .WriteLine("Name: {0}", site.Name)
                .WriteLine("Id: {0}", site.Id)
                .WriteLine("Application Pool: {0}", site.Application().ApplicationPoolName);
        }

        internal static void ContructAlterMessage(this Site site, IMessageBuilder builder)
        {
            builder
                .WriteLine("Altering Website")
                .IncreaseIndent()
                .WriteLine("Name: {0}", site.Name)
                .WriteLine("Id: {0}", site.Id)
                .WriteLine("Application Pool: {0}", site.Application().ApplicationPoolName);
        }

        internal static void ContructRemoveMessage(this Binding binding, IMessageBuilder builder)
        {
            var bindingInformation = binding.ToBindingInformation();

            builder
                .WriteLine("Removing Binding")
                .IncreaseIndent()
                .WriteLine("Protocol: {0}", binding.Protocol)
                .WriteLine("Port: {0}", bindingInformation.Port)
                .WriteLine("IP: {0}", bindingInformation.IpAddress)
                .WriteLine("Host Name: {0}", bindingInformation.HostName);
        }


    }
}