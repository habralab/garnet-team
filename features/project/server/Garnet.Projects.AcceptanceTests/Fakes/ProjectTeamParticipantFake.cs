using Garnet.Common.Infrastructure.Support;

namespace Garnet.Projects.AcceptanceTests.Fakes;

public class ProjectTeamParticipantFake
{
    private readonly Dictionary<string, string> _teams = new()
    {
        { "testTeam", "testTeamId" }
    };


    public string CreateTeam(string name, string id)
    {
        _teams[name] = id;
        return id;
    }

    public string GetTeamIdByTeamName(string username)
    {
        return _teams[username];
    }

    public string GetTeamNameByTeamId(string id)
    {
        var username = _teams.FirstOrDefault(x => x.Value == id).Key;
        return username;
    }
}