using GraphQL.Common.Dtos;
using GraphQL.Domain.Models;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GraphQL.Common
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<CreateUserDto, User>
                .NewConfig()
                .Map(dest => dest.Userame, src => src.Username);

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
