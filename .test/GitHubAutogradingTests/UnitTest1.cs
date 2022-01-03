using FluentAssertions;
using GitHubAutogradingTests.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubAutogradingTests
{
    [TestClass]
    public class UnitTest1
    {
        private GitHubAssignment _assignment;

        [TestInitialize]
        public void TestInitialize()
        {
            _assignment = new GitHubAssignment(Settings.BinariesDirectory, Settings.DefaultStdOut, Settings.ExecutableFileMasks);
        }

        [TestMethod]
        public void TestMethod()
        {
            var result = _assignment.Execute("-param Value -param2 Value2");

            result.Should().NotBeEmpty();
        }
    }
}