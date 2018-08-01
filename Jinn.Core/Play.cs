using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Octokit;
using Octokit.Internal;

namespace Jinn.Core
{
    public class Play
    {
        private readonly GitHubClient _github;

        public Play(string token)
        {
            if(string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException(nameof(token));
            
            _github = new GitHubClient(new ProductHeaderValue("Jinn"), 
                new InMemoryCredentialStore(new Credentials(token)));
        }

        public (string userName, string repoName, string projectName) ParsePackage(string query)
        {
            var match = Regex.Match(query, @"^https:\/\/github\.com\/(.*)\/(.*)$");

            if (!match.Success) throw new RepositoryNotFoundException();

            string userName = match.Groups[1].Value;
            
            string repoName = match.Groups[2].Value;

            return (userName, repoName, null);
        }

        public async Task<Repository> GetRepoHandle(string userName, string repoName)
        {
            return await _github.Repository.Get(userName, repoName);
        }

        public async Task<byte[]> Download(string userName, string repoName)
        {
            return await _github.Repository.Content.GetArchive(userName, repoName, ArchiveFormat.Zipball);
        }
    }
    
    public class RepositoryNotFoundException : Exception
    {
    }
}