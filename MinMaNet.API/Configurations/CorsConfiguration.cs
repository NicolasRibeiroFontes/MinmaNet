using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MinMaNet.API.Configurations
{
	public static class CorsConfiguration
	{
		private static readonly string _corsName = "CorsPolicyMinMaNet";

		public static void AddCORSConfiguration(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(_corsName, builder =>
					builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
			});
		}

		public static void EnableCORS(this IApplicationBuilder app)
		{
			app.UseCors(_corsName);
		}
	}
}
