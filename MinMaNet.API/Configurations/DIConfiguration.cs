using Microsoft.Extensions.DependencyInjection;
using MinMaNet.Domain.Interfaces;
using MinMaNet.Reader;

namespace MinMaNet.API.Configurations
{
    public static class DIConfiguration
    {
		public static void RegisterServices(this IServiceCollection services)
		{
			services.AddTransient<IReader, MindMapsAppService>();
		}
	}
}
