using MinMaNet.Domain.Interfaces;
using MinMaNet.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace MinMaNet.Generator
{
    public abstract class GeneratorService : IGenerator
    {
        private static readonly string folderName = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Projects");

        public virtual string Generate(Project project)
        {
            throw new NotSupportedException();
        }

        protected static string GenerateFiles(string project, string folder, List<(string content, string fileName)> classes)
        {
            string path = CreateFolder(project + folder, folderName);

            classes.ForEach(module => IOService.GenerateFile( string.Concat(path, "/", module.fileName), module.content));

            path = IOService.ZipFiles(folderName, project);

            return path;
        }

        protected static string GetZipPath(string path)
        {
            var folders = path.Split("\\");
            var indexFolderResource = Array.FindIndex(folders, s => s.ToLower().Equals("resources"));

            return string.Join("\\", folders, indexFolderResource, 3);
        }


        private static string CreateFolder(string projectName, string folderName)
        {
            string pathString = Path.Combine(folderName, projectName);
            Directory.CreateDirectory(pathString);
            return pathString;
        }

    }
}
