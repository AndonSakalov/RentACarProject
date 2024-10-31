using Microsoft.Extensions.DependencyInjection;
using RentACar.Data.Models;
using RentACar.Data.Repository;
using RentACar.Data.Repository.Interfaces;
using System.Reflection;
namespace RentACar.Web.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services, Assembly modelsAssembly)
        {
            Type[] typesToExclude = new Type[]
            {
                typeof(ApplicationUser)
            };

            Type[] modelTypes = modelsAssembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && !t.Name.ToLower().EndsWith("attribute") && !t.IsEnum)
                .ToArray();

            foreach (Type type in modelTypes)
            {
                if (!typesToExclude.Contains(type))
                {
                    Type repositoryInterface = typeof(IRepository<,>);
                    Type repositoryInstanceType = typeof(Repository<,>);

                    PropertyInfo? idPropInfo = type
                        .GetProperties()
                        .Where(p => p.Name.ToLower() == "id")
                        .SingleOrDefault();

                    Type[] args = new Type[2];
                    args[0] = type;

                    if (idPropInfo == null)
                    {
                        args[1] = typeof(object);
                    }
                    else
                    {
                        args[1] = idPropInfo.PropertyType;
                    }

                    repositoryInterface = repositoryInterface.MakeGenericType(args);
                    repositoryInstanceType = repositoryInstanceType.MakeGenericType(args);

                    services.AddScoped(repositoryInterface, repositoryInstanceType);
                }
            }
        }

        public static void RegisterServices(this IServiceCollection services, Assembly serviceAssembly)
        {
            Type[] servicesInterfacesTypes = serviceAssembly.GetTypes()
                .Where(t => t.IsInterface)
                .ToArray();

            Type[] servicesTypes = serviceAssembly.GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract && t.Name.ToLower().EndsWith("service"))
                .ToArray();

            foreach (Type serviceInterfaceType in servicesInterfacesTypes)
            {
                Type? serviceType = servicesTypes
                    .SingleOrDefault(t => "i" + t.Name.ToLower() == serviceInterfaceType.Name.ToLower());
                if (serviceType == null)
                {
                    throw new NullReferenceException($"Service type could not be obtained for the service {serviceInterfaceType.Name}");
                }

                services.AddScoped(serviceInterfaceType, serviceType);
            }
        }
    }
}
