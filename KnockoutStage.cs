using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Numerics;
using System.Security;

public class KnockoutStage
{
    private List<Team> qualifiedTeams;
    private List<(Team, Team)> groupMatches;

    public KnockoutStage(List<Team> qualifiedTeams, List<(Team, Team)> groupMatches)
    {
        this.qualifiedTeams = qualifiedTeams;
        this.groupMatches = groupMatches;
    }

    public void Simulate()
    {
        Console.WriteLine("\nEliminaciona faza:");

        var quarterFinals = GenerateMatches(qualifiedTeams, true);
        var semiFinals = SimulateRound(quarterFinals, "Cetvrtfinale:");
        var finals = SimulateRound(semiFinals, "Polufinale:");

        SimulateFinals(finals);
    }

    private List<Match> GenerateMatches(List<Team> teams, bool checkHistory = false)
    {
        var matches = new List<Match>();
        var random = new Random();
        var avaibleTeams = new List<Team>(teams);

        //Dictionary<int, List<Team>> hat = GenerateHat(teams);

        while (avaibleTeams.Count > 1)
        {
            Team team1 = avaibleTeams[random.Next(avaibleTeams.Count)];

            Team team2 = checkHistory ? avaibleTeams.FirstOrDefault(t => !HasPlayedBefore(team1, t)) : avaibleTeams.First(t => t != team1);
            if (team2 != null)
            {
                matches.Add(new Match(team1, team2));
                avaibleTeams.Remove(team1);
                avaibleTeams.Remove(team2);
            }
        }
        return matches;
    }

    public Dictionary<int, List<Team>> GenerateHat(List<Team> teams)
    {
        var hat = new Dictionary<int, List<Team>>();
        for (int i = 0; i < 4; i++)
        {
            hat.Add(i, teams.OrderByDescending(t => t.Rank)
                        .ThenByDescending(t => t.Points)
                        .ThenByDescending(t => t.PointDifference)
                        .Take(2).ToList());
            //teams = teams.Except(hat.Values).ToList();
            hat[i].ForEach(t => teams.Remove(t));
        }
        return hat;
    }

    public bool HasPlayedBefore(Team team1, Team team2)
    {
        return groupMatches.Any(match => (
            (match.Item1 == team1 && match.Item2 == team2) ||
            (match.Item1 == team2 && match.Item2 == team1)
        ));
    }

    private List<Match> SimulateRound(List<Match> matches, string roundName)
    {
        Console.WriteLine($"\n{roundName}");
        var nextRoundTeams = new List<Team>();

        foreach (var match in matches)
        {
            match.SimulateMatch();
            Console.WriteLine(match);

            nextRoundTeams.Add(match.Team1Score > match.Team2Score ? match.Team1 : match.Team2);
        }
        return GenerateMatches(nextRoundTeams);
    }

    private void SimulateFinals(List<Match> finals)
    {
        Console.WriteLine("\nFinale:");
        foreach (var match in finals)
        {
            match.SimulateMatch();
            Console.WriteLine(match);
        }
    }
}