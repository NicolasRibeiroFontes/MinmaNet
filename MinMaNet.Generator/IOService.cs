using System;
using System.IO;
using System.IO.Compression;

namespace MinMaNet.Generator
{
    internal static class IOService
    {
        internal static void GenerateFile(string filePath, string content)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

                using StreamWriter sw = File.CreateText(filePath);
                sw.WriteLine(content.AsSpan(0, content.Length - 0));
        }

        internal static string ZipFiles(string path, string projectName)
        {
            var filePath = path + "\\" + projectName;
            if (File.Exists(filePath + ".zip"))
                File.Delete(filePath + ".zip");


            ZipFile.CreateFromDirectory(filePath, filePath + ".zip");

            return filePath + ".zip";
        }
    }
}
