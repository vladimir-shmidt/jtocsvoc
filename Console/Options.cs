using CommandLine;

namespace gmtinterview
{
    public class Options
    {
        [Option('h', "host", Required = true, HelpText = "Host URL where to download data from")]
        public string Host { get; set; }

        [Option('o', "output", Required = true, HelpText = "Result csv file path")]
        public string Output { get; set; }
    }
}
