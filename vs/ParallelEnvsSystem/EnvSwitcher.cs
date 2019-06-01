//
//
// EnvSwitcher.cs
//
//
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Com.Capra314Cabra.ParallelEnvs
{
    /// <summary>
    /// 
    /// This class support you to turn the env on or off.
    /// 
    /// </summary>
    public class EnvSwitcher
    {
        /// <summary>
        /// 
        /// Turn on the env.
        /// 
        /// </summary>
        public void TurnOn(IParallelEnv env)
        {
            //
            // If the env had been turned off, it would be thrown an exception.
            //
            if(env.Enabled)
            {
                throw new ArgumentException("The env should be disabled when you call TurnOn().");
            }

            env.Enabled = true;

            foreach(var file in env.Files)
            {
                //
                // If the managed file didn't exist, it would be thrown an exception.
                //
                if(!File.Exists(file.ManagedFilePath))
                {
                    throw new FileNotFoundException($"The unmanaged file, \"{file.ManagedFilePath}\" is not found.");
                }

                //
                // If the file targeted existed, it would be deleted.
                //
                if(File.Exists(file.OrdinaryFilePath))
                {
                    File.Delete(file.OrdinaryFilePath);
                }

                File.Copy(file.ManagedFilePath, file.OrdinaryFilePath);
            }
        }

        /// <summary>
        /// 
        /// Turn off the env.
        /// 
        /// </summary>
        public void TurnOff(IParallelEnv env)
        {
            //
            // If the env had been turned on, it would be thrown an exception.
            //
            if (!env.Enabled)
            {
                throw new ArgumentException("The env should be enabled when you call TurnOff().");
            }

            env.Enabled = false;

            foreach (var file in env.Files)
            {
                //
                // If the managed file didn't exist, it would be thrown an exception.
                //
                if (!File.Exists(file.ManagedFilePath))
                {
                    throw new FileNotFoundException($"The unmanaged file, \"{file.ManagedFilePath}\" is not found.");
                }

                //
                // If the file targeted existed, it would be thrown an exception.
                //
                if (!File.Exists(file.OrdinaryFilePath))
                {
                    throw new FileNotFoundException($"The managed file, \"{file.OrdinaryFilePath}\" is not found.");
                }

                File.Delete(file.OrdinaryFilePath);
            }
        }
    }
}
