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

        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.WriteLine("Template file name is expected as 1st argument.");
                return;
            }
            var template = File.ReadAllText(TemplateFileName(args));
            var targetName = TargetName(args);
            var templateKey = targetName;
            // Console.WriteLine($"Target Name:{targetName}");

            var m = new Model() {
                Now = DateTime.Now,
                Uuid = Guid.NewGuid(),
                Ext = new ExpandoObject()
            };
            m.Ext.Name = "World";

            var result = Engine
                .Razor
                .RunCompile(template,
                    templateKey,
                    null,
                    m);

            Console.WriteLine(result);
        }
    }
}
