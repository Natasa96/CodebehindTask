public class Team
{
    public string Name { get; set; }
    public string Iso { get; set; }
    public int Rank { get; set; }
    public int Points { get; set; } = 0;
    public int Wins { get; set; } = 0;
    public int Losses { get; set; } = 0;
    public int ScoredPoints { get; set; } = 0;
    public int ConcededPoints { get; set; } = 0;
    public int PointDifference => ScoredPoints - ConcededPoints;

    public Team(string name, string iso, int rank)
    {
        Name = name;
        Iso = iso;
        Rank = rank;
    }

    public string CalculatePointDifference()
    {
        if (this.PointDifference > 0)
        {
            return $"+{this.PointDifference}";
        }
        else
        {
            return $"{this.PointDifference}";
        }
    }
}