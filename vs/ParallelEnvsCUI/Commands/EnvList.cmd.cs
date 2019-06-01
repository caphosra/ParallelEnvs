using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Capra314Cabra.ParallelEnvs.CUI.Commands
{
    class EnvListCmd : CommandInfo, ICommandWithInfo
    {
        public string CommandName => "envlist";

        public string Description =>
            "It's used for getting the list of envs.\r\n" +
            "usage: envlist";

        public void Execute(List<string> args)
        {
            var envNames = Settings.EnvNames;
            if(envNames.Count == 0)
            {
                Console.WriteLine("No envs which are found.");
            }
            else
            {
                foreach (var envName in envNames)
                {
                    var stream = new EnvStream(Settings.ApplicationWorkDirectoryPath, envName);
                    var env = stream.LoadEnv();
                    Console.WriteLine($" - Name: {env.Name} Active: {(env.Enabled ? "True" : "False")}");
                }
            }
        }
    }
}
