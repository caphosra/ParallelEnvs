using System;
using System.IO;

using Xunit;

namespace Com.Capra314Cabra.ParallelEnvs.Test
{
    public class EnvManagerTest : IDisposable
    {
        private string existsFilePath;
        private string unknownFilePath;

        public EnvManagerTest()
        {
            existsFilePath = Guid.NewGuid().ToString();
            unknownFilePath = Guid.NewGuid().ToString();

            using (var stream = new StreamWriter(existsFilePath))
            {
                stream.Write("SomeContents");
            }
        }

        public void Dispose()
        {
            if (File.Exists(existsFilePath))
            {
                File.Delete(existsFilePath);
            }
        }

        /// <summary>
        /// 
        /// Generate new env
        /// 
        /// </summary>
        [Fact(DisplayName = "Generate new env")]
        public void Test1()
        {
            var env = EnvManager.Generate("Hello");

            Assert.Equal("Hello", env.Name);
            Assert.True(env.Enabled);
        }

        /// <summary>
        /// 
        /// Add a file
        /// 
        /// </summary>
        [Fact(DisplayName = "Add a file")]
        public void Test2()
        {
            var env = EnvManager.Generate("Hello");

            var settings = new ParallelEnvsSettings();
            settings.EnvNames.Add(env.Name);

            EnvManager.AddFile(ref env, existsFilePath, settings);
        }

        /// <summary>
        /// 
        /// Add a unknown file
        /// 
        /// </summary>
        [Fact(DisplayName = "Add a unknown file")]
        public void Test3()
        {
            var env = EnvManager.Generate("Hello");

            var settings = new ParallelEnvsSettings();
            settings.EnvNames.Add(env.Name);

            Assert.Throws<FileNotFoundException>(() =>
            {
                EnvManager.AddFile(ref env, unknownFilePath, settings);
            });
        }
    }
}
