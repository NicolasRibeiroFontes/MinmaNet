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

        public virtual void Generate(Project project)
        {
            throw new NotSupportedException();
        }

        protected static string GenerateFiles(string project, List<string> classes)
        {
            string path = CreateFolder(project + "\\Entities", folderName);

            classes.ForEach(module =>
            {
                string filePath = GetFilePath(path, module, project);

                if (!File.Exists(filePath))
                {
                    using StreamWriter sw = File.CreateText(filePath);
                    sw.WriteLine(module.AsSpan(0, module.Length - 0));
                }
            });

            return "";
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
