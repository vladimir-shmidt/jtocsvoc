using CommandLine;
using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace gmtinterview
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser(config => config.HelpWriter = Console.Out);
            parser.ParseArguments<Options>(args)
            .WithParsed<Options>(opts => ConvertData(opts))
            .WithNotParsed<Options>(errs => HandleParseError(errs));

            Console.ReadLine();
        }

        static void ConvertData(Options opt)
        {
            using (var converter = new DataConverter(new HttpWeatherForcastClient(opt.Host, 3, new CoreHttpClient(new HttpClient())), new JsonToCsvSerializer(), new FileWriter(File.OpenWrite(opt.Output))))
            {
                try
                {
                    var task = converter.Convert();
                    task.Wait();
                }
                catch (AggregateException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        static void HandleParseError(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                //some errors handle here
            }
        }
    }
}
