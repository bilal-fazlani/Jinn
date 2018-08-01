using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Jinn.Core.Tests
{
    public class PlayTests : TestConfig
    {
        private Play play = new Play(Config.GithubToken);
        
        string userName = "bilal-fazlani";
        string repoName = "LiteDb.AutoApi";
        
        [Fact]
        public void DoStuffTest()
        {
            string query = "https://github.com/bilal-fazlani/LiteDb.AutoApi";
            var (userName, repoName, projectName) = play.ParsePackage(query);

            userName.Should().Be("bilal-fazlani");
            repoName.Should().Be("LiteDb.AutoApi");
            
            projectName.Should().BeNull();
        }

        [Fact]
        public async Task RepoHandleTest()
        {
            var repo = await play.GetRepoHandle(userName, repoName);
            repo.NodeId.Should().Be("MDEwOlJlcG9zaXRvcnkxMjQ2OTM2ODU=");
        }

        [Fact]
        public async Task DownloadTest()
        {
            var data = await play.Download(userName, repoName);
            var zipName = "./downloadTest.zip";
            await File.WriteAllBytesAsync(zipName, data);
            ZipFile.ExtractToDirectory(zipName, "./downloadedRepo", true);
        }
    }
}