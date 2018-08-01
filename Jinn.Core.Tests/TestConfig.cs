using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Jinn.Core.Tests
{
    public class TestConfig
    {
        protected static Config Config = GetConfig();
        
        private static Config GetConfig()
        {
            return CreateConfig().Get<Config>();
        }
        
        private static IConfiguration CreateConfig()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetAssembly(typeof(TestConfig)));
            return configBuilder.Build();
        }
    }

    public class Config
    {
        public string GithubToken { get; set; }
    }
}