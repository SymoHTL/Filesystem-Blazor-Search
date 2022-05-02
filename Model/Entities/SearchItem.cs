namespace Model.Entities;

public class SearchItem {
    public string SaveName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;

    public List<string> ExtensionsToSkip { get; set; } = new();
    public bool IgnoreDollarFiles { get; set; } = true;
    public bool IgnoreHidden { get; set; } = true;
    public bool IgnoreSystemFiles { get; set; } = true;
    public bool IgnoreTemporary { get; set; } = true;
    public bool IgnoreWithDotStarting { get; set; } = true;
}