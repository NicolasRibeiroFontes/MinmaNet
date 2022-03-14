using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinMaNet.Domain.Core;
using MinMaNet.Domain.Enums;
using MinMaNet.Domain.Interfaces;
using MinMaNet.Generator.Languages;
using MinMaNet.Reader.Tools;
using System;
using System.Threading.Tasks;

namespace MinMaNet.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private IReader reader;
		private IGenerator generator;

		public ProjectsController(IReader reader, IGenerator generator)
		{
			this.reader = reader;
			this.generator = generator;
		}

		/// <summary>
		/// Endpoint to generate a zip file with the models created
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="files">JSON file from the Mind Map tools</param>
		/// <returns>An URL to download the zip file with the files created.</returns>
		[HttpPost]
		public async Task<IActionResult> Generate([FromQuery] GenerateParameters parameters, [FromForm] IEnumerable<IFormFile> files)
		{
			if (files.Count() == 0)
				return BadRequest("File not uploaded");

			IdentifySourceTool(parameters.Tool);
			IdentifyLanguage(parameters.Tool);

			var project = await reader.GenerateCommonModelFromJsonFile(files.First());
			var filePath = generator.Generate(project);

			return Ok(GetFilePathToDownload(filePath));
		}

		private string GetFilePathToDownload(string filePath) => $"{Request.Scheme}://{Request.Host.Value}/" + filePath;

		private void IdentifySourceTool(MindMapSourceTools sourceTool) =>
			reader = sourceTool switch
			{
				MindMapSourceTools.MindMapsApp => new MindMapsAppService(),
				_ => throw new NotSupportedException("Source Tool not identified"),
			};

		private void IdentifyLanguage(MindMapSourceTools sourceTool) =>
			generator = sourceTool switch
			{
				MindMapSourceTools.MindMapsApp => new CSharpService(),
				_ => throw new NotSupportedException("Source Tool not identified"),
			};
	}
}
