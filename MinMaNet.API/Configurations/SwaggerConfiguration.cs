using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace MinMaNet.API.Configurations
{
	public static class SwaggerConfiguration
	{
		public static void AddSwaggerConfiguration(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "MinMaNet.API", Version = "v1" });
			});
		}

		public static void ConfigureSwaggerConfiguration(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "MinMaNet.API v1");
				c.RoutePrefix = "";
			});
		}
	}
}
