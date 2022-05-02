namespace Model.Entities;

public class Stats {
    public long Time { get; set; }
    public int FoundFiles { get; set; }
    public int CheckedFiles { get; set; }
    public int CheckedDirectories { get; set; }
    public int CheckedSubDirectories { get; set; }
    public int NoAccessErrors { get; set; }
}