using BisleriumCafe.Data.Utils;
using BisleriumCafe.Data.Enums;
using BisleriumCafe.Data.Models;
using System.Text.Json;
using System.Data;

namespace BisleriumCafe.Data.Services;

public static class MembersService
{
    private static void SaveAll(List<Member> members)
    {
        string appDataDirectoryPath = Explorer.GetAppDirectoryPath();
        string membersFilePath = Explorer.GetAppMembersFilePath();

        if (!Directory.Exists(appDataDirectoryPath))
        {
            Directory.CreateDirectory(appDataDirectoryPath);
        }

        var json = JsonSerializer.Serialize(members);
        File.WriteAllText(membersFilePath, json);
    }

    public static List<Member> GetAll()
    {
        string membersFilePath = Explorer.GetAppMembersFilePath();
        if (!File.Exists(membersFilePath))
        {
            return new List<Member>();
        }

        var json = File.ReadAllText(membersFilePath);

        return JsonSerializer.Deserialize<List<Member>>(json);
    }


    public static List<Member> Create(string firstName, string lastName, string number)
    {

        List<Member> members = GetAll();
        bool numberExists = members.Any(x => x.Number == number);

        if (numberExists)
        {
            throw new Exception("Number already exists.");
        }

        members.Add(
            new Member
            {
                FirstName = firstName,
                LastName = lastName,
                Number = number
            }
        );
        SaveAll(members);
        return members;
    }

    public static Member GetById(string number)
    {
        List<Member> numbers = GetAll();
        return numbers.FirstOrDefault(x => x.Number == number);
    }
}