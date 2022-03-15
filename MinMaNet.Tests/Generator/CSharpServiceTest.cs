using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinMaNet.Domain.Models;
using MinMaNet.Generator;
using MinMaNet.Generator.Languages;
using Moq;
using System;
using System.Collections.Generic;

namespace MinMaNet.Tests.Generator
{
    [TestClass]
    public class CSharpServiceTest
    {
        private readonly Mock<CSharpService> generatorService = new();
        private readonly CSharpService cSharpService = new();
        private readonly Project projectSuccess = new("Success",
            new List<Class> { 
                new Class("Class1", new List<Property> { new Property("Prop1", "Type1") }),
                new Class("Class2", new List<Property> { new Property("Prop2", "Type2") }),
            });
        private readonly Project projectFailureNoClass = new("Fail", null);

        [TestInitialize]
        public void Initialize()
        {
            generatorService.Setup(x => x.GenerateFiles(
                "Success", It.IsAny<string>(), It.IsAny<List<(string content, string fileName)>>()))
                .Returns("Resources\\Projects\\Success.zip");
        }

        [TestMethod]
        public void ShouldGenerateWithNoError()
        {
            var result = cSharpService.Generate(projectSuccess);
            Assert.IsNotNull(result);
            Assert.AreEqual($"Resources\\Projects\\{projectSuccess.Title}.zip", result);
        }

        [TestMethod]
        public void ShouldThrowErrorNoClass()
        {
            var result = Assert.ThrowsException<Exception>(() => cSharpService.Generate(projectFailureNoClass));            
            Assert.IsNotNull(result);
            Assert.AreEqual("There is no class for this project", result.Message);

        }
    }
}
