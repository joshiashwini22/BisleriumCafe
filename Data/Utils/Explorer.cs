namespace BisleriumCafe.Data.Utils
{
    public static class Explorer
    {
        public static string GetAppDirectoryPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Bislerium-Products"
                );
        }
        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "users.json");
        }

        public static string GetProductsFilePath(Guid userId)
        {
            return Path.Combine(GetAppDirectoryPath(), userId.ToString() + "_products.json.");
        }
    }
}
