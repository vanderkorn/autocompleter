using System;
using System.Linq;
using Autofac;
using Vdk.AutoCompleter.Common;
using Vdk.AutoCompleter.Common.IOC;
using Vdk.AutoCompleter.Wcf.Client.Models;
using Vdk.AutoCompleter.Wcf.ClientModule;

namespace Vdk.AutoCompleter.Wcf.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            // Parse in 'strict mode', success or quit
            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                CoreInitializer.Initialize(Dependencies);
                using (var scope = ServiceLocator.GetContainer().BeginLifetimeScope())
                {
                    var app = scope.Resolve<IApplicationClient<string>>();
                    app.Connect(options.Host, options.Port);
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
                            var response = app.Get(arr[1]);
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
                }

            }
        }
        private static void Dependencies(ContainerBuilder builder)
        {
            builder.RegisterModule(new WcfClientApplicationModule());
        }
    }
}
