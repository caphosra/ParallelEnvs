using System.IO;

using Xunit;

namespace Com.Capra314Cabra.ParallelEnvs.Test
{
    public class EnvStreamTest
    {
        [Fact(DisplayName = "Create EnvStream")]
        public void Test1()
        {
            new EnvStream("Managed", "Hello");
        }

        [Fact(DisplayName = "Write an env to a file")]
        public void Test2()
        {
            var envName = "Hello";

            Directory.CreateDirectory("Managed");
            var stream = new EnvStream("Managed", envName);
            stream.WriteEnv(EnvManager.Generate(envName));
        }

        [Fact(DisplayName = "Write an env and Load it")]
        public void Test3()
        {
            var envName = "Hello";

            Directory.CreateDirectory("Managed");
            var stream = new EnvStream("Managed", envName);

            var env = EnvManager.Generate(envName);
            stream.WriteEnv(env);

            var env_loaded = stream.LoadEnv();

            Assert.Equal(env.Name, env_loaded.Name);
            Assert.True(env.Enabled == env_loaded.Enabled);
            Assert.Equal(env.Files, env_loaded.Files);
        }
    }
}
