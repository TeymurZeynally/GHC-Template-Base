using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubAutogradingTests.Configuration
{
    internal static class Settings
    {
        private static IConfigurationRoot _config;

        static Settings()
        {
            _config = new ConfigurationBuilder()
                .AddYamlFile("appsettings.yml")
                .Build();
        }

        public static string BinariesDirectory => _config.GetValue<string>("inputs:binariesDirectory");

        public static string DefaultStdOut => _config.GetValue<string>("inputs:defaultStdOut");

        public static string[] ExecutableFileMasks => _config.GetSection("inputs:executableFileMasks").Get<string[]>();
    }
}
