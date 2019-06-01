//
//
// FileList.cmd.cs
//
//
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Capra314Cabra.ParallelEnvs.CUI.Commands
{
    class FileListCmd : CommandInfo, ICommandWithInfo
    {
        public string CommandName => "filelist";

        public string Description =>
            "It's used for getting the list of files which are included in the env.\r\n" +
            "usage: filelist [envName]";

        public void Execute(List<string> args)
        {
            if(args.Count == 0)
            {
                Console.WriteLine(Description);
                return;
            }

            var envName = args[0];
            try
            {
                var stream = new EnvStream(Settings.ApplicationWorkDirectoryPath, envName);
                var env = stream.LoadEnv();
                if (env.Files.Count == 0)
                {
                    Console.WriteLine("No files in this env.");
                }
                else
                {
                    foreach (var file in env.Files)
                    {
                        Console.WriteLine($" - {file.OrdinaryFilePath}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
