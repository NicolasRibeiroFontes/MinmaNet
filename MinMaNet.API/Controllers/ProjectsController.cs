using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinMaNet.Domain.Core;
using MinMaNet.Domain.Enums;
using MinMaNet.Domain.Interfaces;
using MinMaNet.Reader;
using System;
using System.Threading.Tasks;

namespace MinMaNet.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private IReader reader;

		public ProjectsController(IReader reader)
		{
			this.reader = reader;
		}

		[HttpGet("welcome")]
		public IActionResult Welcome()
		{
			return Ok("Welcome!!");
		}

		[HttpPost]
		public async Task<IActionResult> Generate([FromQuery] GenerateParameters parameters, IFormFile file)
		{
			if (file == null)
				return BadRequest("File not uploaded");

			IdentitySourceTool(parameters.Tool);

			return Ok(await reader.GenerateCommonModelFromJsonFile(file));
		}

		private void IdentitySourceTool(MindMapSourceTools sourceTool) =>
			reader = sourceTool switch
			{
				MindMapSourceTools.MindMapsApp => new MindMapsAppService(),
				_ => throw new NotSupportedException("Source Tool not identified"),
			};
	}
}
