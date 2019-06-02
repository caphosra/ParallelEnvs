using System;

using Xunit;

namespace Com.Capra314Cabra.ParallelEnvs.Test
{
    public class EnvSwitcherTest : IDisposable
    {
        EnvSwitcher switcher;

        public EnvSwitcherTest()
        {
            switcher = new EnvSwitcher();
        }

        public void Dispose()
        {

        }

        [Fact(DisplayName = "Turn on and off")]
        public void Test1()
        {
            var env = EnvManager.Generate("Hello");
            switcher.TurnOff(env);
            switcher.TurnOn(env);
        }

        [Fact(DisplayName = "Turn the env disabled off")]
        public void Test2()
        {
            var env = EnvManager.Generate("Hello");
            env.Enabled = false;
            Assert.Throws<ArgumentException>(() =>
            {
                switcher.TurnOff(env);
            });
        }

        [Fact(DisplayName = "Turn the env enabled on")]
        public void Test3()
        {
            var env = EnvManager.Generate("Hello");
            Assert.Throws<ArgumentException>(() =>
            {
                switcher.TurnOn(env);
            });
        }
    }
}
