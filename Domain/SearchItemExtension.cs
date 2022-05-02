namespace Domain;

public static class SearchItemExtension {
    public static bool IsFileNameAndExtension(this SearchItem searchItem, string name, string extension) {
        if (string.IsNullOrEmpty(searchItem.FileName) && string.IsNullOrEmpty(searchItem.FileExtension))
            return true;
        if (string.IsNullOrEmpty(searchItem.FileExtension))
            if (name.Contains(searchItem.FileName, StringComparison.OrdinalIgnoreCase))
                return true;
        if (name.Contains(searchItem.FileName, StringComparison.OrdinalIgnoreCase) &&
            extension == searchItem.FileExtension)
            return true;
        return false;
    }

    public static void Delete(this SearchItem save) =>
        File.Delete(Directory.GetCurrentDirectory() + @"\" + save.SaveName + ".json");

    public static async Task Save(this SearchItem searchItem) {
        var jsonString = JsonSerializer.Serialize(searchItem);
        searchItem.SaveName = $"Save-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}";
        await File.WriteAllTextAsync(@$"{Directory.GetCurrentDirectory()}\{searchItem.SaveName}.json", jsonString);
    }

    public static async Task<List<SearchItem>> Read() {
        var saves = new List<SearchItem>();
        var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
        foreach (var file in directoryInfo.GetFiles()) {
            if (!file.Name.Contains("Save-")) continue;
            if (file.Extension != ".json") continue;
            var jsonString = await File.ReadAllTextAsync(file.FullName);
            var searchItem = JsonSerializer.Deserialize<SearchItem>(jsonString);
            saves.Add(searchItem!);
        }

        return saves.OrderByDescending(s => s.SaveName).ToList();
    }
}