using CommandLine;

namespace Vdk.AutoCompleter.Wcf.Client.Models
{
    public class Options
    {
        [Option('h', "host", Required = true, HelpText = "Input host.")]
        public string Host { get; set; }

        [Option('p', "port", Required = true, HelpText = "Input port.")]
        public int Port { get; set; }
    }
}
