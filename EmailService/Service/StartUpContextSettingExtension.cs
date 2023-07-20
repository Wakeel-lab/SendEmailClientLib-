using EmailService.Implementation;
using EmailService.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Service
{
    public static class StartUpContextSettingExtension
    {
        public static IServiceCollection EmailServiceCollection(this IServiceCollection builderServices)
        {
            builderServices.AddScoped<IEmailRepository, EmailRepository>();
            return builderServices;
        }
    }
}
