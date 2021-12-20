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

        protected static string GenerateFiles(string project, List<string> classes)
        {
            string path = CreateFolder(project + "\\Entities", folderName);

            classes.ForEach(module => IOService.GenerateFile(GetFilePath(path, module, project), module));

            path = IOService.ZipFiles(folderName, project);

            path = GetZipPath(path);

            return path;
        }

        private static string GetZipPath(string path)
        {
            var folders = path.Split("\\");
            var indexFolderResource = Array.FindIndex(folders, s => s.ToLower().Equals("resources"));

            return string.Join("\\", folders, indexFolderResource, 3);
        }

        private static string GetFilePath(string path, string module, string project)
        {
            var publicClass = "public class ";
            int index = module.IndexOf(publicClass);
            int indexAfterName = module.IndexOf("\n{",index);
            string moduleName = module[(index + publicClass.Length)..indexAfterName];
            return path + "/" + moduleName + ".cs";
        }

        private static string CreateFolder(string projectName, string folderName)
        {
            string pathString = Path.Combine(folderName, projectName);
            Directory.CreateDirectory(pathString);
            return pathString;
        }

    }
}
