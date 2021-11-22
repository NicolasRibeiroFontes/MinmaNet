using Microsoft.AspNetCore.Http;
using MinMaNet.Domain.Interfaces;
using MinMaNet.Domain.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MinMaNet.Reader
{
    public abstract class ReaderService : IReader
	{
		protected static string ReadFile(IFormFile file)
		{
			var json = new StringBuilder();

			using (var reader = new StreamReader(file.OpenReadStream()))
				while (reader.Peek() >= 0)
					json.AppendLine(reader.ReadLine());

			return json.ToString();
		}

		public virtual Task<Project> GenerateCommonModelFromJsonFile(IFormFile file)
		{
			throw new NotSupportedException();
		}

	}
}
