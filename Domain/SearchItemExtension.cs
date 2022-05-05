namespace Domain;

public static class SearchItemExtension {
    public static bool IsFileNameAndExtension(this SearchItem searchItem, string name, string extension) {
        if (string.IsNullOrEmpty(searchItem.FileName) && string.IsNullOrEmpty(searchItem.FileExtension))
            return true;
        if (searchItem.IsFileName(name)) return true;
        if (searchItem.IsFileBoth(name, extension)) return true;
        return false;
    }

    public static bool IsFileName(this SearchItem searchItem, string name) {
        if (!string.IsNullOrEmpty(searchItem.FileExtension)) return false;
        if (searchItem.StartsWithOnly) {
            if (searchItem.CheckFileNameStartsWith(name)) return true;
        }
        else if (searchItem.CheckFileNameContains(name)) return true;
        return false;
    }
    
    public static bool IsFileBoth(this SearchItem searchItem, string name, string extension) {
        if (searchItem.StartsWithOnly) {
            if (searchItem.CheckFileNameStartsWith(name) && extension == searchItem.FileExtension) return true;
        }
        else if (searchItem.CheckFileNameContains(name) && extension == searchItem.FileExtension) return true;
        
        return false;
    }

    public static bool CheckFileNameContains(this SearchItem searchItem, string name) {
        if (searchItem.CaseSensitive) {
            if (name.Contains(searchItem.FileName, StringComparison.Ordinal)) return true;
        }
        else if (name.Contains(searchItem.FileName, StringComparison.OrdinalIgnoreCase)) return true;

        return false;
    }

    public static bool CheckFileNameStartsWith(this SearchItem searchItem, string name) {
        if (searchItem.CaseSensitive) {
            if (name.StartsWith(searchItem.FileName, StringComparison.Ordinal))
                return true;
        }
        else if (name.StartsWith(searchItem.FileName, StringComparison.OrdinalIgnoreCase))
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