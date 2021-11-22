using Microsoft.AspNetCore.Http;
using MinMaNet.Domain.Models;
using MinMaNet.Domain.Tools;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MinMaNet.Reader
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
			//All properties must to have a type
			mindMup.MindMap.Root.Children.ForEach(classes =>
			{
				classes.Children.ForEach(properties =>
				{
					if (properties.Children.Count != 1)
						throw new Exception("All the properties must to have only 1 type (children)! " +
							"Error with the property: " + properties.Text.Caption);
				});
			});
		}
	}
}
