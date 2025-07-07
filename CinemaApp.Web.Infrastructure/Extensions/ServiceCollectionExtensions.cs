namespace CinemaApp.Web.Infrastructure.Extensions
{
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        private static readonly string ServiceInterfacePrefix = "I";
        private static readonly string ServiceTypeSuffix = "Service";

        public static IServiceCollection AddUserDefinedServices(this IServiceCollection serviceCollection, Assembly serviceAssembly)
        {
            Type[] serviceClasses = serviceAssembly
                .GetTypes()
                .Where(t => !t.IsInterface && 
                                 t.Name.EndsWith(ServiceTypeSuffix))
                .ToArray();
            foreach (Type serviceClass in serviceClasses)
            {
                Type[] serviceClassInterfaces = serviceClass
                    .GetInterfaces();
                if (serviceClassInterfaces.Length == 1 && 
                    serviceClassInterfaces.First().Name.StartsWith(ServiceInterfacePrefix) &&
                    serviceClassInterfaces.First().Name.EndsWith(ServiceTypeSuffix))
                {
                    Type serviceClassInterface = serviceClassInterfaces.First();

                    serviceCollection.AddScoped(serviceClassInterface, serviceClass);
                }
            }
            
            return serviceCollection;
        }
    }
}
