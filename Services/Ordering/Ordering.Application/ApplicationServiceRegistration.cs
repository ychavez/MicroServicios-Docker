using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            // configuramos automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            ///configuramos fluent validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            //configuramos Mediatr
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
