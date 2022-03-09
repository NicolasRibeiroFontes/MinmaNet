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
            string dbSets = string.Empty;
            List<string> mappings = new();

            project.Classes.ForEach(eachClass =>
            {
                // entities
                var module = EntityModel.Replace("_projectname_", project.Title).Replace("_title_", eachClass.Title);

                GenerateProperties(eachClass.Properties, out string properties);
                classes.Add(module.Replace("_properties_", properties));

                //controllers
                var controller = ControllerModel.Replace("_projectname_", project.Title).Replace("_title_", eachClass.Title);
                controllers.Add(controller);

                dbSets += DbSetsContextModel.Replace("_entity_", eachClass.Title);

                mappings.Add(MappingModel.Replace("_projectname_", project.Title).Replace("_entity_", eachClass.Title));
            });

            string context = DbContextModel.Replace("_projectname_", project.Title).Replace("_dbSets_", dbSets);

            var filePathEntity = GenerateFiles(project.Title, "\\Generate\\Entities", classes);
            GenerateFiles(project.Title, "\\Generate\\Controllers", controllers);
            GenerateFiles(project.Title, "\\Generate\\Context", new List<string>() { context });
            GenerateFiles(project.Title, "\\Generate\\Mappings", mappings);

            return filePathEntity;
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

        private static string DbContextModel => "using Microsoft.EntityFrameworkCore;\nusing _projectname_.Entities;\n" +
            "using _projectname_.Infra.Mapping;\n\nnamespace _projectname_.Infra.Context\n{\n" +
            "public class DBContext : DbContext\n{\npublic DBContext(DbContextOptions<MySQLContext> options) : base(options) { }\n\n" +
            "_dbSets_\n\n" +
            "protected override void OnModelCreating(ModelBuilder modelBuilder)\n{\n" +
            "base.OnModelCreating(modelBuilder);\n\n" +
            "_mappings_\n\n" +
            "}\n}\n}";

        private static string DbSetsContextModel => "public DbSet<_entity_> _entity_s { get; set; }\n";

        private static string MappingModel => "using Microsoft.EntityFrameworkCore;\nusing Microsoft.EntityFrameworkCore.Metadata.Builders;\n" +
            "using _projectname_.Entities;\n\nnamespace _projectname_.Infra.Mapping\n{\n" +
            "public class _entity_Mapping : IEntityTypeConfiguration<_entity_>\n{\n" +
            "public void Configure(EntityTypeBuilder<_entity_> builder)\n{\nbuilder.HasKey(key => key.Id);\n}\n}\n}";
    }
}
