namespace Domain;

public static class FileInfoExtension {
    public static bool CheckFile(this FileInfo file, SearchItem searchItem) {
        if (file.Filter(searchItem)) return true;
        if (file.FilterOutExtensions(searchItem)) return true;
        if (!searchItem.IsFileNameAndExtension(file.Name, file.Extension)) return true;
        return false;
    }

    public static bool FilterOutExtensions(this FileInfo fileInfo, SearchItem searchItem) {
        return searchItem.ExtensionsToSkip.Any(extension => fileInfo.Extension == extension);
    }

    public static bool Filter(this FileInfo fileInfo, SearchItem searchItem) {
        if (searchItem.IgnoreTemporary)
            if (fileInfo.Attributes.HasFlag(FileAttributes.Temporary))
                return true;
        if (searchItem.IgnoreHidden)
            if (fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
                return true;
        if (searchItem.IgnoreSystemFiles)
            if (fileInfo.Attributes.HasFlag(FileAttributes.System))
                return true;
        if (searchItem.IgnoreDollarFiles)
            if (fileInfo.Name.StartsWith("$"))
                return true;
        if (searchItem.IgnoreWithDotStarting)
            if (fileInfo.Name.StartsWith("."))
                return true;
        return false;
    }
}