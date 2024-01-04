using BisleriumCafe.Data.Models;
using BisleriumCafe.Data.Utils;
using BisleriumCafe.Data.Enums;
using System.Text.Json;

namespace BisleriumCafe.Data.Services;

public static class UsersService
{
    public const string SeedUsername = "admin";
    public const string SeedPassword = "admin";

    private static void SaveAll(List<User> users)
    {
        string appDataDirectoryPath = Explorer.GetAppDirectoryPath();
        string appUsersFilePath = Explorer.GetAppUsersFilePath();

        if (!Directory.Exists(appDataDirectoryPath))
        {
            Directory.CreateDirectory(appDataDirectoryPath);
        }

        var json = JsonSerializer.Serialize(users);
        File.WriteAllText(appUsersFilePath, json);
    }
    public static List<User> GetAll()
    {
        string appUsersFilePath = Explorer.GetAppUsersFilePath();
        if (!File.Exists(appUsersFilePath))
        {
            return new List<User>();
        }

        var json = File.ReadAllText(appUsersFilePath);

        return JsonSerializer.Deserialize<List<User>>(json);
    }

    public static List<User> Create(Guid userId, string username, string password, Role role)
    {
        List<User> users = GetAll();
        bool usernameExists = users.Any(x => x.Username == username);

        if (usernameExists)
        {
            throw new Exception("Username already exists.");
        }

        users.Add(
            new User
            {
                Username = username,
                HashedPassword = Hasher.HashedSecret(password),
                Role = role,
                CreatedBy = userId
            }
        );
        SaveAll(users);
        return users;
    }
    public static void SeedUsers()
    {
        var users = GetAll().FirstOrDefault(x => x.Role == Role.Admin);

        if (users == null)
        {
            Create(Guid.Empty, SeedUsername, SeedPassword, Role.Admin);
        }
    }

    public static User GetById(Guid id)
    {
        List<User> users = GetAll();
        return users.FirstOrDefault(x => x.ID == id);
    }

    public static List<User> Delete(Guid id)
    {
        List<User> users = GetAll();
        User user = users.FirstOrDefault(x => x.ID == id);

        if (user == null)
        {
            throw new Exception("User not found.");
        }

        //TodosService.DeleteByUserId(id);
        users.Remove(user);
        SaveAll(users);

        return users;
    }

    public static User Login(string username, string password)
    {
        var loginErrorMessage = "Invalid username or password.";
        List<User> users = GetAll();
        User user = users.FirstOrDefault(x => x.Username == username);

        if (user == null)
        {
            throw new Exception(loginErrorMessage);
        }

        bool passwordIsValid = Hasher.VerifyHashedSecret(password, user.HashedPassword);

        if (!passwordIsValid)
        {
            throw new Exception(loginErrorMessage);
        }

        return user;
    }

    public static User ChangePassword(Guid id, string currentPassword, string newPassword)
    {
        if (currentPassword == newPassword)
        {
            throw new Exception("New password must be different from current password.");
        }

        List<User> users = GetAll();
        User user = users.FirstOrDefault(x => x.ID == id);

        if (user == null)
        {
            throw new Exception("User not found.");
        }

        bool passwordIsValid = Hasher.VerifyHashedSecret(currentPassword, user.HashedPassword);

        if (!passwordIsValid)
        {
            throw new Exception("Incorrect current password.");
        }

        user.HashedPassword = Hasher.HashedSecret(newPassword);
        user.HasInitialPassword = false;
        SaveAll(users);

        return user;
    }

}
