using System.Collections.Generic;
using System.IO;
using System.Text.Json;

//Reading form JSON files
public static class Utilities
{
    public static Dictionary<string, List<Team>> LoadGroups(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var groupsData = JsonSerializer.Deserialize<Dictionary<string, List<TeamData>>>(json, options);

        var groups = new Dictionary<string, List<Team>>();

        foreach (var group in groupsData)
        {
            var teams = new List<Team>();
            foreach (var teamData in group.Value)
            {
                teams.Add(new Team(teamData.Team, teamData.ISOCode, teamData.FIBARanking));
            }
            groups.Add(group.Key, teams);
        }
        return groups;
    }

    private class TeamData
    {
        public string Team { get; set; }
        public string ISOCode { get; set; }
        public int FIBARanking { get; set; }
    }
}