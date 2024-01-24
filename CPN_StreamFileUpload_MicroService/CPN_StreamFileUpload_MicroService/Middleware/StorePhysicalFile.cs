using static System.Net.Mime.MediaTypeNames;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileProviders;

namespace CPN_StreamFileUpload_MicroService.Middleware
{
    internal static class StorePhysicalFile
    {
        public static IServiceCollection AddStorePhysicalFileConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            // To list physical files from a path provided by configuration:
            string storeFilePath = configuration.GetValue<string>("StoredFilesPath");

            bool exists = System.IO.Directory.Exists(storeFilePath);

            if (!exists)
                System.IO.Directory.CreateDirectory(storeFilePath);

            var physicalProvider = new PhysicalFileProvider(storeFilePath);

            services.AddSingleton<IFileProvider>(physicalProvider);

            return services;
        }
    }
}
