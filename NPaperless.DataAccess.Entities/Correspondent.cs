namespace NPaperless.DataAccess.Entities;

public class Correspondent
{
    public int Id { get; set; }
    public string Slug { get; set; }
    public string Name { get; set; }
    public string Match { get; set; }
    public int MatchingAlgorithm { get; set; }
    public bool IsInsensitive { get; set; }
    public int DocumentCount { get; set; }
    public DateTime? LastCorrespondence { get; set; }
    public int Owner { get; set; }
}