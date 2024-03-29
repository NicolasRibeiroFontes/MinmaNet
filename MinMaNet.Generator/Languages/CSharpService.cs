﻿using MinMaNet.Domain.Models;
using System.Collections.Generic;

namespace MinMaNet.Generator.Languages
{
    public class CSharpService : GeneratorService
    {
        public override string Generate(Project project)
        {
            List<(string content, string fileName)> classes = new();
            List<(string content, string fileName)> controllers = new();
            string dbSets = string.Empty;
            List<(string content, string fileName)> mappings = new();
            List<(string content, string fileName)> repositories = new();
            List<(string content, string fileName)> interfaceRepositories = new();

            if (project.Classes.Count == 0)
                throw new System.Exception("There is no class for this project");

            project.Classes.ForEach(eachClass =>
            {
                // entities
                var module = EntityModel.Replace("_projectname_", project.Title).Replace("_title_", eachClass.Title);
                module = module.Replace("_properties_", GenerateProperties(eachClass.Properties));
                classes.Add((module, GetFileName(module)));

                // controllers
                var controller = ControllerModel.Replace("_projectname_", project.Title).Replace("_title_", eachClass.Title);
                controllers.Add((controller, GetFileName(controller)));

                // dbSets
                dbSets += DbSetsContextModel.Replace("_entity_", eachClass.Title);

                // mappings
                var map = MappingModel.Replace("_projectname_", project.Title).Replace("_entity_", eachClass.Title);
                mappings.Add((map, GetFileName(map)));

                // repositories
                var repository = RepositoryModel.Replace("_projectname_", project.Title).Replace("_entity_", eachClass.Title);
                repositories.Add((repository, GetFileName(repository)));

                var interfaceRepository = IRepositoryModel.Replace("_projectname_", project.Title).Replace("_entity_", eachClass.Title);
                interfaceRepositories.Add((interfaceRepository, GetFileName(interfaceRepository)));
            });

            string context = DbContextModel.Replace("_projectname_", project.Title).Replace("_dbSets_", dbSets);

            var filePathEntity = GenerateFiles(project.Title, "/Generate/Domain/Entities", classes) ;
            GenerateFiles(project.Title, @"/Generate/API/Controllers", controllers);
            GenerateFiles(project.Title, @"/Generate/Infra/Context", new List<(string content, string fileName)> { (context, GetFileName(context)) });
            GenerateFiles(project.Title, @"/Generate/Infra/Mappings", mappings);
            GenerateFiles(project.Title, @"/Generate/Infra/Repositories", repositories);
            GenerateFiles(project.Title, @"/Generate/Domain/Interfaces", interfaceRepositories);

            GenerateFiles(project.Title, @"/Generate/Domain", new List<(string content, string fileName)>() { (CsProjDomainXML, project.Title + ".Domain.csproj") });
            GenerateFiles(project.Title, @"/Generate/Infra", new List<(string content, string fileName)>() { (CsProjInfraXML, project.Title + ".Infra.csproj") });

            filePathEntity = GetZipPath(filePathEntity);

            return filePathEntity;
        }

        private static string GetFileName(string content)
        {
            var publicClass = "public class ";
            int index = content.LastIndexOf(publicClass);

            if (index == -1)
            {
                publicClass = "public interface ";
                index = content.LastIndexOf(publicClass);
            }

            int indexAfterName = content.IndexOf(" ", index + publicClass.Length);
            string moduleName = content[(index + publicClass.Length)..indexAfterName];
            return moduleName + ".cs";
        }

        private static string GenerateProperties(List<Property> properties)
        {
            string propertiesGenerated = string.Empty;
            foreach (var property in properties)
             propertiesGenerated += PropertiesModel.Replace("_type_", property.Type).Replace("_name_", property.Title);

            return propertiesGenerated;
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

        private static string RepositoryModel => "using Microsoft.EntityFrameworkCore;\nusing _projectname_.Entities;\nusing _projectname_.Infra.Context;\n\n" +
            "namespace _projectname_.Infra.Repositories\n{\npublic class _entity_Repository : I_entity_Repository\n{\nprivate readonly DBContext context;\n\n" +
            "public _entity_Repository(DBContext context)\n{\nthis.context = context;\n}\n\npublic _entity_ Create(_entity_ model)\n{\n" +
            "this.context._entity_s.Add(model);\nthis.context.SaveChanges();\nreturn model;\n}\n\n" +
            "public _entity_? Get(int id)\n{\nreturn context._entity_s.FirstOrDefault(x => x.Id == id);\n}\n\n" +
            "public IQueryable<_entity_> Get()\n{\nreturn context._entity_s;\n}\n\n" +
            "public _entity_ Update(_entity_ model)\n{\nvar entry = context.Entry(model);\nthis.context._entity_s.Attach(model);\n" +
            "entry.State = EntityState.Modified;\ncontext.SaveChanges();\nreturn model;\n}\n\n" +
            "public void Delete(_entity_ model)\n{\ncontext._entity_s.Remove(user);\ncontext.SaveChanges();\n}\n}\n}";

        private static string IRepositoryModel => "using _projectname_.Entities;\n\nnamespace _projectname_.Infra.Interfaces\n{\n" +
            "public interface I_entity_Repository \n{\n_entity_ Create(_entity_ model);\n_entity_? Get(int id);\n" +
            "IQueryable<_entity_> Get();\n_entity_ Update(_entity_ model);\nvoid Delete(_entity_ model);\n}\n}";

        private static string CsProjDomainXML => "<Project Sdk=\"Microsoft.NET.Sdk\">\n\n" +
            "<PropertyGroup>\n<TargetFramework>net6.0</TargetFramework>\n<ImplicitUsings>enable</ImplicitUsings>\n" +
            "<Nullable>enable</Nullable>\n</PropertyGroup>\n\n</Project>\n";

        private static readonly string CsProjInfraXML = "<Project Sdk=\"Microsoft.NET.Sdk\">\n\n" +
            "<PropertyGroup>\n<TargetFramework>net6.0</TargetFramework>\n<ImplicitUsings>enable</ImplicitUsings>\n" +
            "<Nullable>enable</Nullable>\n</PropertyGroup>\n\n" +
            "<ItemGroup><PackageReference Include=\"Microsoft.EntityFrameworkCore\" Version=\"6.0.2\" />\n" +
            "<PackageReference Include=\"Microsoft.EntityFrameworkCore.Design\" Version=\"6.0.2\">\n" +
            "<PrivateAssets>all</PrivateAssets>\n<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>\n" +
            "</PackageReference>\n<PackageReference Include=\"Microsoft.EntityFrameworkCore.Tools\" Version=\"6.0.2\">\n" +
            "<PrivateAssets>all</PrivateAssets>\n<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>\n" +
            "</PackageReference>\n<PackageReference Include=\"Pomelo.EntityFrameworkCore.MySql\" Version=\"6.0.1\" />\n</ItemGroup>\n\n" +
            "ItemGroup>\n<ProjectReference Include=\"..\\_projectname_.Domain\\_projectname_.Domain.csproj\" />\n</ItemGroup>" +
            "\n\n</Project>\n";
    }
}
