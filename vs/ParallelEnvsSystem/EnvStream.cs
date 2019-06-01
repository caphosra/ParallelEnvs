//
//
// EnvStream.cs
//
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Capra314Cabra.ParallelEnvs
{
    /// <summary>
    /// 
    /// This class support you to load IParallelEnv from a file or write IParallelEnv to the file.
    /// 
    /// </summary>
    public class EnvStream
    {
        private const char CONTENTS_SEPARATOR = ';';
        private readonly string badFormatMessage;
        private readonly Encoding defaultEncoding;

        /// <summary>
        /// A path to the env file.
        /// </summary>
        public string EnvFilePath { get; }

        public EnvStream(string directoryPath, string envName)
        {
            //
            // Calculate a path to the env file from directoryPath and envName.
            //
            EnvFilePath = Path.Combine(directoryPath, $"{envName}.env");

            //
            // Initalize readonly fields.
            //
            badFormatMessage = $"The file which located at \"{EnvFilePath}\" is not \"ParallelEnvFile\".";
            defaultEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// 
        /// Load IParallelEnv from the file.
        /// 
        /// </summary>
        public IParallelEnv LoadEnv()
        {
            //
            // Generate an IParallelEnv instance.
            //
            var parallelenv = EnvManager.Generate(null);

            using (var sr = new StreamReader(EnvFilePath, defaultEncoding))
            {
                //
                // Read all contents.
                //
                var contentTexts = sr.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

                //
                // If the number of contents are not enough, it would be thrown an exception.
                //
                if (contentTexts.Count < 2)
                {
                    throw new InvalidDataException(badFormatMessage);
                }

                //
                // Convert the text to IParallelEnv.
                //
                parallelenv.Name = contentTexts[0];
                parallelenv.Enabled = 
                    contentTexts[1] == "T" ? true : 
                    (
                        contentTexts[1] == "F" ? false : throw new InvalidDataException(badFormatMessage)
                    );

                //
                // Skip the contents loaded.
                //
                contentTexts = contentTexts.Skip(2).ToList();

                //
                // Load the others.
                //
                parallelenv.Files = ConvertToParallelFile(contentTexts).ToList();
            }

            return parallelenv;
        }

        /// <summary>
        /// 
        /// Generate IParallelFile instances from the list of files.
        /// 
        /// </summary>
        /// <param name="contents">The list of files</param>
        private IEnumerable<IParallelFile> ConvertToParallelFile(List<string> contents)
        {
            foreach(var content in contents)
            {
                var items = content.Split(CONTENTS_SEPARATOR);
                if(items.Length != 2)
                {
                    throw new InvalidDataException(badFormatMessage);
                }

                var file = new InternalParallelFile();
                file.OrdinaryFilePath = items[0];
                file.ManagedFilePath = items[1];

                yield return file;
            }
        }

        /// <summary>
        /// 
        /// Write IParallelEnv to a file.
        /// 
        /// </summary>
        /// <param name="env">The env writen</param>
        public void WriteEnv(IParallelEnv env)
        {
            using (var sw = new StreamWriter(EnvFilePath, append: false, defaultEncoding))
            {
                sw.WriteLine(env.Name);
                sw.WriteLine(env.Enabled ? "T" : "F");
                foreach(var file in env.Files)
                {
                    sw.WriteLine($"{file.OrdinaryFilePath}{CONTENTS_SEPARATOR}{file.ManagedFilePath}");
                }
            }
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
