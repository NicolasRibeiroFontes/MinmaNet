﻿using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinMaNet.Reader.Tools;
using Moq;
using System;
using System.IO;

namespace MinMaNet.Tests.Reader
{
    [TestClass]
    public class MindMapsAppServiceTest
    {
        readonly Mock<IFormFile> fileSuccess = new();
        readonly MindMapsAppService target = new();
        readonly Mock<IFormFile> fileErrorNoProperty = new();
        readonly Mock<IFormFile> fileErrorNoType = new();

        [TestInitialize]
        public void Initialize()
        {
            //Setup mock file sucess
            var contentSuccess = "{\"id\":\"bd70f701-1b8a-464d-974f-0f60599b2889\",\"title\":\"Store\",\"mindmap\":{\"root\":{\"id\":\"40466f59-aea7-43be-b396-c3a3ae2dd1f2\",\"parentId\":null,\"text\":{\"caption\":\"Store\",\"font\":{\"style\":\"normal\",\"weight\":\"bold\",\"decoration\":\"none\",\"size\":20,\"color\":\"#000000\"}},\"offset\":{\"x\":0,\"y\":0},\"foldChildren\":false,\"branchColor\":\"#000000\",\"children\":[{\"id\":\"a8ca8fc0-a31c-4117-8552-35c948a29e65\",\"parentId\":\"40466f59-aea7-43be-b396-c3a3ae2dd1f2\",\"text\":{\"caption\":\"Product\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":133.546875,\"y\":68.734375},\"foldChildren\":false,\"branchColor\":\"#2a7f47\",\"children\":[{\"id\":\"4f3f0751-fc8a-4a34-a16e-8b7a4107b34f\",\"parentId\":\"a8ca8fc0-a31c-4117-8552-35c948a29e65\",\"text\":{\"caption\":\"Name\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":154.09375,\"y\":-10},\"foldChildren\":false,\"branchColor\":\"#2a7f47\",\"children\":[{\"id\":\"1e200d91-7f08-4fab-8a07-1ec07c604346\",\"parentId\":\"4f3f0751-fc8a-4a34-a16e-8b7a4107b34f\",\"text\":{\"caption\":\"string\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":157.31676659851405,\"y\":2},\"foldChildren\":false,\"branchColor\":\"#2a7f47\",\"children\":[]}]},{\"id\":\"1b399b77-b432-44bc-9485-f2215ae49da6\",\"parentId\":\"a8ca8fc0-a31c-4117-8552-35c948a29e65\",\"text\":{\"caption\":\"Price\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":155.421875,\"y\":43},\"foldChildren\":false,\"branchColor\":\"#2a7f47\",\"children\":[{\"id\":\"dbd01926-f285-4ad0-a96f-d6fded06a2b7\",\"parentId\":\"1b399b77-b432-44bc-9485-f2215ae49da6\",\"text\":{\"caption\":\"decimal\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":153.28025039409664,\"y\":0},\"foldChildren\":false,\"branchColor\":\"#2a7f47\",\"children\":[]}]},{\"id\":\"68ab7a9e-a039-4549-81ce-30dfb1d05972\",\"parentId\":\"a8ca8fc0-a31c-4117-8552-35c948a29e65\",\"text\":{\"caption\":\"Description\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":156.75,\"y\":96},\"foldChildren\":false,\"branchColor\":\"#2a7f47\",\"children\":[{\"id\":\"f7edf15c-a1f8-403e-900d-fb63bcc1aa49\",\"parentId\":\"68ab7a9e-a039-4549-81ce-30dfb1d05972\",\"text\":{\"caption\":\"string\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":151.45775905435218,\"y\":-1},\"foldChildren\":false,\"branchColor\":\"#2a7f47\",\"children\":[]}]},{\"id\":\"da10355d-6b92-4e0a-bafd-82c4d0b440ab\",\"parentId\":\"a8ca8fc0-a31c-4117-8552-35c948a29e65\",\"text\":{\"caption\":\"CategoryId\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":157.640625,\"y\":139},\"foldChildren\":false,\"branchColor\":\"#2a7f47\",\"children\":[{\"id\":\"4c30238e-0055-48ba-81ff-2cc783a1a935\",\"parentId\":\"da10355d-6b92-4e0a-bafd-82c4d0b440ab\",\"text\":{\"caption\":\"int\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":158.5804987044437,\"y\":3},\"foldChildren\":false,\"branchColor\":\"#2a7f47\",\"children\":[]}]}]},{\"id\":\"a13e58e5-736e-4f6f-81bf-07024c89031f\",\"parentId\":\"40466f59-aea7-43be-b396-c3a3ae2dd1f2\",\"text\":{\"caption\":\"Customer\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":125.359375,\"y\":-113.96875},\"foldChildren\":false,\"branchColor\":\"#9cffcb\",\"children\":[{\"id\":\"713cd13d-da5d-48b3-9c88-85488a2bb333\",\"parentId\":\"a13e58e5-736e-4f6f-81bf-07024c89031f\",\"text\":{\"caption\":\"Name\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":142.84375,\"y\":-35},\"foldChildren\":false,\"branchColor\":\"#9cffcb\",\"children\":[{\"id\":\"f2a9f8a0-3cc8-4e4a-b321-cef57fb78350\",\"parentId\":\"713cd13d-da5d-48b3-9c88-85488a2bb333\",\"text\":{\"caption\":\"string\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":159.1222292797935,\"y\":-4},\"foldChildren\":false,\"branchColor\":\"#9cffcb\",\"children\":[]}]},{\"id\":\"a957aa45-b0a5-482a-8d44-e72a8df57366\",\"parentId\":\"a13e58e5-736e-4f6f-81bf-07024c89031f\",\"text\":{\"caption\":\"Email\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":142.78125,\"y\":3},\"foldChildren\":false,\"branchColor\":\"#9cffcb\",\"children\":[{\"id\":\"9f069979-af5c-4aa9-93e0-00b57d70baaf\",\"parentId\":\"a957aa45-b0a5-482a-8d44-e72a8df57366\",\"text\":{\"caption\":\"string\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":155.0567589244171,\"y\":4},\"foldChildren\":false,\"branchColor\":\"#9cffcb\",\"children\":[]}]},{\"id\":\"3ca87856-2b87-461d-9ac1-248a5f549b8d\",\"parentId\":\"a13e58e5-736e-4f6f-81bf-07024c89031f\",\"text\":{\"caption\":\"Birth\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":146.59375,\"y\":40},\"foldChildren\":false,\"branchColor\":\"#9cffcb\",\"children\":[{\"id\":\"e8bbfb02-1c7c-4d90-b9cc-41c50f45a497\",\"parentId\":\"3ca87856-2b87-461d-9ac1-248a5f549b8d\",\"text\":{\"caption\":\"DateTime\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":152.5428224953045,\"y\":-3},\"foldChildren\":false,\"branchColor\":\"#9cffcb\",\"children\":[]}]}]},{\"id\":\"56aa53bb-7698-4c9f-9a0d-0c1b08e26972\",\"parentId\":\"40466f59-aea7-43be-b396-c3a3ae2dd1f2\",\"text\":{\"caption\":\"Category\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":-203.328125,\"y\":-13.203125},\"foldChildren\":false,\"branchColor\":\"#5ef064\",\"children\":[{\"id\":\"7e675807-9d42-4bd4-9d7f-7c6b857e8a7e\",\"parentId\":\"56aa53bb-7698-4c9f-9a0d-0c1b08e26972\",\"text\":{\"caption\":\"Name\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":-151.94607786573835,\"y\":2},\"foldChildren\":false,\"branchColor\":\"#5ef064\",\"children\":[{\"id\":\"285200b3-3653-4472-8fb7-a0592ddf3ba2\",\"parentId\":\"7e675807-9d42-4bd4-9d7f-7c6b857e8a7e\",\"text\":{\"caption\":\"string\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":-159.5431649642784,\"y\":2},\"foldChildren\":false,\"branchColor\":\"#5ef064\",\"children\":[]}]},{\"id\":\"dc5ba06a-40b0-4061-bd19-593deb33dda6\",\"parentId\":\"56aa53bb-7698-4c9f-9a0d-0c1b08e26972\",\"text\":{\"caption\":\"Description\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":-147.484375,\"y\":51},\"foldChildren\":false,\"branchColor\":\"#5ef064\",\"children\":[{\"id\":\"f516717e-7a98-4461-89ad-81845f5930d3\",\"parentId\":\"dc5ba06a-40b0-4061-bd19-593deb33dda6\",\"text\":{\"caption\":\"string\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":-151.5952582915234,\"y\":-2},\"foldChildren\":false,\"branchColor\":\"#5ef064\",\"children\":[]}]}]}]}},\"dates\":{\"created\":1637254610423,\"modified\":1637254851668},\"dimensions\":{\"x\":4000,\"y\":2000},\"autosave\":false}";
            var contentErrorNoProperty = "{\"id\":\"acd8e066-36cb-47cd-bd68-e8951807ebad\",\"title\":\"StoreError1\",\"mindmap\":{\"root\":{\"id\":\"90c0da0d-e5b6-47d4-9d3e-77948e1f641c\",\"parentId\":null,\"text\":{\"caption\":\"StoreError1\",\"font\":{\"style\":\"normal\",\"weight\":\"bold\",\"decoration\":\"none\",\"size\":20,\"color\":\"#000000\"}},\"offset\":{\"x\":0,\"y\":0},\"foldChildren\":false,\"branchColor\":\"#000000\",\"children\":[{\"id\":\"aaa590e2-6e45-47f7-9dc3-184f40acff18\",\"parentId\":\"90c0da0d-e5b6-47d4-9d3e-77948e1f641c\",\"text\":{\"caption\":\"Product\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":115.625,\"y\":-9.53125},\"foldChildren\":false,\"branchColor\":\"#68242b\",\"children\":[{\"id\":\"823333a7-3338-425e-b717-14f5aabae481\",\"parentId\":\"aaa590e2-6e45-47f7-9dc3-184f40acff18\",\"text\":{\"caption\":\"Name\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":110.453125,\"y\":-23},\"foldChildren\":false,\"branchColor\":\"#68242b\",\"children\":[{\"id\":\"6e388b31-adb3-4d7b-a0ef-dbe063c84f5e\",\"parentId\":\"823333a7-3338-425e-b717-14f5aabae481\",\"text\":{\"caption\":\"string\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":99.453125,\"y\":1},\"foldChildren\":false,\"branchColor\":\"#68242b\",\"children\":[]}]},{\"id\":\"72e5a882-f64e-489e-b72a-97e8b25e4d8a\",\"parentId\":\"aaa590e2-6e45-47f7-9dc3-184f40acff18\",\"text\":{\"caption\":\"Description\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":109.1875,\"y\":36},\"foldChildren\":false,\"branchColor\":\"#68242b\",\"children\":[{\"id\":\"90aec2cd-ee3c-4b56-a2d2-50ba33a8278c\",\"parentId\":\"72e5a882-f64e-489e-b72a-97e8b25e4d8a\",\"text\":{\"caption\":\"string\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":100.171875,\"y\":0},\"foldChildren\":false,\"branchColor\":\"#68242b\",\"children\":[]}]}]},{\"id\":\"eede9276-2a03-4cab-a044-4c04ca2cfa89\",\"parentId\":\"90c0da0d-e5b6-47d4-9d3e-77948e1f641c\",\"text\":{\"caption\":\"User\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":-175.25,\"y\":-5.34375},\"foldChildren\":false,\"branchColor\":\"#e8b2f1\",\"children\":[]}]}},\"dates\":{\"created\":1647339658436,\"modified\":1647339736376},\"dimensions\":{\"x\":4000,\"y\":2000},\"autosave\":false}";
            var contentErrorNoType = "{\"id\":\"acd8e066-36cb-47cd-bd68-e8951807ebad\",\"title\":\"StoreError1\",\"mindmap\":{\"root\":{\"id\":\"90c0da0d-e5b6-47d4-9d3e-77948e1f641c\",\"parentId\":null,\"text\":{\"caption\":\"StoreError1\",\"font\":{\"style\":\"normal\",\"weight\":\"bold\",\"decoration\":\"none\",\"size\":20,\"color\":\"#000000\"}},\"offset\":{\"x\":0,\"y\":0},\"foldChildren\":false,\"branchColor\":\"#000000\",\"children\":[{\"id\":\"aaa590e2-6e45-47f7-9dc3-184f40acff18\",\"parentId\":\"90c0da0d-e5b6-47d4-9d3e-77948e1f641c\",\"text\":{\"caption\":\"Product\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":115.625,\"y\":-9.53125},\"foldChildren\":false,\"branchColor\":\"#68242b\",\"children\":[{\"id\":\"823333a7-3338-425e-b717-14f5aabae481\",\"parentId\":\"aaa590e2-6e45-47f7-9dc3-184f40acff18\",\"text\":{\"caption\":\"Name\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":110.453125,\"y\":-23},\"foldChildren\":false,\"branchColor\":\"#68242b\",\"children\":[{\"id\":\"6e388b31-adb3-4d7b-a0ef-dbe063c84f5e\",\"parentId\":\"823333a7-3338-425e-b717-14f5aabae481\",\"text\":{\"caption\":\"string\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":99.453125,\"y\":1},\"foldChildren\":false,\"branchColor\":\"#68242b\",\"children\":[]}]},{\"id\":\"72e5a882-f64e-489e-b72a-97e8b25e4d8a\",\"parentId\":\"aaa590e2-6e45-47f7-9dc3-184f40acff18\",\"text\":{\"caption\":\"Description\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":109.1875,\"y\":36},\"foldChildren\":false,\"branchColor\":\"#68242b\",\"children\":[{\"id\":\"90aec2cd-ee3c-4b56-a2d2-50ba33a8278c\",\"parentId\":\"72e5a882-f64e-489e-b72a-97e8b25e4d8a\",\"text\":{\"caption\":\"string\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":100.171875,\"y\":0},\"foldChildren\":false,\"branchColor\":\"#68242b\",\"children\":[]}]}]},{\"id\":\"eede9276-2a03-4cab-a044-4c04ca2cfa89\",\"parentId\":\"90c0da0d-e5b6-47d4-9d3e-77948e1f641c\",\"text\":{\"caption\":\"User\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":-175.25,\"y\":-5.34375},\"foldChildren\":false,\"branchColor\":\"#e8b2f1\",\"children\":[{\"id\":\"ee66e29b-0bbe-4402-96cc-79c2ceb9ec4a\",\"parentId\":\"eede9276-2a03-4cab-a044-4c04ca2cfa89\",\"text\":{\"caption\":\"Name\",\"font\":{\"style\":\"normal\",\"weight\":\"normal\",\"decoration\":\"none\",\"size\":15,\"color\":\"#000000\"}},\"offset\":{\"x\":-106.828125,\"y\":1},\"foldChildren\":false,\"branchColor\":\"#e8b2f1\",\"children\":[]}]}]}},\"dates\":{\"created\":1647339658436,\"modified\":1647339759845},\"dimensions\":{\"x\":4000,\"y\":2000},\"autosave\":false}";
            var fileName = "test.json";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(contentSuccess);
            writer.Flush();
            ms.Position = 0;
            fileSuccess.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileSuccess.Setup(_ => _.FileName).Returns(fileName);
            fileSuccess.Setup(_ => _.Length).Returns(ms.Length);

            ms = new MemoryStream();
            writer = new StreamWriter(ms);
            writer.Write(contentErrorNoProperty);
            writer.Flush();
            ms.Position = 0;
            fileErrorNoProperty.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileErrorNoProperty.Setup(_ => _.FileName).Returns(fileName);
            fileErrorNoProperty.Setup(_ => _.Length).Returns(ms.Length);

            ms = new MemoryStream();
            writer = new StreamWriter(ms);
            writer.Write(contentErrorNoType);
            writer.Flush();
            ms.Position = 0;
            fileErrorNoType.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileErrorNoType.Setup(_ => _.FileName).Returns(fileName);
            fileErrorNoType.Setup(_ => _.Length).Returns(ms.Length);
        }

        [TestMethod]
        public void ShouldCreateObjectWithNoErrors()
        {            
            var result = target.GenerateCommonModelFromJsonFile(fileSuccess.Object);
            Assert.IsNotNull(result);
            Assert.AreEqual("Store", result.Result.Title);
        }

        [TestMethod]
        public void ShouldThrowErrorNoProperty()
        {
            var result = Assert.ThrowsException<Exception>(() => target.GenerateCommonModelFromJsonFile(fileErrorNoProperty.Object));            
            Assert.IsNotNull(result);
            Assert.AreEqual("The module User has no properties defined", result.Message);
        }

        [TestMethod]
        public void ShouldThrowErrorNoType()
        {
            var result = Assert.ThrowsException<Exception>(() => target.GenerateCommonModelFromJsonFile(fileErrorNoType.Object));
            Assert.IsNotNull(result);
            Assert.AreEqual("All the properties must to have only 1 type! Error with the property: User.Name", result.Message);
        }


    }
}