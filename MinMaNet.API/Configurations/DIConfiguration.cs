using Microsoft.Extensions.DependencyInjection;
using MinMaNet.Domain.Interfaces;
using MinMaNet.Generator.Languages;
using MinMaNet.Reader.Tools;

namespace MinMaNet.API.Configurations
{
    public static class DIConfiguration
    {
		public static void RegisterServices(this IServiceCollection services)
		{
			services.AddTransient<IReader, MindMapsAppService>();

			services.AddTransient<IGenerator, CSharpService>();
		}
	}
}
