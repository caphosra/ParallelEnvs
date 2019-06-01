using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Capra314Cabra.ParallelEnvs.CUI.Commands
{
    class CreateCmd : CommandInfo, ICommandWithInfo
    {
        public string CommandName => "create";

        public string Description =>
            "Create a new env\r\n" +
            "usage: create [envName]";

        public void Execute(List<string> args)
        {
            if(args.Count == 0)
            {
                Console.WriteLine(Description);
                return;
            }

            var envName = args[0];
            var env = EnvManager.Generate(envName);

            Settings.EnvNames.Add(env.Name);
            Settings.SaveToFile();

            var stream = new EnvStream(Settings.ApplicationWorkDirectoryPath, env.Name);
            stream.WriteEnv(env);

            Console.WriteLine("Created");
        }
    }
}
