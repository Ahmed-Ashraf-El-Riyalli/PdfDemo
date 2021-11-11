using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Web
{
    public static class AddLibwkhtmltoxHelper
    {
        public static void AddLibwkhtmltox(this IServiceCollection services)
        {
            var context = new CustomAssemblyLoadContext();

            string architectureFolder = (IntPtr.Size == 8) ? "64bit" : "32bit";
            string wkhtmltoxPath = @$"wwwroot\libwkhtmltox\v0.12.4\{architectureFolder}\libwkhtmltox";
            string wkhtmltoxAbsolutePath = Path.Combine(Directory.GetCurrentDirectory(), wkhtmltoxPath);

            context.LoadUnmanagedLibrary(wkhtmltoxAbsolutePath);

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        }
    }
}
