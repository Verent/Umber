using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;
using System.Linq;

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

            var file = File.OpenText(FileRef);



            var args = ChartVersion.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var charts = args.Where(a => a.Contains('=', StringComparison.OrdinalIgnoreCase)).Select(c =>
            {
                var s = c.Split('=', StringSplitOptions.RemoveEmptyEntries);
                return (Chart: s[0], Version: s[1]);
            });

            foreach (var definition in charts)
            {
                Console.WriteLine($"{definition.Chart} : {definition.Version}");
            }


            //var subject = Subject ?? "world";
            Console.WriteLine($"Hello!");

        }
    }
}