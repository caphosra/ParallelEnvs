using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Capra314Cabra.ParallelEnvs.CUI.Commands
{
    class AddCmd : CommandInfo, ICommandWithInfo
    {
        public string CommandName => "add";

        public string Description =>
            "Add the files to the env.\r\n" +
            "usage: add [envName] [fileName]";

        public void Execute(List<string> args)
        {
            if(args.Count < 2)
            {
                Console.WriteLine(Description);
                return;
            }

            var stream = new EnvStream(Settings.ApplicationWorkDirectoryPath, args[0]);

            var env = stream.LoadEnv();
            {
                EnvManager.AddFile(ref env, args[1], Settings);
            }
            stream.WriteEnv(env);

            Console.WriteLine($"Add {args[1]} to {args[0]}");
        }
    }
}
