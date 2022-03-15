using Microsoft.AspNetCore.Http;
using MinMaNet.Domain.Models;
using MinMaNet.Domain.Tools;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MinMaNet.Reader.Tools
{
    public class MindMapsAppService : ReaderService
	{
		private static MindMapsApp mindMup;
		private static string json;

		public override Task<Project> GenerateCommonModelFromJsonFile(IFormFile file)
		{
			//Read JSON from any file
			json = ReadFile(file);

			//Deserialize Object
			DeserializeObject();

			//Validate
			Validate();

			//Convert
			return Task.FromResult(mindMup.Convert());
		}

		private static void DeserializeObject() =>
			mindMup = JsonConvert.DeserializeObject<MindMapsApp>(json);

		private static void Validate()
		{
			if (mindMup.MindMap.Root.Children.Count == 0)
				throw new Exception($"The project has no module defined");
			
			mindMup.MindMap.Root.Children.ForEach(classes =>
			{
				if (classes.Children.Count == 0)
					throw new Exception($"The module {classes.Text.Caption} has no properties defined");

				classes.Children.ForEach(properties =>
				{
					if (properties.Children.Count != 1)
						throw new Exception($"All the properties must to have only 1 type! " +
							$"Error with the property: {classes.Text.Caption}.{properties.Text.Caption}");
				});
			});
		}
	}
}
