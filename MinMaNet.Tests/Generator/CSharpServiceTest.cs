using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinMaNet.Domain.Models;
using MinMaNet.Generator.Languages;
using System;
using System.Collections.Generic;

namespace MinMaNet.Tests.Generator
{
    [TestClass]
    public class CSharpServiceTest
    {
        private readonly CSharpService cSharpService = new();
        private readonly Project projectSuccess = new("Success",
            new List<Class> { 
                new Class("Class1", new List<Property> { new Property("Prop1", "Type1") }),
                new Class("Class2", new List<Property> { new Property("Prop2", "Type2") }),
            });
        private readonly Project projectFailureNoClass = new("Fail", null);

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
