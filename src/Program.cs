using System;
using RazorEngine;
using RazorEngine.Templating;

namespace PushToApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string template = "Hello @Model.Name!";
            var result = Engine
                .Razor
                .RunCompile(template,
                    "templateKey",
                    null,
                    new
                    {
                        Name = "World"
                    });

            Console.WriteLine(result);        }
    }
}
