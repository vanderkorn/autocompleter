﻿using CommandLine;

namespace Vdk.AutoCompleter.TestClient.Models
{
    public class Options
    {
        [Option('r', "read", Required = false, HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }
    }
}
