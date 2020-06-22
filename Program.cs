using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;
using System.Linq;

using YamlDotNet.RepresentationModel;

namespace Umber
{
    public class Program
    {
        public static int Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        [Option(ShortName = "f")]
        public string FileRef { get; }

        [Option(ShortName = "c")]
        public string ChartVersion { get; }

        private void OnExecute()
        {
            if(string.IsNullOrWhiteSpace(FileRef) || File.Exists(FileRef) == false)
            {
                Console.WriteLine("Template file not found.");
                return;
            }


            var args = ChartVersion.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var charts = args.Where(a => a.Contains('=', StringComparison.OrdinalIgnoreCase)).Select(c =>
            {
                var s = c.Split('=', StringSplitOptions.RemoveEmptyEntries);
                return (Name: s[0], Version: s[1]);
            });

            var yaml = new YamlStream();
            using (var file = File.OpenText(FileRef))
            {
                yaml.Load(file);
            }

            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
            var dependencies = (YamlSequenceNode)mapping.Children[new YamlScalarNode("dependencies")];

            foreach (var dependency in dependencies)
            {
                var name = dependency["name"].ToString();
                var chart = charts.FirstOrDefault(c => c.Name == name);
                
                if(chart != default)
                {
                    ((YamlScalarNode)dependency["version"]).Value = chart.Version;
                }
            }

            using var output = File.OpenWrite(FileRef);
            using var text = new StreamWriter(output);
            yaml.Save(text);
        }
    }
}