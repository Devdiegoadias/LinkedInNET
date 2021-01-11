﻿
namespace Sparkle.LinkedInNET.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sparkle.LinkedInNET.ServiceDefinition;

    [TestClass]
    public class CSharpGeneratorTests
    {
        [TestMethod]
        public void ImplicitReturnTypes()
        {
            var stream = new System.IO.MemoryStream();
            var writer = new System.IO.StreamWriter(stream);
            var generator = new CSharpGenerator(writer);
            var root = new ApisRoot
            {
                ApiGroups = new List<ApiGroup>()
                {
                    new ApiGroup
                    {
                        Name = "g",
                        Methods = new List<ApiMethod>(),
                        ReturnTypes = new List<ReturnType>()
                        {
                            new ReturnType
                            {
                                Name = "r1",
                                Fields = new List<Field>()
                                {
                                    new Field
                                    {
                                        Name = "f1",
                                    },
                                    new Field
                                    {
                                        Name = "f2:(name)",
                                    },
                                },
                            },
                        },
                    },
                },
            };
            var builder = new ServiceDefinitionBuilder();
            builder.AppendServiceDefinition(new ServiceDefinition(root));
            generator.Run(builder.Definition);

            writer.Flush();
            stream.Seek(0L, System.IO.SeekOrigin.Begin);
            var result = new StreamReader(stream).ReadToEnd();

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains("public class R1"));
            Assert.IsTrue(result.Contains("public class F2"));
            Assert.IsTrue(result.Contains("public string F1"));
            Assert.IsTrue(result.Contains("public string Name"));
        }

        [TestMethod]
        public void ImplicitSubReturnTypes()
        {
            var stream = new System.IO.MemoryStream();
            var writer = new System.IO.StreamWriter(stream);
            var generator = new CSharpGenerator(writer);
            var root = new ApisRoot
            {
                ApiGroups = new List<ApiGroup>()
                {
                    new ApiGroup
                    {
                        Name = "g",
                        Methods = new List<ApiMethod>(),
                        ReturnTypes = new List<ReturnType>()
                        {
                            new ReturnType
                            {
                                Name = "r1",
                                Fields = new List<Field>()
                                {
                                    new Field
                                    {
                                        Name = "f1",
                                    },
                                    new Field
                                    {
                                        Name = "f2:(r2:(name))",
                                    },
                                },
                            },
                        },
                    },
                },
            };
            var builder = new ServiceDefinitionBuilder();
            builder.AppendServiceDefinition(new ServiceDefinition(root));
            generator.Run(builder.Definition);

            writer.Flush();
            stream.Seek(0L, System.IO.SeekOrigin.Begin);
            var result = new StreamReader(stream).ReadToEnd();

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains("public class R1"));
            Assert.IsTrue(result.Contains("public class F2"));
            Assert.IsTrue(result.Contains("public string F1"));
            Assert.IsTrue(result.Contains("public F2 F2"));
            Assert.IsTrue(result.Contains("public string Name"));
        }

        [TestMethod]
        public void ImplicitSubReturnTypes2()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ReturnType Name=""person"" ClassName=""Person"">
      <Field Name=""location:(name)"" />
      <Field Name=""location:(country:(code))"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var serializer = new XmlSerializer(typeof(ApisRoot));
            var reader = new StringReader(xml);
            var root = (ApisRoot)serializer.Deserialize(reader);
            var stream = new System.IO.MemoryStream();
            var writer = new System.IO.StreamWriter(stream);
            var generator = new CSharpGenerator(writer);
            var builder = new ServiceDefinitionBuilder();
            builder.AppendServiceDefinition(new ServiceDefinition(root));
            generator.Run(builder.Definition);

            writer.Flush();
            stream.Seek(0L, System.IO.SeekOrigin.Begin);
            var result = new StreamReader(stream).ReadToEnd();

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
        /*
        [TestMethod, Ignore] // wont implement
        public void ExplicitReturnTypes_int()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ReturnType Name=""person"" ClassName=""Person"">
      <Field Name=""location"" Type=""PersonLocation"" />
      <Field Name=""location:(name)"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains("public class Person"));
            Assert.IsTrue(result.Contains("public class PersonLocation"));
            Assert.IsTrue(result.Contains("public PersonLocation Location"));
            Assert.IsFalse(result.Contains("public class Location"));
            Assert.IsTrue(result.Contains("public string Name"));
        }
        */
        [TestMethod]
        public void ImplicitAndExplicitReturnTypes()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ApiMethod MethodName=""ftw"" ReturnType=""Person"" Path=""/ftw"" />
    <ReturnType Name=""person"" ClassName=""Person"">
      <Field Name=""location"" Type=""PersonLocation"" />
    </ReturnType>
    <ReturnType Name=""location"" ClassName=""PersonLocation"">
      <Field Name=""name"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var serializer = new XmlSerializer(typeof(ApisRoot));
            var reader = new StringReader(xml);
            var root = (ApisRoot)serializer.Deserialize(reader);
            var stream = new System.IO.MemoryStream();
            var writer = new System.IO.StreamWriter(stream);
            var generator = new CSharpGenerator(writer);
            var builder = new ServiceDefinitionBuilder();
            builder.AppendServiceDefinition(new ServiceDefinition(root));
            generator.Run(builder.Definition);

            writer.Flush();
            stream.Seek(0L, System.IO.SeekOrigin.Begin);
            var result = new StreamReader(stream).ReadToEnd();

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains("public class Person"));
            Assert.IsTrue(result.Contains("public class PersonLocation"));
            Assert.IsTrue(result.Contains("public PersonLocation Location"));
            Assert.IsFalse(result.Contains("public class Location"));
            Assert.IsTrue(result.Contains("public string Name"));
        }
        /*
        [TestMethod, Ignore] // wont implement
        public void SubtypeFieldType()
        {
            // the interesting thing here is:
            // we declare a 2-part field of type int
            // this is to hard to implement
            // in this case, separately declare a relation-to-viewer return type with a int field
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ReturnType Name=""person"" ClassName=""Person"">
      <Field Name=""relation-to-viewer:(distance)"" Type=""int"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);


            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains("public class Person"));
            Assert.IsTrue(result.Contains("public class RelationToViewer"));
            Assert.IsTrue(result.Contains("public int Distance"));
        }
        */
        [TestMethod]
        public void UsesAcceptLanguageFalse()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ApiMethod MethodName=""NoAL"" Path=""hello"" ReturnType=""test"" />
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsFalse(result.Contains("string[] acceptLanguages"));
            Assert.IsFalse(result.Contains("context.AcceptLanguages = acceptLanguages"));
        }

        [TestMethod]
        public void UsesAcceptLanguageTrue()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ApiMethod MethodName=""WithAL"" Path=""hello"" ReturnType=""test"" UsesAcceptLanguage=""1"" />
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains("string[] acceptLanguages"));
            Assert.IsTrue(result.Contains("context.AcceptLanguages = acceptLanguages"));
        }

        [TestMethod]
        public void GeneratesFieldSelectorForProperty1()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ReturnType Name=""return"" AutoGenerateFieldSelectors=""1"">
      <Field Name=""location"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains(@"public static FieldSelector<Return> WithLocation(this FieldSelector<Return> me) { return me.Add(""location""); }"));
        }

        [TestMethod]
        public void GeneratesFieldSelectorForProperty2Sub()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ReturnType Name=""return"" AutoGenerateFieldSelectors=""1"">
      <Field Name=""location:(name)"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue (result.Contains(@"public static FieldSelector<Return> WithLocationName(this FieldSelector<Return> me) { return me.Add(""location:(name)""); }"));
            Assert.IsFalse(result.Contains(@"public static FieldSelector<Return> WithLocation(this FieldSelector<Return> me)"));
            Assert.IsFalse(result.Contains(@"(this FieldSelector<Return> me) { return me.Add(""location""); }"));
        }

        [TestMethod]
        public void GeneratesFieldSelectorForProperty2Ext()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ReturnType Name=""return"" AutoGenerateFieldSelectors=""1"">
      <Field Name=""location"" Type=""location"" />
    </ReturnType>
    <ReturnType Name=""location"">
      <Field Name=""name"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsFalse(result.Contains(@"public static FieldSelector<Return> WithLocationName(this FieldSelector<Return> me)"));
            Assert.IsFalse(result.Contains(@"{ return me.Add(""location:(name)""); }"));
            Assert.IsTrue( result.Contains(@"public static FieldSelector<Return> WithLocation(this FieldSelector<Return> me) { return me.Add(""location""); }"));
        }

        [TestMethod]
        public void GeneratesFieldSelectorForProperty2ExtAndFieldSelector()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ReturnType Name=""return"">
      <Field Name=""location"" Type=""location"">
        <FieldSelector Name=""location:(name)"" Title=""the name of the location"" />
      </Field>
    </ReturnType>
    <ReturnType Name=""location"">
      <Field Name=""name"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsFalse(result.Contains(@"public static FieldSelector<Return> WithLocation(this FieldSelector<Return> me)"));
            Assert.IsFalse(result.Contains(@"(this FieldSelector<Return> me) { return me.Add(""location""); }"));
            Assert.IsTrue(result.Contains(@"public static FieldSelector<Return> WithLocationName(this FieldSelector<Return> me) { return me.Add(""location:(name)""); }"));
        }

        [TestMethod]
        public void ReturnTypeHeader()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ReturnType Name=""return"">
      <Header Name=""location"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains(@"public string Location { get; set; }"));
        }

        [TestMethod]
        public void ReturnTypeHeaderWithMethod()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ApiMethod MethodName=""Oops"" Path=""oops"" ReturnType=""return"" />
    <ReturnType Name=""return"">
      <Header Name=""location"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains(@"public string Location { get; set; }"));
            Assert.IsTrue(result.Contains(@"public group.Return Oops"));
            Assert.IsTrue(result.Contains(@"result.Location = this.ReadHeader"));
        }

        [TestMethod]
        public void ReturnTypeHeaderWithMethodDifferentGroup()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""mtd"">
    <ApiMethod MethodName=""Oops"" Path=""oops"" ReturnType=""Rtn.Return"" />
  </ApiGroup>
  <ApiGroup Name=""Rtn"">
    <ReturnType Name=""Return"">
      <Header Name=""location"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains(@"public string Location { get; set; }"));
            Assert.IsTrue(result.Contains(@"public Rtn.Return Oops"));
            Assert.IsTrue(result.Contains(@"result.Location = this.ReadHeader"));
        }

        [TestMethod]
        public void PostReturnType()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""mtd"">
    <ApiMethod MethodName=""Oops"" Path=""oops"" PostReturnType=""Post"" ReturnType=""Return"" />
    <ReturnType Name=""Post"">
      <Header Name=""location"" />
    </ReturnType>
    <ReturnType Name=""Return"">
      <Header Name=""location"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains(@"public mtd.Return Oops"));
            Assert.IsTrue(result.Contains(@", mtd.Post postData"));
        }

        private static string GetGeneratedCodeFromXmlDefinition(string xml)
        {
            ////var serializer = new XmlSerializer(typeof(ApisRoot));
            ////var reader = new StringReader(xml);
            ////var root = (ApisRoot)serializer.Deserialize(reader);

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new System.IO.StreamWriter(stream);
            var generator = new CSharpGenerator(writer);
            var xmlStream = new MemoryStream();
            var xmlWriter = new StreamWriter(xmlStream);
            xmlWriter.Write(xml);
            xmlWriter.Flush();
            xmlStream.Seek(0L, SeekOrigin.Begin);

            var builder = new ServiceDefinitionBuilder();
            ////builder.AppendServiceDefinition(new ServiceDefinition(root));
            builder.AppendServiceDefinition(xmlStream);
            generator.Run(builder.Definition);
            stream.Seek(0L, SeekOrigin.Begin);
            var result = new StreamReader(stream).ReadToEnd();
            return result;
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var stream = new System.IO.MemoryStream();
            var writer = new System.IO.StreamWriter(stream);
            var generator = new CSharpGenerator(writer);
            var root = new ApisRoot
            {
                ApiGroups = new List<ApiGroup>()
                {
                    new ApiGroup
                    {
                        Name = "g",
                        Methods = new List<ApiMethod>()
                        {
                            new ApiMethod
                            {
                                Path = "/v1/test1",
                                MethodName="mtd1",
                            },
                            new ApiMethod
                            {
                                Path = "/v1/test1/{UserId}",
                                MethodName="mtd2",
                            },
                        },
                        ReturnTypes = new List<ReturnType>(),
                    },
                },
            };
            var builder = new ServiceDefinitionBuilder();
            builder.AppendServiceDefinition(new ServiceDefinition(root));
            generator.Run(builder.Definition);

            writer.Flush();
            stream.Seek(0L, System.IO.SeekOrigin.Begin);
            var result = new StreamReader(stream).ReadToEnd();

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains("public void mtd1("));
            Assert.IsTrue(result.Contains("public void mtd2("));
            Assert.IsTrue(result.Contains("string userId"));
        }
        /* won't support that
        [TestMethod]
        public void SubtypeInMultipleFieldsAndType()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ReturnType Name=""return1"">
      <Field Name=""sub1:(name)"" />
      <Field Name=""sub1:(return2)"" Type=""return2"" />
    </ReturnType>
    <ReturnType Name=""return2"">
      <Field Name=""sub2"" />
    </ReturnType>
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));

        }
        */

        [TestMethod]
        public void UrlPathWithBoolVariable()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ApiMethod MethodName=""mymethod"" Path=""/api?isOk={bool isOk}"" RequiresUserAuthentication=""1"" />
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains(@"        , bool isOk"));
            Assert.IsTrue(result.Contains(@"var url = FormatUrl(urlFormat, fields, ""bool isOk"", isOk);"));
        }

        [TestMethod]
        public void UrlPathWithBoolVariableAndDefaultValue()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <ApiGroup Name=""group"">
    <ApiMethod MethodName=""mymethod"" Path=""/api?isOk={bool isOk = false}"" RequiresUserAuthentication=""1"" />
  </ApiGroup>
