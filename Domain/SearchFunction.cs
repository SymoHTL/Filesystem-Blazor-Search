namespace Domain;

public static class SearchFunction {
    public static (string, string, string) Init(SearchItem searchItem) {
        var filePathBuilder = new StringBuilder();
        var statsBuilder = new StringBuilder();
        var errorBuilder = new StringBuilder();
        var stopwatch = new Stopwatch();

        var foundFiles = 0;
        var searchedFiles = 0;
        var searchedDirectories = 0;
        var noAccess = 0;

        stopwatch.Start();

        Searching(searchItem.Path);


        void Searching(string path) {
            try {
                var directoryInfo = new DirectoryInfo(path);
                if (!directoryInfo.Exists)
                    throw new DirectoryNotFoundException($"Source directory not found: {directoryInfo.FullName}");

                searchedDirectories++;
                foreach (var file in directoryInfo.GetFiles()) {
                    searchedFiles++;
                    if (file.CheckFile(searchItem)) continue;

                    foundFiles++;
                    filePathBuilder.AppendLine($"Found after {stopwatch.ElapsedMilliseconds} ms : {file.FullName}");
                }

                //  Recursion
                foreach (var subDir in directoryInfo.GetDirectories()) {
                    if (subDir.Filter(searchItem))
                        continue;
                    Searching(subDir.FullName);
                }
            }
            catch (Exception e) {
                if (e is UnauthorizedAccessException) noAccess++;
                errorBuilder.AppendLine(e.Message);
            }
        }

        stopwatch.Stop();
        statsBuilder.AppendLine("Elapsed time: " + stopwatch.ElapsedMilliseconds + " ms");
        statsBuilder.AppendLine("Found files: " + foundFiles);
        statsBuilder.AppendLine("Searched files: " + searchedFiles);
        statsBuilder.AppendLine("Searched directories: " + searchedDirectories);
        statsBuilder.AppendLine("No access: " + noAccess);


        return (filePathBuilder.ToString(), statsBuilder.ToString(), errorBuilder.ToString());
    }
}