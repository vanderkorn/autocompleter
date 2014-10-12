using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;
using Vdk.AutoCompleter.Thrift.Client.Models;
using Vdk.AutoCompleter.Thrift.Core;

namespace Vdk.AutoCompleter.Thrift.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            // Parse in 'strict mode', success or quit
            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                TTransport transport = new TSocket(options.Host, options.Port);
                transport.Open();

                var proto = new TBinaryProtocol(transport);
                var client = new AutoCompleteService.Client(proto);     

             
                while (true)
                {
                    var line = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        if (line == "exit")
                            break;
                        var arr = line.Split(' ');
                        if (arr.Length < 2 || arr[0] != "get")
                            continue;
                        var response = client.Get(arr[1]);
                        if (response != null)
                        {
                            if (response.Any())
                            {
                                foreach (var val in response)
                                    Console.WriteLine(val);
                                Console.WriteLine();
                            }
                        }
                    }

                }
                client.Dispose();
            }
        }
    }
}
