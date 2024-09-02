using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Load Groups from JSON
        var groups = Utilities.LoadGroups("groups.json");

        // Simulate Group Stage
        var groupStage = new GroupStage(groups);
        groupStage.Simulate();
        groupStage.DisplayGroupResults();

        // Get top 8 teams
        var qualifiedTeams = groups.Values.SelectMany(g => g).OrderByDescending(t => t.Points)
                            .ThenByDescending(t => t.PointDifference)
                            .ThenByDescending(t => t.ScoredPoints)
                            .Take(8).ToList();

        // Simulate Knockout Stage
        var knockoutStage = new KnockoutStage(qualifiedTeams, groupStage.GetGroupMatches());
        knockoutStage.Simulate();
    }
}