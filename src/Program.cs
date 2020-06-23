using System;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;

namespace PushToApi {
    class Program {
        static string TemplateFileName(string[] args) {
            if (args.Length < 1) {
                throw new PushToApiException("Template file name is expected as 1st argument.");
            }
            return args[0];
        }

        static void Main(string[] args) {
            var template = File.ReadAllText(TemplateFileName(args));
            var result = Engine
                .Razor
                .RunCompile(template,
                    "templateKey",
                    null,
                    new {
                        Name = "World"
                    });

            Console.WriteLine(result);
        }
    }
}
