//
// 
// EnvCreater.cs
//
//
using System;
using System.Collections.Generic;
using System.IO;

namespace Com.Capra314Cabra.ParallelEnvs
{
    /// <summary>
    /// 
    /// EnvManager has many functions which controlls the IParallelEnv instance.
    /// 
    /// </summary>
    public static class EnvManager
    {
        /// <summary>
        /// 
        /// Generate an IParallelEnv instance which has no contents.
        /// 
        /// </summary>
        /// <param name="name">Env Names</param>
        public static IParallelEnv Generate(string name)
        {
            var env = new InternalParallelEnv();
            env.Name = name;
            env.Enabled = true;
            env.Files = new List<IParallelFile>();
            return env;
        }

        /// <summary>
        /// 
        /// Add a file to an IParallelEnv.
        /// Warnings: After calling this function, you should save the env to a file.
        /// 
        /// </summary>
        /// <param name="env">To be added</param>
        /// <param name="filePath">The path for the file</param>
        /// <param name="settings">IParallelEnvsSettings</param>
        public static void AddFile(ref IParallelEnv env, string filePath, IParallelEnvsSettings settings)
        {
            //
            // If there were no file which is located at "filePath", it would be thrown an exception.
            //
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file, {filePath} is not found.");
            }

            //
            // Generate a managed file path from GUID.
            //
            var file = new InternalParallelFile() as IParallelFile;
            file.OrdinaryFilePath = filePath;
            file.ManagedFilePath = Path.Combine(settings.ManagedDirectoryPath, Guid.NewGuid().ToString());

            Directory.CreateDirectory(settings.ManagedDirectoryPath);

            //
            // Copy the file to the managed file path.
            //
            File.Copy(file.OrdinaryFilePath, file.ManagedFilePath);

            //
            // Add a file to the env.
            //
            env.Files.Add(file);
        }

        /// <summary>
        /// 
        /// Implementation for IParallelEnv
        /// 
        /// </summary>
        private class InternalParallelEnv : IParallelEnv
        {
            public string Name { get; set; }
            public bool Enabled { get; set; }
            public List<IParallelFile> Files { get; set; }
        }

        /// <summary>
        /// 
        /// Implementation for IParallelFile
        /// 
        /// </summary>
        private class InternalParallelFile : IParallelFile
        {
            public string OrdinaryFilePath { get; set; }
            public string ManagedFilePath { get; set; }
        }
    }
}
