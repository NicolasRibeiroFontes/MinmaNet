using MinMaNet.Domain.Models;
using System.Collections.Generic;

namespace MinMaNet.Generator.Languages
{
    public class CSharpService : GeneratorService
    {
        public override string Generate(Project project)
        {
            List<string> classes = new();

            project.Classes.ForEach(eachClass =>
            {
                var module = EntityModel.Replace("_projectname_", project.Title).Replace("_title_", eachClass.Title);

                GenerateProperties(eachClass.Properties, out string properties);

                classes.Add(module.Replace("_properties_", properties.ToString()));
            });

            var filePath = GenerateFiles(project.Title, classes);

            return filePath;
        }

        private static void GenerateProperties(List<Property> properties, out string propertiesGenerated)
        {
            propertiesGenerated = string.Empty;
            foreach (var property in properties)
             propertiesGenerated += PropertiesModel.Replace("_type_", property.Type).Replace("_name_", property.Title);
        }

        private static string EntityModel => "using System;\n\nnamespace _projectname_.Entities\n{\n" +
            "public class _title_\n{\n" +
            "_properties_" +
            "}\n}";
        private static string PropertiesModel => "public _type_ _name_ { get; set; }\n";
    }
}
