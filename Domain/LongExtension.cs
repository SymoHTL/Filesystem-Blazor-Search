namespace Domain;

public static class LongExtension {
    public static string ConvertToReadable(this long num) {
        if (num / 1024f < 1) return num + "B";
        if (num / 1048576f < 1) return (num / 1024f).ToString("n2") + "KB";
        if (num / 1073741824f < 1) return (num / 1048576f).ToString("n2") + "MB";
        if (num / 1099511627776f < 1) return (num / 1073741824f).ToString("n2") + "GB";
        if (num / 1125899906842624f < 1) return (num / 1099511627776f).ToString("n2") + "TB";
        return "sus";
    }
}