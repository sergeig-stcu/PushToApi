using System;
using System.Dynamic;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;

namespace PushToApi {
    class Program {
        static string TemplateFileName(string[] args) {
            return args[0];
        }

        static string TargetName(string[] args) {
            // if args[0] value is "templates/hello.txt", returns "hello".
            return Path.GetFileNameWithoutExtension(args[0]);
        }

        static string ModelFileName(string[] args) {
            return $"templates/{TargetName(args)}.yaml";
        }

        static Model ReadModel(string[] args) {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var text = File.ReadAllText(ModelFileName(args));
            var rv = deserializer.Deserialize<Model>(text);
            return rv;
        }

        static String DescribeModel(Model m) {
            var serializer = new YamlDotNet.Serialization.Serializer();
            var rv = serializer.Serialize(m);
            return rv;
        }

        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.WriteLine("Template file name is expected as 1st argument.");
                return;
            }
            var template = File.ReadAllText(TemplateFileName(args));
            var targetName = TargetName(args);
            var templateKey = targetName;

            var model = ReadModel(args);
            model.Init();
            Console.WriteLine("Using Model:");
            Console.WriteLine();
            Console.WriteLine(DescribeModel(model));

            Console.WriteLine("Proceed?");
            Console.ReadLine();

            var result = Engine
                .Razor
                .RunCompile(template,
                    templateKey,
                    null,
                    model);

            Console.WriteLine(result);
        }
    }
}

/*
            var m = new Model() {
                Now = DateTime.Now,
                Uuid = Guid.NewGuid(),
                Ext = new ExpandoObject()
            };
            m.Ext.Name = "World";
*/
