//
//
// IParallelEnvsSetting.cs
//
//
using System.Collections.Generic;

namespace Com.Capra314Cabra.ParallelEnvs
{
    /// <summary>
    /// 
    /// This content expressed the settings file.
    /// 
    /// </summary>
    public interface IParallelEnvsSettings
    {
        /// <summary>
        /// 
        /// Managed directory path
        /// 
        /// </summary>
        string ManagedDirectoryPath { get; set; }

        /// <summary>
        /// 
        /// Application work directory path
        /// 
        /// </summary>
        string ApplicationWorkDirectoryPath { get; }

        /// <summary>
        /// 
        /// The environments
        /// 
        /// </summary>
        List<string> EnvNames { get; set; }

        /// <summary>
        /// 
        /// Load the ParallelEnvs settings from the file.
        /// 
        /// </summary>
        void LoadFromFile();

        /// <summary>
        /// 
        /// Write the ParallelEnvs settings to a file.
        /// 
        /// </summary>
        void SaveToFile();
    }
}
