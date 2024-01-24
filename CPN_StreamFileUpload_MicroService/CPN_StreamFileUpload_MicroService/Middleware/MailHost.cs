using static System.Net.Mime.MediaTypeNames;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileProviders;
using CPN_StreamFileUpload_MicroService.Models;

namespace CPN_StreamFileUpload_MicroService.Middleware
{
    internal static class MailHost
    {
        public static IServiceCollection AddMailHostConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection("MailHost").Get<MailConfig>());

            return services;
        }
    }
}
