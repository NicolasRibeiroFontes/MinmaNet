using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace MinMaNet.API.Configurations
{
	public static class SwaggerConfiguration
	{
		public static void AddSwaggerConfiguration(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "MinMaNet",
					Description = "Welcome to MinMaNet! This is an open source tool to generate software code based on Mind Maps of external tools/website which has the functionality to export mind maps in specific formats.",
					Contact = new OpenApiContact
					{
						Name = "Repository on Github",
						Email = string.Empty,
						Url = new Uri("https://github.com/NicolasRibeiroFontes/MinmaNet"),
					}
				});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
				c.DescribeAllParametersInCamelCase();
			});
		}

		public static void ConfigureSwaggerConfiguration(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "MinMaNet v1");
				c.RoutePrefix = "documentation";
			});
		}
	}
}
