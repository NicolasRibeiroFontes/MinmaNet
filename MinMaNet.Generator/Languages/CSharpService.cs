using MinMaNet.Domain.Models;
using System.Collections.Generic;

namespace MinMaNet.Generator.Languages
{
    public class CSharpService : GeneratorService
    {
        public override string Generate(Project project)
        {
            List<string> classes = new();
            List<string> controllers = new();

            project.Classes.ForEach(eachClass =>
            {
                var module = EntityModel.Replace("_projectname_", project.Title).Replace("_title_", eachClass.Title);

                GenerateProperties(eachClass.Properties, out string properties);

                classes.Add(module.Replace("_properties_", properties.ToString()));

                var controller = ControllerModel.Replace("_projectname_", project.Title).Replace("_title_", eachClass.Title);
                controllers.Add(controller);
            });

            var filePath = GenerateFiles(project.Title, "\\Entities2", classes);
            var filePathController = GenerateFiles(project.Title, "\\Controllers2", controllers);

            return filePath;
        }

        private static void GenerateProperties(List<Property> properties, out string propertiesGenerated)
        {
            propertiesGenerated = string.Empty;
            foreach (var property in properties)
             propertiesGenerated += PropertiesModel.Replace("_type_", property.Type).Replace("_name_", property.Title);
        }

        private static string EntityModel => "using System;\n\nnamespace _projectname_.Entities\n{\n" +
            "public class _title_ \n{\n" +
            "_properties_" +
            "}\n}";
        private static string PropertiesModel => "public _type_ _name_ { get; set; }\n";

        private static string ControllerModel => "using Microsoft.AspNetCore.Http;\nusing Microsoft.AspNetCore.Mvc;\n" +
            "using _projectname_.Domain.Interfaces;\nusing _projectname_.Entities;\n\nnamespace _projectname_.Controllers\n{\n" +
            "[Route(\"api/[controller]\"), ApiController]\n" +
            "public class _title_Controller : ControllerBase\n{\n" +
            "private readonly I_title_Repository repository;\n\n" +
            "public _title_Controller(I_title_Repository repository)\n{\nthis.repository = repository;\n}\n\n" +
            "[HttpPost]\npublic IActionResult Post(_title_ model)\n{\nrepository.Post(model);\nreturn Ok(model);\n}\n\n" +
            "[HttpGet]\npublic IActionResult Get()\n{\nreturn Ok(repository.Get());\n}\n\n" +
            "[HttpGet(\"{id}\")]\npublic IActionResult GetById(int id)\n{\nreturn Ok(repository.Get(id));\n}\n\n" +
            "[HttpPut]\npublic IActionResult Put(_title_ model)\n{\nrepository.Put(model);\nreturn Ok(model);\n}\n\n" +
            "[HttpDelete]\npublic IActionResult Delete(int id)\n{\nreturn Ok(repository.Delete(id));\n}\n}\n}";
    }
}