</Root>";
            var result = GetGeneratedCodeFromXmlDefinition(xml);
            result = result.Replace(Environment.NewLine, string.Empty);

            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Contains(@"        , bool isOk = false"));
            Assert.IsTrue(result.Contains(@"var url = FormatUrl(urlFormat, fields, ""bool isOk = false"", isOk);"));
        }

        [TestClass]
        public class GetUrlPathParametersMethod
        {
            [TestMethod]
            public void BasicVariable()
            {
                var path = "hello{World}?nice";
                var target = new TestCSharpGenerator();
                var items = target.InvokeGetUrlPathParameters(path);

                Assert.AreEqual(1, items.Count);
                Assert.IsTrue(items.ContainsKey("World"));
                Assert.AreEqual("World", items["World"].OriginalName);
                Assert.AreEqual("World", items["World"].Name);
                Assert.IsNull(items["World"].Type);
            }

            [TestMethod]
            public void BasicVariableWithDefaultValue()
            {
                var path = "hello{World = \"hello world\"}?nice";
                var target = new TestCSharpGenerator();
                var items = target.InvokeGetUrlPathParameters(path);

                Assert.AreEqual(1, items.Count);
                Assert.IsTrue(items.ContainsKey("World"));
                Assert.AreEqual("World = \"hello world\"", items["World"].OriginalName);
                Assert.AreEqual("World", items["World"].Name);
                Assert.AreEqual("\"hello world\"", items["World"].Value);
                Assert.IsNull(items["World"].Type);
            }

            [TestMethod]
            public void TypedVariable()
            {
                var path = "hello{DateTime World}?nice";
                var target = new TestCSharpGenerator();
                var items = target.InvokeGetUrlPathParameters(path);

                Assert.AreEqual(1, items.Count);
                Assert.IsTrue(items.ContainsKey("World"));
                Assert.AreEqual("DateTime World", items["World"].OriginalName);
                Assert.AreEqual("World", items["World"].Name);
                Assert.AreEqual("DateTime", items["World"].Type);
            }

            [TestMethod]
            public void TypedVariable2()
            {
                var path = "hello{int id}?nice";
                var target = new TestCSharpGenerator();
                var items = target.InvokeGetUrlPathParameters(path);

                Assert.AreEqual(1, items.Count);
                Assert.IsTrue(items.ContainsKey("id"));
                Assert.AreEqual("int id", items["id"].OriginalName);
                Assert.AreEqual("id", items["id"].Name);
                Assert.AreEqual("int", items["id"].Type);
            }

            [TestMethod]
            public void TypedVariable2WithDefaultValue()
            {
                var path = "hello{int id = 12}?nice";
                var target = new TestCSharpGenerator();
                var items = target.InvokeGetUrlPathParameters(path);

                Assert.AreEqual(1, items.Count);
                Assert.IsTrue(items.ContainsKey("id"));
                Assert.AreEqual("int id = 12", items["id"].OriginalName);
                Assert.AreEqual("id", items["id"].Name);
                Assert.AreEqual("int", items["id"].Type);
                Assert.AreEqual("12", items["id"].Value);
            }

            [TestMethod]
            public void TypedAndSimpleVariable()
            {
                var path = "hello{DateTime World}?nice{test}";
                var target = new TestCSharpGenerator();
                var items = target.InvokeGetUrlPathParameters(path);

                Assert.AreEqual(2, items.Count);
                Assert.IsTrue(items.ContainsKey("World"));
                Assert.AreEqual("DateTime World", items["World"].OriginalName);
                Assert.AreEqual("World", items["World"].Name);
                Assert.AreEqual("DateTime", items["World"].Type);
                Assert.IsTrue(items.ContainsKey("test"));
                Assert.AreEqual("test", items["test"].OriginalName);
                Assert.AreEqual("test", items["test"].Name);
                Assert.IsNull(items["test"].Type);
            }

            [TestMethod]
            public void BoolVariableWithDefaultValue()
            {
                var path = "hello{bool World = false}?nice";
                var target = new TestCSharpGenerator();
                var items = target.InvokeGetUrlPathParameters(path);

                Assert.AreEqual(1, items.Count);
                Assert.IsTrue(items.ContainsKey("World"));
                Assert.AreEqual("bool World = false", items["World"].OriginalName);
                Assert.AreEqual("World", items["World"].Name);
                Assert.AreEqual("bool", items["World"].Type);
                Assert.AreEqual("false", items["World"].Value);
            }
        }

        public class TestCSharpGenerator : CSharpGenerator
        {
            public TestCSharpGenerator()
                : base(null)
            {
            }

            internal IDictionary<string, Parameter> InvokeGetUrlPathParameters(string path, NameTransformation transform = NameTransformation.None)
            {
                return base.GetUrlPathParameters(path, transform);
            }
        }
    }
}
