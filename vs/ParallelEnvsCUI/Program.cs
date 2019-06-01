//
//
// Program.cs
//
//
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Com.Capra314Cabra.ParallelEnvs.CUI.Commands;

namespace Com.Capra314Cabra.ParallelEnvs.CUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----- ParallelEnvs -----");
            Console.WriteLine("© 2019 Akihisa Yagi");
            Console.WriteLine();

            InitalizeSettings(out var settings);

            CommandStore.Initalize(settings);

            while(true)
            {
                Console.Write("Envs > ");
                var inputs = Separate(Console.ReadLine()).ToArray();

                if(inputs.Length == 0)
                {
                    continue;
                }

                if(inputs[0] == "exit")
                {
                    break;
                }

                Console.WriteLine();

                if (inputs[0] == "help")
                {
                    if(inputs.Length == 1)
                    {
                        foreach(var cmd in CommandStore.Commands)
                        {
                            Console.WriteLine($" - {cmd.CommandName}");
                        }
                    }
                    else
                    {
                        var commandName = inputs[1];
                        var helpText = CommandStore.Commands
                            .Where(cmd => (cmd.CommandName == commandName))
                            .Select(cmd => cmd.Description)
                            .First();

                        Console.WriteLine(helpText);
                    }
                }

                var command = CommandStore.Commands
                    .Where(cmd => (cmd.CommandName == inputs[0]))
                    .First();
                command.Execute(inputs.Skip(1).ToList());

                Console.WriteLine();
            }
        }

        static void InitalizeSettings(out IParallelEnvsSettings settings)
        {
            settings = new ParallelEnvsSettings();

            if (File.Exists(Path.Combine(settings.ApplicationWorkDirectoryPath, ParallelEnvsSettings.FILE_NAME)))
            {
                settings.LoadFromFile();
            }
        }
        
        static IEnumerable<string> Separate(string input)
        {
            var output = new List<string>();

            var sb = new StringBuilder();
            var isPath = false;
            foreach(var character in input)
            {
                if(!isPath && character == ' ')
                {
                    output.Add(sb.ToString());
                    sb.Clear();
                }
                else if(character == '\"')
                {
                    isPath = !isPath;
                }
                else
                {
                    sb.Append(character);
                }
            }
            output.Add(sb.ToString());

            return output.Where(str => !(str == null || str == ""));
        }
    }
}
