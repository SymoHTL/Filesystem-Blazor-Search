namespace Domain;

public static class DirectoryInfoExtension {
    public static bool Filter(this DirectoryInfo directoryInfo, SearchItem searchItem) {
        if (searchItem.IgnoreTemporary)
            if (directoryInfo.Attributes.HasFlag(FileAttributes.Temporary))
                return true;

        if (searchItem.IgnoreHidden)
            if (directoryInfo.Attributes.HasFlag(FileAttributes.Hidden))
                return true;


        if (searchItem.IgnoreSystemFiles)
            if (directoryInfo.Attributes.HasFlag(FileAttributes.System))
                return true;


        if (searchItem.IgnoreDollarFiles)
            if (directoryInfo.Name.StartsWith("$"))
                return true;


        if (searchItem.IgnoreWithDotStarting)
            if (directoryInfo.Name.StartsWith("."))
                return true;


        return false;
    }
}