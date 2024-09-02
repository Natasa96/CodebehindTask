//Match simulation between two teams

public class Match
{
    public Team Team1 { get; set; }
    public Team Team2 { get; set; }
    public int Team1Score { get; set; }
    public int Team2Score { get; set; }

    public Match(Team team1, Team team2)
    {
        Team1 = team1;
        Team2 = team2;
    }

    public void SimulateMatch()
    {
        Random rand = new Random();
        Team1Score = rand.Next(60, 100);
        Team2Score = rand.Next(60, 100);

        if (Team1.Rank < Team2.Rank)
            Team1Score += rand.Next(0, 10);
        else if (Team1.Rank > Team2.Rank)
            Team2Score += rand.Next(0, 10);

        Team1.ScoredPoints += Team1Score;
        Team1.ConcededPoints += Team2Score;
        Team2.ScoredPoints += Team2Score;
        Team2.ConcededPoints += Team1Score;

        if (Team1Score > Team2Score)
        {
            Team1.Wins++;
            Team1.Points += 2;
            Team2.Losses++;
            Team2.Points += 1;
        }
        else
        {
            Team2.Wins++;
            Team2.Points += 2;
            Team1.Losses++;
            Team1.Points += 1;
        }
    }

    public override string ToString()
    {
        return $"{Team1.Name} - {Team2.Name} ({Team1Score}:{Team2Score})";
    }
}