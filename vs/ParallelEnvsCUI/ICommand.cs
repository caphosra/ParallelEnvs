//
//
// ICommand.cs
//
//
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Capra314Cabra.ParallelEnvs.CUI.Commands
{
    interface ICommand
    {
        string CommandName { get; }
        string Description { get; }
        void Execute(List<string> args);
    }

    interface ICommandInfo
    {
        void AttachInfo(IParallelEnvsSettings settings);
    }

    class CommandInfo : ICommandInfo
    {
        protected IParallelEnvsSettings Settings { get; private set; }

        public void AttachInfo(IParallelEnvsSettings settings)
        {
            Settings = settings;
        }
    }

    interface ICommandWithInfo : ICommand, ICommandInfo
    {

    }

    static class CommandStore
    {
        public static List<ICommand> Commands { get; private set; }

        public static void Initalize(IParallelEnvsSettings settings)
        {
            Commands = new List<ICommand>();

            var commands = new ICommandWithInfo[] 
            {
                new FileListCmd(),
                new EnvListCmd(),
                new CreateCmd(),
                new AddCmd()
            };

            AddCommands(settings, commands);
        }

        private static void AddCommands(IParallelEnvsSettings settings, ICommandWithInfo[] commands)
        {
            foreach(var command in commands)
            {
                command.AttachInfo(settings);
                Commands.Add(command as ICommand);
            }
        }
    }
}
