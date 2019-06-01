//
//
// IParallelEnv.cs
//
//
using System.Collections.Generic;

namespace Com.Capra314Cabra.ParallelEnvs
{
    /// <summary>
    /// 
    /// This content expressed a "ParallelEnv".
    /// 
    /// </summary>
    public interface IParallelEnv
    {
        /// <summary>
        /// 
        /// Environment name
        /// 
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 
        /// This environment is enabled? If this value is true, the files exists. Otherwise, they located in "ManagedFolder".
        /// 
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// 
        /// Files which are included in the environment.
        /// 
        /// </summary>
        List<IParallelFile> Files { get; set; }
    }

    /// <summary>
    /// 
    /// This content expressed a file which is managed by ParallelEnvs. 
    /// 
    /// </summary>
    public interface IParallelFile
    {
        /// <summary>
        /// 
        /// The file 
        /// 
        /// </summary>
        string OrdinaryFilePath { get; set; }

        /// <summary>
        /// 
        /// You make the file disabled, when the file will moved to "ManagedFilePath".
        /// 
        /// </summary>
        string ManagedFilePath { get; set; }
    }
}
