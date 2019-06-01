//
//
// ParallelEnvsSettings.cs
//
//
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace Com.Capra314Cabra.ParallelEnvs
{
    /// <summary>
    /// 
    /// This class has settings for ParallelEnvs.
    /// 
    /// </summary>
    public class ParallelEnvsSettings : IParallelEnvsSettings
    {
        public string ManagedDirectoryPath { get; set; }

        public List<string> EnvNames { get; set; }

        public string ApplicationWorkDirectoryPath { get; }

        public const string FILE_NAME = "\\Settings.penvs";

        public ParallelEnvsSettings()
        {
            ApplicationWorkDirectoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            ManagedDirectoryPath = Path.Combine(ApplicationWorkDirectoryPath, "Managed");
            EnvNames = new List<string>();
        }

        public void LoadFromFile()
        {
            using (var sr = new StreamReader(ApplicationWorkDirectoryPath + FILE_NAME))
            {
                var text = sr.ReadToEnd();
                var contents = text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                if(contents.Length == 0)
                {
                    throw new InvalidDataException($"The file, {ApplicationWorkDirectoryPath} is not \"ParallelEnvsSettingsFile\".");
                }

                ManagedDirectoryPath = contents[0];
                EnvNames = contents.Skip(1).ToList();
            }
        }

        public void SaveToFile()
        {
            using (var sw = new StreamWriter(ApplicationWorkDirectoryPath + FILE_NAME))
            {
                sw.WriteLine(ManagedDirectoryPath);
                foreach(var env in EnvNames)
                {
                    sw.WriteLine(env);
                }
            }
        }
    }
}
