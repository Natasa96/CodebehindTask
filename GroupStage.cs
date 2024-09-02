using System.Collections.Generic;

public class GroupStage
{
    private Dictionary<string, List<Team>> groups;
    private List<(Team, Team)> groupMatches;

    public GroupStage(Dictionary<string, List<Team>> groups)
    {
        this.groups = groups;
        this.groupMatches = new List<(Team, Team)>();
    }

    public void Simulate()
    {
        foreach (var group in groups)
        {
            Console.WriteLine($"Simulacija meceva u grupnoj fazi {group.Key}");

            var teams = group.Value;
            for (int i = 0; i < teams.Count; i++)
            {
                for (int j = i + 1; j < teams.Count; j++)
                {
                    var match = new Match(teams[i], teams[j]);
                    match.SimulateMatch();
                    Console.WriteLine(match);

                    groupMatches.Add((teams[i], teams[j]));
                }
            }

            teams.Sort((t1, t2) => t2.Points != t1.Points ? t2.Points.CompareTo(t1.Points)
            : (t2.PointDifference != t1.PointDifference ? t2.PointDifference.CompareTo(t1.PointDifference)
            : t2.ScoredPoints.CompareTo(t1.ScoredPoints)));

            Console.WriteLine();
        }
    }

    public void DisplayGroupResults()
    {
        Console.WriteLine("\nRezultati grupne faze:\nRank / Tim / Pobede / Porazi / Bodovi / Dati kosevi / Primljeni kosevi / Razlika");
        foreach (var group in groups)
        {
            Console.WriteLine($"Group {group.Key}");
            int rank = 1;
            foreach (var team in group.Value)
            {
                Console.WriteLine($"{rank}. {team.Name} - {team.Wins}/{team.Losses}/{team.Points} - {team.ScoredPoints}/{team.ConcededPoints} ({team.CalculatePointDifference()})");
                rank++;
            }
            Console.WriteLine();
        }
    }

    public List<(Team, Team)> GetGroupMatches()
    {
        return groupMatches;
    }
}