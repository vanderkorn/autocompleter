using CommandLine;

namespace Vdk.AutoCompleter.Wcf.Server.Models
{
    public class Options
    {
        [Option('r', "read", Required = true, HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option('p', "port", Required = true, HelpText = "Input port.")]
        public int Port { get; set; }
    }
}
