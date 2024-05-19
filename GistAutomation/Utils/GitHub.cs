using Octokit;
using Octokit.Internal;

namespace GistAutomation.Utils;

public static class GitHub
{
    private const string GIST_UPDATE_HEADER = "gistupdate";

    public static async Task Save(string githubAccessToken, string gistId, string data)
    {
        var credentials = new InMemoryCredentialStore(new Credentials(githubAccessToken));
        var client = new GitHubClient(new ProductHeaderValue(GIST_UPDATE_HEADER), credentials);
        var gistUpdate = new GistUpdate
        {
            Description = $"List Of Countries With States And Other Useful Information, Updated On {DateTime.UtcNow}",
        };
        gistUpdate.Files.Add("country_state.json", new GistFileUpdate
        {
            Content = data
        });
        await client.Gist.Edit(gistId, gistUpdate);
    }
}