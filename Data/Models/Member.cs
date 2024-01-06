using BisleriumCafe.Data.Enums;

namespace BisleriumCafe.Data.Models;

public class Member
{
    public Guid MemberId { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Number { get; set; }
}