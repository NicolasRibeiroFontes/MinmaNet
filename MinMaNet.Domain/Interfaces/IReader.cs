using Microsoft.AspNetCore.Http;
using MinMaNet.Domain.Models;
using System.Threading.Tasks;

namespace MinMaNet.Domain.Interfaces
{
    public interface IReader
	{
		Task<Project> GenerateCommonModelFromJsonFile(IFormFile file);
	}
}
