namespace Model.Entities;

public class SearchItem {
    public string SaveName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public List<string> ExtensionsToSkip { get; set; } = new();

    public bool ShouldSave { get; set; }

    public long MinSize { get; set; } = 0;
    public long MaxSize { get; set; } = 0;
    public bool CaseSensitive { get; set; }
    public bool StartsWithOnly { get; set; } = true;
    public bool IgnoreDollarFiles { get; set; } = true;
    public bool IgnoreHidden { get; set; } = true;
    public bool IgnoreSystemFiles { get; set; } = true;
    public bool IgnoreTemporary { get; set; } = true;
    public bool IgnoreWithDotStarting { get; set; } = true;
    public bool IgnoreWithUnderscoreStarting { get; set; } = true;
}