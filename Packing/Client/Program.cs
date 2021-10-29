using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MudBlazor.Services;
using Packing.Client.Servicios;

namespace Packing.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("Packing.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            
            builder.Services.AddScoped(sp => 
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("Packing.ServerAPI"));

            builder.Services.AddApiAuthorization();
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("oidc", options.ProviderOptions);
                options.UserOptions.RoleClaim = "role";
            });

            ConfigurarServicios(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigurarServicios(IServiceCollection services)
        {
            services.AddSingleton<EnviadorCorreos>();
            services.AddMudServices();
        }
    }
}
