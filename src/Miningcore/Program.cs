using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Miningcore
{
    public class Program
    {
        private static object configFile = null;
        private static CommandOption dumpConfigOption;
        private static CommandOption shareRecoveryOption;

        public static void Main(string[] args)
        {

            var app = new CommandLineApplication(false)
            {
                FullName = "MiningCore - Pool Mining Engine",
                ShortVersionGetter = () => $"v{Assembly.GetEntryAssembly().GetName().Version}",
                LongVersionGetter = () => $"v{Assembly.GetEntryAssembly().GetName().Version}"
            };

            var versionOption = app.Option("-v|--version", "Version Information", CommandOptionType.NoValue);
            var configFileOption = app.Option("-c|--config <configfile>", "Configuration File", CommandOptionType.SingleValue);
            dumpConfigOption = app.Option("-dc|--dumpconfig", "Dump the configuration (useful for trouble-shooting typos in the config file)", CommandOptionType.NoValue);
            shareRecoveryOption = app.Option("-rs", "Import lost shares using existing recovery file", CommandOptionType.SingleValue);
            app.HelpOption("-? | -h | --help");

            app.Execute(args);

            if(versionOption.HasValue())
            {
                app.ShowVersion();
            }

            if(!configFileOption.HasValue())
            {
                app.ShowHelp();
            }
            else
            {
                configFile = configFileOption.Value();

                // Start Miningcore PoolCore
                PoolCore.Pool.Start(args);
            }

            

        }

    }
}
