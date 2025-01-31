﻿using System.Diagnostics;

namespace BisleriumCafe.Data.Utils
{
    public static class Explorer
    {
        public static string GetAppDirectoryPath()
        {
            Debug.WriteLine(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                "Bislerium-Products"
                ));
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                "Bislerium-Products"
                );

        }
        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "users.json");
        }

        public static string GetProductsFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "_products.json.");
        }
        public static string GetAppMembersFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "members.json");
        }
        public static string GetOrderItemsFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "orderItems.json");
        }
        public static string GetSalesFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "sales.json");
        }
    }
}
