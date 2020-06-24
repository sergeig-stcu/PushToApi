using System;
using System.Dynamic;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;
using System.Threading.Tasks;

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

        static string ConfigFileName(string[] args) {
            return args[1];
        }

        static Model ReadModel(string[] args) {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var text = File.ReadAllText(ModelFileName(args));
            var rv = deserializer.Deserialize<Model>(text);
            return rv;
        }

        static Config ReadConfig(string[] args) {
            var deserializer = new YamlDotNet.Serialization.Deserializer();
            var text = File.ReadAllText(ConfigFileName(args));
            var rv = deserializer.Deserialize<Config>(text);
            return rv;
        }
        static String Describe<T>(T m) {
            var serializer = new YamlDotNet.Serialization.Serializer();
            var rv = serializer.Serialize(m);
            return rv;
        }

        static void PromptToContinue() {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        static async Task<int> Main(string[] args) {
            if (args.Length < 2) {
                Console.WriteLine("SYNTAX: template_file_name config_file_name");
                return 1;
            }
            var template = File.ReadAllText(TemplateFileName(args));
            var targetName = TargetName(args);
            var templateKey = targetName;

            var model = ReadModel(args);
            model.Init();
            Console.WriteLine("    ==Using Model==");
            Console.WriteLine(Describe(model));

            var config = ReadConfig(args);
            Console.WriteLine("    ==Usisng Config==");
            Console.WriteLine(Describe(config));

            PromptToContinue();

            var payload = Engine
                .Razor
                .RunCompile(template,
                    templateKey,
                    null,
                    model);

            Console.WriteLine(payload);

            PromptToContinue();

            int errCode = await RestPusher.SendMessageAsync(config, model, payload);
            return errCode;
        }

    } // end of class

} // end of namespace

/*
            var m = new Model() {
                Now = DateTime.Now,
                Uuid = Guid.NewGuid(),
                Ext = new ExpandoObject()
            };
            m.Ext.Name = "World";
*/
